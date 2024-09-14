using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataSample.Data.Context;
using ODataSample.Data.DTO;
using ODataSample.Data.Models;
namespace TestOData.Controllers
{
    [ODataAttributeRouting]
    public class PressesController : ODataController
    {
        private BookStoreContext _context;
        /*private readonly IGenericRepository<Book> _repository;
        private readonly IGenericRepository<Press> _pressRepository;*/
        public PressesController(BookStoreContext storeContext)
        {
            _context = storeContext;
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
            /*_repository = repository;
            _pressRepository = pressRepository;
            if (repository.Get().Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    repository.Insert(b);
                    pressRepository.Insert(b.Press);
                }
                repository.Save();
                pressRepository.Save();
            }*/
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Presses);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] PressDTO press)
        {
            if (press == null)
            {
                return BadRequest();
            }

            Press p = new Press()
            {
                Id = press.Id,
                Email = press.Email,
                Category = press.Category,
                Name = press.Name,
            };
            _context.Presses.Add(p);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = press.Id }, press);
        }

    }
}
