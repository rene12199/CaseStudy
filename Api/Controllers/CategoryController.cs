using CaseStudy.Api.Converter;
using CaseStudy.Api.DTOs;
using CaseStudy.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ICategoryConverter _converter;

    public CategoryController(ICategoryService categoryService, ICategoryConverter converter)
    {
        _categoryService = categoryService;
        _converter = converter;

    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<CategoryDto>), StatusCodes.Status200OK)]
    public IActionResult GetAllCategories()
    {
        var categories = _categoryService.GetAllCategories();

        return Ok(categories.Select(f => _converter.Convert(f)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateCategory(CategoryDto categoryDto)
    {
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        _categoryService.CreateCategory(_converter.Convert(categoryDto));

        //https://github.com/dotnet/aspnetcore/issues/58949
        return Created();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateCategory(CategoryDto transactionDto)
    {
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        _categoryService.UpdateCategory(_converter.Convert(transactionDto));

        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(Guid transactionId)
    {
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        _categoryService.DeleteCategory(transactionId);

        return Ok();
    }
}