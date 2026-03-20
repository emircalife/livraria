using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using livraria.Controllers;

namespace livraria.Services
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly ConcurrentDictionary<Guid, Book> _store = new();

        public void Add(Book book) => _store[book.Id] = book;

        public List<Book> GetAll() => _store.Values.ToList();

        public Book? GetById(Guid id) => _store.TryGetValue(id, out var book) ? book : null;

        public bool Update(Book book)
        {
            if (!_store.ContainsKey(book.Id)) return false;
            _store[book.Id] = book;
            return true;
        }

        public bool Remove(Guid id) => _store.TryRemove(id, out _);

        public bool ExistsByTitleAndAuthor(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) return false;
            var t = title.Trim();
            var a = author.Trim();
            return _store.Values.Any(b =>
                string.Equals(b.Title?.Trim(), t, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(b.Author?.Trim(), a, StringComparison.OrdinalIgnoreCase));
        }
    }
}