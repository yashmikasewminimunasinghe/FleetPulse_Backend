using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace FleetPulse_BackEndDevelopment.Utilities
{
    public static class FirebaseInitializer
    {
        public static void InitializeFirebase()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("Configuration/serviceAccountKey.json")
                });

                Console.WriteLine("Firebase has been initialized.");
            }
        }
    }
}