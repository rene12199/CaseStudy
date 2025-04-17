using CaseStudy.Api.DTOs;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IConverter<CategoryDto, Category> _converter;

    public CategoryController(ICategoryService categoryService, IConverter<CategoryDto, Category> converter)
    {
        _categoryService = categoryService;
        _converter = converter;

    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllCategories()
    {
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        var categories = _categoryService.GetAllCategories();

        //https://github.com/dotnet/aspnetcore/issues/58949
        return Ok(categories.Select(f => _converter.ReverseConvert(f)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateCategory(CategoryDto transactionDto)
    {
        // if(invalid)
        // {
        //     return BadRequest();
        // }
        _categoryService.CreateCategory(_converter.Convert(transactionDto));

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

        //https://github.com/dotnet/aspnetcore/issues/58949
        return Created();
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

        //https://github.com/dotnet/aspnetcore/issues/58949
        return Created();
    }
}