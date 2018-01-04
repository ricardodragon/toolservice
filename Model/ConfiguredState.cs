namespace toolservice.Model
{
    public class ConfiguredState
    {
        public string state { get; set; }
        public string[] possibleNextStates { get; set; }
        public bool needsJustification{get;set;}
    }
}