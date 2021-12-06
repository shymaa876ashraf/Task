using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.DAL.DbModels
{
    [Table("Category",Schema ="Task")]
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
