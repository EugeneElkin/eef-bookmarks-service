namespace BookmarksAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/bookmarks")]
    public class BookmarksController : Controller
    {
    }
}
