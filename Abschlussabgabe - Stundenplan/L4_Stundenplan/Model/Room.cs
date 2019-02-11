
using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    public class RoomSummary
    {
        public string Time { get; set; }
        public int Day { get; set; }

        public bool IsSame(RoomSummary other)
        {
            return Time == other.Time && Day == other.Day;
        }
    }

    public class RoomItem
    {
        // Beamer, overhead projector, ...
        [JsonProperty("type")]
        public string Type { get; private set; }

        // how many of the above are present in the room?
        [JsonProperty("amount")]
        public int Amount { get; private set; }
    }
    
    public class Room
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("capacity")]
        public int Capacity { get; private set; }

        [JsonProperty("equipment")]
        public List<RoomItem> Equipment { get; private set; } = new List<RoomItem>();
    }
}
