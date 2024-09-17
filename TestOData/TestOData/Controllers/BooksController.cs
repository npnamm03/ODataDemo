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
        private readonly IGenericRepository<Book> _repository;
        private readonly IGenericRepository<Press> _pressRepository;
        public BooksController(IGenericRepository<Book> repository, IGenericRepository<Press> pressRepository)
        {
            _repository = repository;
            _pressRepository = pressRepository;
            if (_repository.Get().Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    repository.Insert(b);
                    pressRepository.Insert(b.Press);
                }
                repository.Save();
                pressRepository.Save();
            }
            
        }

        [EnableQuery(PageSize = 1)]
        public IActionResult Get()
        {
            return Ok(_repository.Get(includeProperties: "Press"));
        }

        [EnableQuery]
        public IActionResult Get(int key, string version)
        {
            return Ok(_repository.Get(filter: c => c.Id == key, includeProperties: "Press").FirstOrDefault());
        }

        public IActionResult Post([FromBody] BookDTO b)
        {
            
            Book book = new Book()
            {
                Author = b.Author,
                ISBN = b.ISBN,
                Location = b.Location,
                Price = b.Price,
                Title = b.Title,
                Press = b.Press,
            };

            _repository.Insert(book);
            _repository.Save();

            return Created(book);
        }

        [EnableQuery]
        [HttpDelete("DeleteHello")]
        public IActionResult Delete( int bId)
        {
            _repository.Delete(bId);
            _repository.Save();

            return Ok();
        }
    }
}
