using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Data
{
    public class Cohort
    {
        [JsonProperty("name")]
        public string Name {get; private set;}

        [JsonProperty("semester")]
        public int Semester {get; private set;}

        [JsonProperty("students")]
        public int Students {get; private set;}
    }
}
