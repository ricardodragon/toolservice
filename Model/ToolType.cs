using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace toolservice.Model
{
    public class ToolType
    {
        public int id{get;set;}
        [MaxLength(50)]
        public string name{get;set;}
        [MaxLength(100)]
        public string description{get;set;}
        [Column("thingGroupIds", TypeName = "integer[]")]
        public int[] thingGroupIds{get;set;}
        [Required]
        public string status{get;set;}

    }
}