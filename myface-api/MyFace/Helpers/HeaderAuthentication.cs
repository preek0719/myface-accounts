using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using MyFace.Helpers;
using MyFace.Migrations;
using MyFace.Models.Database;
using MyFace.Repositories;
using SQLitePCL;

namespace MyFace.Helpers
{
    public class HeaderAuthentication
    {
        private readonly RequestDelegate _next;

        public HeaderAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUsersRepo usersRepo)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header missing.");
                return;
            }

            var authHeaderValues = context.Request.Headers["Authorization"].ToString();

            if (authHeaderValues.Length == 0)
            {
                await context.Response.WriteAsync("Authorization header missing line 38.");
                return;
            }

            if (authHeaderValues.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                var authHeader = authHeaderValues.ToString();

                string encoded = authHeader.Substring("Basic ".Length).Trim();

                byte[] binaryData = System.Convert.FromBase64String(encoded);
                string decodedHeader = System.Text.Encoding.UTF8.GetString(binaryData);

                var parts = decodedHeader.Split(':');
                string username = parts[0];
                string password = parts[1];

                User user = usersRepo.GetByUsername(username);

                if (user != null)
                {
                    Console.WriteLine("test51");
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
                        await _next(context);
                        return;
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Invalid credentials.");
                    }
                }
            }
            await context.Response.WriteAsync("Invalid authorization header");
        }
    }
}
