namespace BaseSolution.Infrastructure.Extensions
{
    public static class EmailVerification
    {
        private static string Email { get; set; } = string.Empty;
        private static string CodeVerify { get; set; }

        private static DateTime TimeExpired { get; set; }

        public static bool SetCodeForEmail(string email, string code)
        {
            if (TimeExpired < DateTime.Now && Email.Equals(email) || !Email.Equals(email))
            {
                Email = email;
                CodeVerify = code;
                TimeExpired = DateTime.Now.AddMinutes(5);
                return true;
            }
            return false;
        }
        public static bool ConfirmCode(string code)
        {
            if (code.Equals(CodeVerify) && TimeExpired >= DateTime.Now)
            {
                Email = string.Empty;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
