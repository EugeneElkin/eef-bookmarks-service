namespace BookmarksAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApiInstructions.DataInstructions.Instructions;
    using ApiInstructions.DataInstructions.Instructions.Structures;
    using AutoMapper;
    using BookmarksAPI.Models;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        private readonly BookmarksDbContext context;

        public CategoriesController(BookmarksDbContext context)
        {
            this.context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories(string orderByField = null, bool isDescending = false, int? pageSize = null, int? pageAt = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = await new ReceivingListInstruction<Category>(this.context,
                new ListInstructionParams<Category>
                {
                    orderByField = orderByField,
                    isDescending = isDescending,
                    pageAt = pageAt,
                    pageSize = pageSize
                }).Execute();

            var sanitizedCategories = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

            return Ok(sanitizedCategories);
        }

        //// GET: api/Categories/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Categories
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Categories/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
