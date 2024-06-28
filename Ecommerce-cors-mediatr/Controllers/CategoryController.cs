using Application.Common.Catalogue.Commands;
using Application.Common.Catalogue.Commands.DeleteCategory;
using Application.Common.Catalogue.Queries;
using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Ecommerce_cors_mediatr.Controllers
{
    public class CategoryController : BaseApiController
    {
        /// <summary>
        /// Creates a new Category record.
        /// </summary>
        /// <response code="201">Recipe created.</response>
        /// <response code="400">Recipe has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Recipe.</response>
        [ProducesResponseType(typeof(Response<CategoryDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody]CreateCategoryDto createCategoryDto)
        {
            var command = new CreateCategoryCommand(createCategoryDto);
            var commandResponse = await Mediatr.Send(command);

            var response = new Response<CategoryDto>(commandResponse);
            return CreatedAtRoute("GetCategory", new { id = commandResponse.Id }, response);
        }

        /// <summary>
        /// Updates a Category record.
        /// </summary>
        /// <response code="201">Recipe created.</response>
        /// <response code="400">Recipe has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Recipe.</response>
        [ProducesResponseType(typeof(Response<CategoryDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, UpdateCategoryDto category)
        {
            var command = new UpdateCategoryCommand(id, category);
            await Mediatr.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Creates a new Category record.
        /// </summary>
        /// <response code="201">Recipe created.</response>
        /// <response code="400">Recipe has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Recipe.</response>
        [ProducesResponseType(typeof(Response<CategoryDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpDelete]
        public async Task<ActionResult<CategoryDto>> DeleteCategory(Guid id)
        {
            var command = new DeleteCategoryCommand(id);
            await Mediatr.Send(command);
            return NoContent();
        }

        



        /// <summary>
        /// Gets a single Category by ID.
        /// </summary>
        /// <response code="200">Recipe record returned successfully.</response>
        /// <response code="400">Recipe has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while Getting the Category.</response>
        [ProducesResponseType(typeof(Response<CategoryDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [Produces("application/json")]
        [OpenApiOperation("Get category", "")]
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            var query = new GetCategory(id);
            var queryResponse = await Mediatr.Send(query);

            var response = new Response<CategoryDto>(queryResponse);
            return Ok(response);

        }




    }
}
