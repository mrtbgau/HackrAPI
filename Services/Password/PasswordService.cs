using System.Text;

namespace API.Services.Password
{
    public class PasswordService(HashSet<string> commonPasswords) : IPasswordService
    {
        private readonly HashSet<string> _commonPasswords = commonPasswords;

        public PasswordService() : this(
            new HashSet<string>(
                File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Data", "10k-most-common.txt"))
                ? File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Data", "10k-most-common.txt"))
                : throw new FileNotFoundException($"The file {Path.Combine(Directory.GetCurrentDirectory(), "Data", "10k-most-common.txt")} was not found.")
            ))
        {
        }

        public async Task<bool> IsWeakPasswordAsync(string password)
        {
            return await Task.FromResult(_commonPasswords.Contains(password));
        }

        public string GenerateSecurePassword()
        {
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+[]{}|;:,.<>?/";

            var random = new Random();
            var password = new StringBuilder();

            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(lowerChars[random.Next(lowerChars.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            var allChars = upperChars + lowerChars + digits + specialChars;
            for (int i = 4; i < 12; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
        }
    }
}
