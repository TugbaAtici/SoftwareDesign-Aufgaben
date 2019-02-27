using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;


namespace L4_Stundenplan.Data
{
    public class TimetableData
    {
        // Kurse
        [JsonProperty("cohorts")]
        public List<Cohort> Cohorts { get; private set; }

        [JsonProperty("courses")]
        public List<Course> Courses { get; private set; }

        [JsonProperty("WPV")]
        public List<WPV> WPV {get; private set;}

        [JsonProperty("professors")]
        public List<Professor> Professors { get; private set; }

        [JsonProperty("rooms")]
        public List<Room> Rooms { get; private set; }

        [JsonProperty("blocks")]
        public List<string> BlockStrings { get; set; }

        public List<Block> Blocks
        {
            get { return Block.ConvertBlockStrings(BlockStrings); }
        }
    }
}
