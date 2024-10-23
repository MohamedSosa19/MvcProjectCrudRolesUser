using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
       
        public string Name { get; set; }

        public int? Age { get; set; }

        //[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
        //    ErrorMessage ="Address Musr be like 123-street-city-country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

       
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string ImageName { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
