using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDemo.Api.Infrastructure;
using BlazorDemo.Model.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDemo.Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController
        : Controller
    {
        private readonly BlazorDemoContext Context;
        public BookController(BlazorDemoContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IList<Book> Get()
        {
            return Context.Books.ToList();
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return Context.Books.Where(book => book.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public async Task<Book> Post([FromBody] Book value)
        {
            var bookEntity = await Context.AddAsync(value);
            await Context.SaveChangesAsync();
            return bookEntity.Entity;
        }

        [HttpPut("{id}")]
        public async Task<Book> Put(int id, [FromBody] Book value)
        {
            var bookEntity = Context.Books.Where(book => book.Id == id).FirstOrDefault();
            bookEntity.ISBN = value.ISBN;
            bookEntity.PublisherName = value.PublisherName;
            bookEntity.BookTitle = value.BookTitle;
            await Context.SaveChangesAsync();
            return bookEntity;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var bookEntity = Context.Books.Where(book => book.Id == id).FirstOrDefault();
            Context.Remove(bookEntity);
            await Context.SaveChangesAsync();
        }
    }
}
