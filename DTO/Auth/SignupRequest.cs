    namespace PhoenicianCapital.DTO.Auth
    {
        public record SignupRequest(
            string Email,
            string Password,
            string ConfirmPassword
        );
    }
