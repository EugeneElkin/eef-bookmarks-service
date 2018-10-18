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
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly BookmarksDbContext context;

        public CategoriesController(BookmarksDbContext context)
        {
            this.context = context;
        }

        // GET: api/categories/{categoryId}
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory([FromRoute] string categoryId)
        {
            try
            {
                var category = await new ReceivingUserConextedInstruction<Category, string, string>(
                    this.context,
                    new ReceivingInstructionParams<string>
                    {
                        Id = categoryId,
                        NavigationProperties = new string[] {"Categories", "Bookmarks"}
                    },
                    this.User.Identity.Name)
                    .Execute();

                var sanitizedCategory = Mapper.Map<Category, CategoryViewModel>(category);
                return Ok(sanitizedCategory);
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }

        // GET: api/categories/root
        [HttpGet("root")]
        public async Task<IActionResult> GetRootCategories(string orderByField = null, bool isDescending = false)
        {
            try
            {
                var categories = await new ReceivingListInstruction<Category>(
                this.context,
                new ListInstructionParams<Category>
                {
                    OrderByField = orderByField,
                    IsDescending = isDescending,
                    FilterExpr = (rec) => rec.UserId == this.User.Identity.Name && rec.ParentId == null
                })
               .Execute();

                var sanitizedCategories = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);
                return Ok(sanitizedCategories);
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]NewCategoryViewModel newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categoryEntity = Mapper.Map<NewCategoryViewModel, Category>(newCategory);
                categoryEntity.UserId = this.User.Identity.Name;
                var createdCategory = await new CreationInstruction<Category>(this.context, categoryEntity).Execute();
                return CreatedAtAction("GetCategory", new { categoryId = createdCategory.Id }, Mapper.Map<Category, CategoryViewModel>(createdCategory));
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }

        // PATCH: api/categories/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCategory([FromRoute] string id, [FromBody] JsonPatchDocument<Category> patchingCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await new PatchUserContextedInstruction<Category, string, string>(
                    this.context,
                    new PatchInstructionParams<Category, string>
                    {
                        Id = id,
                        DeltaEntity = patchingCategory
                    },
                    this.User.Identity.Name)
                    .Execute();

                return NoContent();
            }
            catch (InstructionException ex)
            {
                return StatusCode((int)(ex.httpStatusCode), ex.Message);
            }
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await new RemovalRecursiveUserContextedInstruction<Category, string, string>(
                    this.context,
                    new RemovalInstructionParams<string>()
                    {
                        Id = id
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
