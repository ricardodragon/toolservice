using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace toolservice.Model
{
    public class Tool
    {
        public int id{get;set;}
        [MaxLength(50)]
        public string name{get;set;}
        [MaxLength(100)]
        public string description{get;set;}
        [MaxLength(100)]
        public string serialNumber{get;set;}
        [MaxLength(100)]
        public string code{get;set;}
        [Required]
        public double lifeCycle{get;set;}
        public double currentLife{get;set;}
        [Required]
        public string unitOfMeasurement{get;set;}
        [Required]
        [ToolTypeValidation]
        public int? typeId{get;set;}
        [NotMapped]
        public string typeName{get;set;}
        [Required]
        public string status{get;set;}
    }
}