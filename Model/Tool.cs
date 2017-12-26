using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace toolservice.Model
{
    public class Tool
    {
        public int id{get;set;}
        [MaxLength(50)]
        public string name{get;set;}
        [MaxLength(100)]
        public string description{get;set;}
        [Required]
        public double lifeCycle{get;set;}
        public double currentLife{get;set;}
        [Required]
        public string unitOfMeasurement{get;set;}
        [Required]
        public int typeId{get;set;}
        [Required]
        public string status{get;set;}
    }
}