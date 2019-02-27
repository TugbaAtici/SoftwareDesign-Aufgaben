using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Data
{
    public class Room
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("capacity")]
        public int Capacity { get; private set; }

        [JsonProperty("equipment")]
        public List<string> Equipment { get; private set; }


        public override string ToString()
        {
            return $"{Name}\nPlätze: {Capacity}\nRaumequipment: {string.Join(", ", Equipment)}";
        }
    }
}
