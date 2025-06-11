// using System;
// using System.Reflection.Metadata;
// using System.Security.Cryptography;
// using Microsoft.AspNetCore.Cryptography.KeyDerivation;
// using MyFace.Repositories;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.Extensions.Options;
// using Microsoft.Extensions.Logging;
// using System.Net.Http.Headers;
// using System.Security.Claims;
// using System.Text;
// using System.Text.Encodings.Web;
// using System.Threading.Tasks;

// namespace MyFace.Helpers
// {
//     public class BasicAuthentication : AuthenticationHandler<AuthenticationSchemeOptions>

//     {
//         private readonly IUserRepository _userRepository;

//         public BasicAuthenticationHandler(
//         IOptionsMonitor<AuthenticationSchemeOptions> options,
//         ILoggerFactory logger,
//         UrlEncoder encoder,
//         IUserRepository userRepository)
//         : base(options, logger, encoder)
//         {
//             _userRepository = userRepository;
//         }

//         protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//         {
//             if (!Request.Headers.ContainsKey("Authorization"))
//             {
//                 return AuthenticateResult.Fail("Missing Authorization Header");
//             }

//             User? user;
//             try
//             {
//                 var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
//                 var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
//                 var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
//                 if (credentials.Length != 2)
//                 {
//                     return AuthenticateResult.Fail("Invalid Authorization Header Content");
//                 }
//                 var username = credentials[0];
//                 var password = credentials[1];
//                 user = await _userRepository.ValidateUser(username, password);
//             }
//             catch
//             {
//                 return AuthenticateResult.Fail("Invalid Authorization Header");
//             }

//             if (user == null)
//             {
//                 return AuthenticateResult.Fail("Invalid Username or Password");
//             }

//             var claims = new[] {
//             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//             new Claim(ClaimTypes.Name, user.Username),
// };
//             var identity = new ClaimsIdentity(claims, Scheme.Name);
//             var principal = new ClaimsPrincipal(identity);
//             var ticket = new AuthenticationTicket(principal, Scheme.Name);
//             return AuthenticateResult.Success(ticket);



//         }
//     }

