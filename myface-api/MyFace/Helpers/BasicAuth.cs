using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using MyFace.Helpers;
using MyFace.Migrations;
using MyFace.Models.Database;
using MyFace.Repositories;
using SQLitePCL;

namespace MyFace.Helpers
{
    public class AuthorizationHeaderReader
    {
        public static bool AuthenticateUser(HttpRequest request, IUsersRepo usersRepo)
        {
            var authHeaderValues = request.Headers["Authorization"];

            if (authHeaderValues.Count == 0)
            {
                return false;
            }

            var authHeader = authHeaderValues.ToString();
            {
                string encoded = authHeader.Substring("Basic ".Length - 1).Trim();

                byte[] binaryData = System.Convert.FromBase64String(encoded);
                string decodedHeader = System.Text.Encoding.UTF8.GetString(binaryData);

                var parts = decodedHeader.Split(':');
                string username = parts[0];
                string password = parts[1];

                User user = usersRepo.GetByUsername(username);
                if (user != null)
                {
                    var userSalt = user.Salt;
                    string hashedUserInput = Convert.ToBase64String(
                        KeyDerivation.Pbkdf2(
                            password: password,
                            salt: userSalt,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8
                        )
                    );

                    if (hashedUserInput == user.HashedPassword)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
