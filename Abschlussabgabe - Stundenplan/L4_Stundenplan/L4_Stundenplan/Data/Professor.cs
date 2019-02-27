using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Data
{
    public class Professor
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("email")]
        public string Email {get; private set;}

        [JsonProperty("courses")]
        public List<string> Courses { get; private set; }

        [JsonProperty("unavailable")]
        public List<BlockDescription> Unavailable { get; private set; }
    }
}
