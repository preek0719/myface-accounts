using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using MyFace.Helpers;
using MyFace.Models.Database;
using MyFace.Repositories;
using SQLitePCL;

namespace MyFace.Helpers
{
    public class AuthorizationHeaderReader 
    {
        // private IUsersRepo _iUsersRepo;

        // public AuthorizationHeaderReader(IUsersRepo iUsersRepo)
        // {
        //     _iUsersRepo = iUsersRepo;
        // }
       
       
        public static string GetAuthorizationHeader(HttpRequest request, IUsersRepo usersRepo)

        {

            var authHeaderValues = request.Headers["Authorization"];

            if (authHeaderValues.Count == 0)
            {
                return null;
            }

            var authHeader = authHeaderValues.ToString();
            {
                string encoded = authHeader.Substring("Basic ".Length - 1).Trim();

                byte[] binaryData = System.Convert.FromBase64String(encoded);

                string decodedHeader = System.Text.Encoding.UTF8.GetString(binaryData);

                var parts = decodedHeader.Split(':');
                string username = parts[0];

                usersRepo.GetByUsername(username);

                PasswordHelper.CreatePasswordHash(decodedHeader.Split(':')[1], out byte[] Salt, out string HashedPassword);

                Console.WriteLine(decodedHeader);
                Console.WriteLine(HashedPassword);
                return decodedHeader;
            }

        }

        public static void ValidatePassword(string userDetails)
        {
           
        }
    }
    

}