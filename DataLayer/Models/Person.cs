using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

    }
}
