using System.Security.Cryptography;

namespace Kafo.Application.Utilities;

public static class PasswordHashUtility
{
    /// <summary>
    /// Generates salt and hash for password.
    /// </summary>
    /// <param name="password"></param>
    /// <returns>Password hash and generated salt.</returns>
    public static HashSalt GenerateSaltedHash(string password, int size = 64)
    {
        var saltBytes = new byte[size];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetNonZeroBytes(saltBytes);
        }

        var salt = Convert.ToBase64String(saltBytes);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA512);
        var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

        var hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
        return hashSalt;
    }

    /// <summary>
    /// Computes hash of salted password and compares it to stored value.
    /// </summary>
    /// <param name="enteredPassword">Entered password in clear text.</param>
    /// <param name="storedHash">Stored hash of user password.</param>
    /// <param name="storedSalt">Stored salt used in user password.</param>
    /// <returns>If entered password is equal to stored.</returns>
    public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000, HashAlgorithmName.SHA512);

        return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
    }
}

public class HashSalt
{
    public required string Hash { get; set; }
    public required string Salt { get; set; }
}