﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


namespace PasswordHasherApp
{
    public class PasswordHasher : IPasswordHasher 
    {
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit
        private HashingOptions Options { get; }

        

        public PasswordHasher()
        {
            Options = new HashingOptions();
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
             password,
             SaltSize,
             Options.Iterations,
             HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Options.Iterations}.{salt}.{key}";
            }
        }

        //Checks if it is the right password

        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
        {
            try
            {
                var parts = hash.Split('.', 3);

                if (parts.Length != 3)
                {
                    return (false, false);
                    throw new FormatException("Unexpected hash format. " +
                      "Should be formatted as `{iterations}.{salt}.{hash}`");
                }

                var iterations = Convert.ToInt32(parts[0]);
                var salt = Convert.FromBase64String(parts[1]);
                var key = Convert.FromBase64String(parts[2]);

                var needsUpgrade = iterations != Options.Iterations;

                using (var algorithm = new Rfc2898DeriveBytes(
                  password,
                  salt,
                  iterations,
                  HashAlgorithmName.SHA512))
                {
                    var keyToCheck = algorithm.GetBytes(KeySize);

                    var verified = keyToCheck.SequenceEqual(key);

                    return (verified, needsUpgrade);
                }
            }
            catch
            {
                return (false, false);
            }

        }
    }

    
}
