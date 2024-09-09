using System.ComponentModel.DataAnnotations;

public class ValidateVerificationCodeRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Code { get; set; }
}