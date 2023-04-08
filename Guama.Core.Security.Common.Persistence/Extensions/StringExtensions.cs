namespace MOROPO.Framework.Guama.Core.Security.Common.Persistence.Extensions
{
    public static class StringExtensions
    {
        public static string ToHashedPassword(this string plainPassword)
        {
            var hasher = System.Security.Cryptography.SHA256.Create();

            var originalBytes = System.Text.Encoding.ASCII.GetBytes(plainPassword);

            var encodedBytes = hasher.ComputeHash(originalBytes);

            return System.BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}