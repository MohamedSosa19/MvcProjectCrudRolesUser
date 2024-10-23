using System.ComponentModel.DataAnnotations;
using System;
using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {

        public int id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Range(20, 33)]
        public int? Age { get; set; }

        //[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
        //    ErrorMessage ="Address Musr be like 123-street-city-country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Range(5000, 10000)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public DateTime HiringDate { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }


    }
}
