using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    public class Root
    {
        // Kurse
        [JsonProperty("courses")]
        public Courses Courses { get; private set; }

        // Räume
        [JsonProperty("rooms")]
        public List<Room> Rooms { get; private set; }

        // Professoren
        [JsonProperty("professors")]
        public List<Professor> Professors { get; private set; }
    }
}
