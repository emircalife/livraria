using Microsoft.AspNetCore.Mvc;
using livraria.Communication.Requests;
using livraria.Communication.Responses;

namespace livraria.Controllers;

public class LivroController : BaseController
{
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult GetById(int id)
    {
        var response = new Livro();

        response.Id = Guid.NewGuid();   //.ToString("N")    // Sem hífens
        response.Title = "Bíblia";
        response.Author = "Emir Calife";
        response.Genre = GenreEnum.Religioso;
        response.Price = 100.00m;
        response.Stock = 10;

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RequestRegistrationLivroJson), StatusCodes.Status201Created)]
    public IActionResult Post([FromBody] RequestRegistrationLivroJson request)
    {
        var response = new ResponseRegistrationLivroJson();

        response.Id = 1;
        response.Name = request.Name;

        return Created(String.Empty, response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update([FromBody] RequestUpdateLivroJson request)
    {
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete()
    {
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Livro>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var response = new List<Livro>()
        {
            new Livro { Id = Guid.NewGuid(), Title = "Bíblia", Author = "Emir Calife", Genre = GenreEnum.Religioso, Price = 100.00m, Stock = 10 },
            new Livro { Id = Guid.NewGuid(), Title = "Me ensina a orar", Author = "Hellen Reis", Genre = GenreEnum.Religioso, Price = 55.00m, Stock = 50 }
        };

        return Ok(response);
    }
}