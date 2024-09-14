using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataSample.Data.Context;
using ODataSample.Data.Models;
using ODataSample.Repository;
using ODataSample.Data.DTO;

namespace TestOData.Controllers
{
    [ODataAttributeRouting] //<- Make sure it is there
    public class BooksController : ODataController
    {
        private readonly BookStoreContext _storeContext;
        private readonly IGenericRepository<Book> _repository;
        private readonly IGenericRepository<Press> _pressRepository;
        public BooksController(BookStoreContext storeContext, IGenericRepository<Book> repository, IGenericRepository<Press> pressRepository)
        {
            _storeContext = storeContext;
            _repository = repository;
            _pressRepository = pressRepository;
            if (storeContext.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    /*repository.Insert(b);
                    pressRepository.Insert(b.Press);*/
                    storeContext.Books.Add(b);
                    storeContext.Presses.Add(b.Press);
                }
                /*repository.Save();
                pressRepository.Save();*/
                storeContext.SaveChanges();
            }
            
        }

        [EnableQuery(PageSize = 1)]
        public IActionResult Get()
        {
            //return Ok(_repository.Get(includeProperties: "Press"));
            return Ok(_storeContext.Books);
        }

        [EnableQuery]
        public IActionResult Get(int key, string version)
        {
            //return Ok(_repository.Get(filter: c => c.Id == key, includeProperties: "Press").FirstOrDefault());
            return Ok(_storeContext.Books.FirstOrDefault(c => c.Id == key));
        }

        public IActionResult Post([FromBody] BookDTO b)
        {
            /*_pressRepository.Insert(b.Press);
            _repository.Insert(b);
            _repository.Save();*/

            Book book = new Book()
            {
                Id = b.Id,
                Author = b.Author,
                ISBN = b.ISBN,
                Location = b.Location,
                Price = b.Price,
                Title = b.Title,
                Press = b.Press,
            };

            _storeContext.Books.Add(book);
            _storeContext.SaveChanges();
            return Created(book);
        }

        [EnableQuery]
        [HttpDelete("DeleteHello")]
        public IActionResult Delete( int bId)
        {
            var b = _storeContext.Books.FirstOrDefault(d => d.Id == bId);
            _storeContext.Books.Remove(b);
            _storeContext.SaveChanges();
            return Ok();
        }
    }
}
