using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace toolservice.Model
{    
    public class StateConfiguration
    {
        public ICollection<ConfiguredState> states { get; set; }
    }
}