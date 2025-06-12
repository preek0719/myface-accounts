// using Microsoft.AspNetCore.Mvc;
// using MyFace.Models.Database;
// using MyFace.Models.Request;
// using MyFace.Models.Response;
// using MyFace.Repositories;

// namespace MyFace.Controllers
// {
//     [ApiController]
//     [Route("/Login")]
//     public class LoginController : ControllerBase
//     {
//         private readonly IUsersRepo _users;

//         public UsersController(IUsersRepo users)
//         {
//             _users = users;
//         }

//         [HttpGet("{id}")]
//         public ActionResult<PostResponse> GetById([FromRoute] int id)
//         {
//             var post = _posts.GetById(id);
//             return new PostResponse(post);
//         }
//     }
// }
