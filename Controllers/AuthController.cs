using FleetPulse_BackEndDevelopment.Configuration;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMailService _mailService;
        private readonly IEmailService _emailService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly FleetPulseDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly MailSettings _mailSettings;

        public AuthController(IAuthService authService,
            IMailService mailService,
            IEmailService emailService,
            IVerificationCodeService verificationCodeService,
            FleetPulseDbContext context,
            IConfiguration configuration,
            ILogger<AuthController> logger,
            IOptions<MailSettings> mailSettings)
        {
            _authService = authService;
            _mailService = mailService;
            _emailService = emailService;
            _verificationCodeService = verificationCodeService;
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _mailSettings = mailSettings.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginDTO userModel)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Error = "Invalid Data";
                    return BadRequest(response);
                }

                var user = _authService.IsAuthenticated(userModel.Username, userModel.Password);

                if (user != null)
                {
                    string[] validJobTitles;
                    string errorMessage;

                    if (user.JobTitle == "Admin" || user.JobTitle == "Staff")
                    {
                        validJobTitles = new[] { "Admin", "Staff" };
                        errorMessage = "Unauthorized: Only Admin or Staff can login";
                    }
                    else if (user.JobTitle == "Driver" || user.JobTitle == "Helper")
                    {
                        validJobTitles = new[] { "Driver", "Helper" };
                        errorMessage = "Unauthorized: Only Driver or Helper can login";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Unauthorized: Your job title does not have access to this endpoint";
                        return new JsonResult(response);
                    }

                    if (validJobTitles.Contains(user.JobTitle))
                    {
                        var accessToken = await _authService.GenerateJwtToken(user.UserName, user.JobTitle);
                        var refreshToken = await _authService.GenerateRefreshToken(user.UserId);

                        response.Data = new
                            { AccessToken = accessToken, RefreshToken = refreshToken, JobTitle = user.JobTitle };
                        return new JsonResult(response);
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = errorMessage;
                        return new JsonResult(response);
                    }
                }

                response.Status = false;
                response.Message = "Invalid username or password";
                return new JsonResult(response);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occurred while processing the login request: {Message}",
                    error.Message);
                response.Status = false;
                response.Error = "An internal error occurred";
                return StatusCode(500, response);
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var emailExists = _authService.DoesEmailExists(model.Email);

                    if (!emailExists)
                    {
                        response.Status = false;
                        response.Message = "Email not found";
                        return new JsonResult(response);
                    }

                    var verificationCode = await _verificationCodeService.GenerateVerificationCode(model.Email);
                    var mailRequest = new MailRequest
                    {
                        Subject = "Password Reset Verification",
                        Body = verificationCode.Code,
                        ToEmail = model.Email
                    };

                    await _mailService.SendEmailAsync(mailRequest);

                    response.Message = "Verification code sent successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Message = "Invalid model state";
                    return BadRequest(response);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occurred while processing the forgot password request: {Message}",
                    error.Message);

                response.Status = false;
                response.Message = "An error occurred while processing your request";
                return StatusCode(500, response);
            }
        }

        [AllowAnonymous]
        [HttpPost("validate-verification-code")]
        public async Task<IActionResult> ValidateVerificationCode([FromBody] ValidateVerificationCodeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ApiResponse
            {
                Status = true
            };

            bool isValid = await _verificationCodeService.ValidateVerificationCode(request.Email, request.Code);

            if (isValid)
            {
                return new JsonResult(response);
            }
            else
            {
                response.Status = false;
                response.Error = "Invalid Data";
                return BadRequest(response);
            }
        }

        [HttpPost("reset-password-staff")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var emailExists = _authService.DoesEmailExists(model.Email);

                    if (!emailExists)
                    {
                        response.Status = false;
                        response.Message = "Email not found";
                        return new JsonResult(response);
                    }

                    bool passwordReset = _authService.ResetPassword(model.Email, model.NewPassword);

                    if (passwordReset)
                    {
                        response.Message = "Password reset successfully";
                        return new JsonResult(response);
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed to reset password";
                        return new JsonResult(response);
                    }
                }
                else
                {
                    response.Message = "Invalid model state";
                    return BadRequest(response);
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occurred while processing the reset password request: {Message}",
                    error.Message);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpPost("reset-password-driver")]
        public async Task<IActionResult> ResetDriverPassword([FromBody] ResetDriverPasswordRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.EmailAddress) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Invalid request.");
            }

            var result = await _authService.ResetDriverPasswordAsync(request.EmailAddress, request.NewPassword);

            if (result)
            {
                var mailRequest = new MailRequest
                {
                    ToEmail = request.EmailAddress,
                    Subject = "Password Reset Notification",
                    Body = request.NewPassword
                };

                try
                {
                    await _emailService.SendEmailAsync(mailRequest);
                }
                catch (Exception ex)
                {
                    // Handle email sending error
                    return StatusCode(500, "Password reset successful but failed to send email notification.");
                }

                return Ok(new { Status = true, Message = "Password reset successful." });
            }
            else
            {
                return BadRequest(new { Status = false, Error = "Failed to reset password." });
            }
        }
        
        [HttpPost("change-password-staff")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var user = _authService.GetByUsername(model.Username);

                    if (user == null)
                    {
                        response.Status = false;
                        response.Error = "Failed to change password";
                        return new JsonResult(response);
                    }

                    var isOldPasswordValid = _authService.IsAuthenticated(user.UserName, model.OldPassword);

                    if (isOldPasswordValid == null)
                    {
                        response.Status = false;
                        response.Error = "Old password is incorrect";
                        return new JsonResult(response);
                    }

                    var passwordReset = _authService.ResetPassword(user.EmailAddress, model.NewPassword);

                    if (passwordReset)
                    {
                        response.Message = "Password changed successfully";
                        return new JsonResult(response);
                    }

                    response.Status = false;
                    response.Error = "Failed to change password";
                    return new JsonResult(response);
                }

                response.Error = "Invalid model state";
                return BadRequest(response);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occurred while processing the change password request: {Message}",
                    error.Message);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] StaffDTO staff)
        {
            try
            {
                var oldUser = _authService.GetByUsername(staff.UserName);

                if (oldUser is null)
                {
                    return NotFound("User not found");
                }

                oldUser.FirstName = staff.FirstName;
                oldUser.LastName = staff.LastName;
                oldUser.NIC = staff.NIC;
                oldUser.DateOfBirth = staff.DateOfBirth;
                oldUser.PhoneNo = staff.PhoneNo;
                oldUser.EmailAddress = staff.EmailAddress;

                if (string.IsNullOrEmpty(staff.ProfilePicture))
                {
                    oldUser.ProfilePicture = null;
                }
                else
                {
                    oldUser.ProfilePicture = Convert.FromBase64String(staff.ProfilePicture);
                }

                var result = await _authService.UpdateUserAsync(oldUser);

                if (result)
                {
                    return Ok("User updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update user.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user: {Message}", ex.Message);
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }

        [HttpGet("userProfile")]
        public async Task<ActionResult<StaffDTO>> GetUserByUsernameAsync(string username)
        {
            var user = await _authService.GetUserByUsernameAsync(username);

            if (user == null)
                return NotFound();

            var profilePictureBase64 = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null;

            var staffDTO = new StaffDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                EmailAddress = user.EmailAddress,
                PhoneNo = user.PhoneNo,
                NIC = user.NIC,
                ProfilePicture = profilePictureBase64
            };

            return Ok(staffDTO);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("localStorageKey");

            return RedirectToAction("Login");
        }
    }
}