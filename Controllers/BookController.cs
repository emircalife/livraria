using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using livraria.Services;
using livraria.Communication.Requests;
using livraria.Communication.Responses;
using livraria.Entity;

namespace livraria.Controllers;

public class BookController : BaseController
{
    private readonly IBookRepository _repo;

    public BookController(IBookRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult GetById(string id)
    {
        if (!Guid.TryParse(id, out var guid)) return BadRequest("Invalid id");
        var book = _repo.GetById(guid);
        return Ok(book ?? new Book());
    }

    [HttpPost]
    [ProducesResponseType(typeof(RequestRegistrationBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Post([FromBody] RequestRegistrationBookJson request)
    {
        if (request == null) 
            return BadRequest("Request body is required.");

        if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Author))
            return BadRequest("Title and Author are required.");

        if (_repo.ExistsByTitleAndAuthor(request.Title, request.Author))
            return Conflict("A book with the same title and author already exists.");

        if (request.Stock < 0)
            return BadRequest("Stock can't be nagative.");

        if (request.Price < 0)
            return BadRequest("Price can't be nagative.");

        bool existe = Enum.TryParse<GenreEnum>(request.Genre.ToString(), true, out _);
        if (!existe)
            return BadRequest("Invalid genre.");

        var response = new ResponseRegistrationBookJson
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Stock = request.Stock
        };

        var book = new Book
        {
            Id = response.Id,
            Title = response.Title,
            Author = response.Author,
            Genre = response.Genre,
            Price = response.Price,
            Stock = response.Stock
        };

        _repo.Add(book);

        return Created(string.Empty, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update(Guid id, [FromBody] RequestUpdateBookJson request)
    {
        var book = new Book
        {
            Id = id,
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Stock = request.Stock
        };

        if (!_repo.Update(book)) 
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(Guid id)
    {
        if (_repo.Remove(id)) 
            return NoContent();

        return NotFound();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_repo.GetAll());
    }
}