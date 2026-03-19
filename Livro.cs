using System;

namespace livraria.Controllers;

public class Livro
{
    public Guid Id { get; set; }        //Gerado automaticamente pelo sistema.
    public string Title { get; set; }   //Deve ter entre 2 e 120 caracteres.
    public string Author { get; set; }  //   Deve ter entre 2 e 120 caracteres.
    public GenreEnum Genre { get; set; }   //   Deve ser um dos valores válidos: ficção, romance, mistério, ....
    public decimal Price { get; set; }  //   Deve ser maior ou igual a 0.
    public int Stock { get; set; }      //  m Deve ser maior ou igual a 0.
}
