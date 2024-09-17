using ODataSample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataSample.Data.Context
{
    public class DataSource
    {
        private static IList<Book> _books {  get; set; }

        public static IList<Book> GetBooks()
        {
            if(_books != null)
            {
                return _books;
            }

            _books = new List<Book>();

            Book book = new Book
            {
                Id = 1,
                ISBN = "982-0-123-231223-1",
                Title = "Essential c#5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address { City = "HCM City", Street = "D2, Thu Duc District" },
                Press = new Press
                {
                    PressId = 1,
                    Name = "Addison-Wesley",
                    Email = "nam@gmail.com",
                    Category = Category.Book
                }
            };

            Book book1 = new Book
            {
                Id = 2,
                ISBN = "982-0-123-231223-1",
                Title = "Hello c#5.0",
                Author = "Michaelis",
                Price = 59.99m,
                Location = new Address { City = "HCM City", Street = "D2, Thu Duc District" },
                Press = new Press
                {
                    PressId = 2,
                    Name = "Addison-Wesley",
                    Email = "hung@gmail.com",
                    Category = Category.Book
                }
            };
            _books.Add(book);
            _books.Add(book1);
            return _books;
        }
    }
}
