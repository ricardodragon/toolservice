using System.Collections.Generic;

namespace toolservice.Model
{
    public class ThingGroup
    {
        public int thingGroupId { get; set; }
        public string groupName { get; set; }
        public string groupCode { get; set; }
        public ICollection<Thing> things { get; set; }
    }
}