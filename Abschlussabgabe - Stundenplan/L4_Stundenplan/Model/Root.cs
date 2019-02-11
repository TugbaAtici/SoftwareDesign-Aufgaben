
using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    public class Root
    {
        [JsonProperty("courses")]
        public Courses Courses { get; private set; }

        [JsonProperty("rooms")]
        public List<Room> Rooms { get; private set; }

        [JsonProperty("professors")]
        public List<Professor> Professors { get; private set; }
    }
}
