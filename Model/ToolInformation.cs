using System.Collections.Generic;

namespace toolservice.Model
{
    public class ToolInformation
    {
        public int toolInformationId{get;set;}
        public long datetime{get;set;}
        public List<ToolInformationAdditional> informationAdditional{get;set;}
    }
}