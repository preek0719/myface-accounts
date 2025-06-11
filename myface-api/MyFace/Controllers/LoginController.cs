// using Microsoft.AspNetCore.Mvc;
// using MyFace.Models.Database;
// using MyFace.Models.Request;
// using MyFace.Models.Response;
// using MyFace.Repositories;

// namespace MyFace.Controllers
// {

//     [ApiController]
//     [Route("[controller]")]
//     public class LoginController : ControllerBase
//     {
//         private readonly IUsersRepo _usersRepo;

//         public LoginController(IUsersRepo usersRepo)
//         {
//             _usersRepo = usersRepo;
//         }


//         [HttpPost("/login")]
//         public IActionResult Login([FromBody] User user)
//         {

//         }



//     //   Header =  Basic dGVzdC11c2VyOnNlY3JldA==
//     // decode = 

//     // Pick an endpoint within the application to add our Basic Auth to.

// // Add code to read the Auth header from the request

// // Then decode the username and password

// // Next, find the user from the database.

// // Hash the password and compare it to what is in the Database.

// // If they are the same, continue with the request, otherwise, return a 401 response.

//     }
    
    