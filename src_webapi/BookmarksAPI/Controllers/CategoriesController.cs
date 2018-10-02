﻿namespace BookmarksAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookmarksAPI.Models;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using EEFApps.ApiInstructions.DataInstructions.Instructions;
    using EEFApps.ApiInstructions.DataInstructions.Instructions.Structures;
    using Microsoft.AspNetCore.Authorization;
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

        // GET: api/Categories
        [HttpGet("root")]
        public async Task<IActionResult> GetRootCategories(string orderByField = null, bool isDescending = false)
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
                    filterExpr = (rec) => rec.UserId == this.User.Identity.Name && rec.Parent == null,
                    navigationProperties = new string[] { "Categories", "Bookmarks" }
                }).Execute();

            // TODO: think about loading modes: complete load or by pieces

            var sanitizedCategories = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

            return Ok(sanitizedCategories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
            }

            base.Dispose(disposing);
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
