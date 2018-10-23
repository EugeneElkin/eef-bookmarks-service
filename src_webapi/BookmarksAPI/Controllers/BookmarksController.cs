namespace BookmarksAPI.Controllers
{
    using System.Collections.Generic;
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
                var bookmark = await new ReceivingUserContextedInstruction<Bookmark, string, string>(
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

        // GET: api/bookmarks/root
        [HttpGet("root")]
        public async Task<IActionResult> GetRootBookmarks(string orderByField = null, bool isDescending = false)
        {
            try
            {
                var bookmarks = await new ReceivingListInstruction<Bookmark>(
                this.context,
                new ListInstructionParams<Bookmark>
                {
                    OrderByField = orderByField,
                    IsDescending = isDescending,
                    FilterExpr = (rec) => rec.UserId == this.User.Identity.Name && rec.CategoryId == null
                })
               .Execute();

                var sanitizedBookmarks = Mapper.Map<IEnumerable<Bookmark>, IEnumerable<BookmarkViewModel>>(bookmarks);
                return Ok(sanitizedBookmarks);
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

            try
            {
                var bookmarkEntity = Mapper.Map<NewBookmarkViewModel, Bookmark>(newBookmark);
                bookmarkEntity.UserId = this.User.Identity.Name;
                var createdBookmark = await new CreationUserContextedInstruction<Bookmark, Category, string, string>(
                    this.context,
                    bookmarkEntity,
                    bookmarkEntity.CategoryId,
                    this.User.Identity.Name).Execute();
                return CreatedAtAction("GetBookmark", new { bookmarkId = createdBookmark.Id }, Mapper.Map<Bookmark, BookmarkViewModel>(createdBookmark));
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }

        // DELETE: api/bookmarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, string rowVersion)
        {
            try
            {
                await new RemovalOptimizedUserContextedInstruction<Bookmark, string, string>(
                    this.context,
                    new RemovalInstructionParams<string>()
                    {
                        Id = id,
                        Base64RowVersion = rowVersion
                    },
                    this.User.Identity.Name)
                    .Execute();

                return Ok();
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }
    }
}
