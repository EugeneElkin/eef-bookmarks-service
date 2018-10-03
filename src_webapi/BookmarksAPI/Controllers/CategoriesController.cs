﻿namespace BookmarksAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookmarksAPI.Exceptions;
    using BookmarksAPI.Models;
    using DataWorkShop;
    using DataWorkShop.Entities;
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
        public async Task<IActionResult> GetCategories([FromRoute] string categoryId, string orderByField = null, bool isDescending = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categories = await this.LoadCategoriesByLevel(categoryId, orderByField, isDescending);
                return Ok(categories);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/categories/root
        [HttpGet("root")]
        public async Task<IActionResult> GetCategories(string orderByField = null, bool isDescending = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categories = await this.LoadCategoriesByLevel(null, orderByField, isDescending);
                return Ok(categories);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                return CreatedAtAction("GetCategories", new { id = createdCategory.Id }, Mapper.Map<Category, CategoryViewModel>(createdCategory));
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PATCH: api/categories/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCategory([FromRoute] string id, [FromBody] JsonPatchDocument<Category> patchingCategory)
        {
            // TODO: check that category belongs to user
            await new PatchInstruction<Category, string>(this.context, id, patchingCategory).Execute();
            return NoContent();
        }


        private async Task<IEnumerable<CategoryViewModel>> LoadCategoriesByLevel(string level, string orderByField, bool isDescending)
        {
            var categories = await new ReceivingListInstruction<Category>(this.context,
               new ListInstructionParams<Category>
               {
                   orderByField = orderByField,
                   isDescending = isDescending,
                   filterExpr = (rec) => rec.UserId == this.User.Identity.Name && rec.ParentId == level
               }).Execute();

            var sanitizedCategories = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

            return sanitizedCategories;
        }

        //// PUT: api/categories/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/categories/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
