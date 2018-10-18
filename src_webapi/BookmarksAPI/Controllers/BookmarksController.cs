namespace BookmarksAPI.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using BookmarksAPI.Models;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using EEFApps.ApiInstructions.DataInstructions.Exceptions;
    using EEFApps.ApiInstructions.DataInstructions.Instructions;
    using EEFApps.ApiInstructions.DataInstructions.Instructions.Structures;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/bookmarks")]
    public class BookmarksController : Controller
    {
        private readonly BookmarksDbContext context;

        public BookmarksController(BookmarksDbContext context)
        {
            this.context = context;
        }

        // GET: api/bookmarks/{bookmarkId}
        [HttpGet("{bookmarkId}")]
        public async Task<IActionResult> GetBookmark([FromRoute] string bookmarkId)
        {
            try
            {
                var bookmark = await new ReceivingUserConextedInstruction<Bookmark, string, string>(
                    this.context,
                    new ReceivingInstructionParams<string>
                    {
                        Id = bookmarkId
                    },
                    this.User.Identity.Name)
                    .Execute();

                var sanitizedBookmark = Mapper.Map<Bookmark, BookmarkViewModel>(bookmark);
                return Ok(sanitizedBookmark);
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }

        // POST: api/bookmarks
        [HttpPost]
        public async Task<IActionResult> CreateBookmark([FromBody]NewBookmarkViewModel newBookmark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: create a new instruction that check context of a parent entity CreationUserContextedInstruction
            try
            {
                var bookmarkEntity = Mapper.Map<NewBookmarkViewModel, Bookmark>(newBookmark);
                bookmarkEntity.UserId = this.User.Identity.Name;
                var createdBookmark = await new CreationInstruction<Bookmark>(this.context, bookmarkEntity).Execute();
                return CreatedAtAction("GetBookmark", new { bookmarkId = createdBookmark.Id }, Mapper.Map<Bookmark, BookmarkViewModel>(createdBookmark));
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }
    }
}
