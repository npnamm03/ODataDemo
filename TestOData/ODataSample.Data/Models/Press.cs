using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ODataSample.Data.Models
{
    public class Press
    {
        public int PressId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Category Category { get; set; }
    }
}
