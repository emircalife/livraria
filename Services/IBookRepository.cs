using System;
using System.Collections.Generic;
using livraria.Controllers;
using livraria.Entity;

namespace livraria.Services
{
    public interface IBookRepository
    {
        void Add(Book book);
        List<Book> GetAll();
        Book? GetById(Guid id);
        bool Update(Book book);
        bool Remove(Guid id);
        bool ExistsByTitleAndAuthor(string title, string author);
    }
}