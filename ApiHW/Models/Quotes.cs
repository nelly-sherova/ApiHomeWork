using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHW.Models
{
    public class Quotes
    {
       
        public int Id { get; set; }
      
        public string Text { get; set; }
        public string Author { get; set; }
      
        public DateTime InsertDate  { get; set; }
    }
}
