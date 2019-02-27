using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Data
{
    public class WPV
    {
        [JsonProperty("name")]
        public string Name {get; private set;}

        [JsonProperty("professor")]
        public string Professor {get; private set;}

        [JsonProperty("description")]
        public string Description {get; private set;}

        [JsonProperty("equipment")]
        public List<string> Equipment {get; private set;}

        [JsonProperty("blocks")]
        public List<BlockDescription> BlockDescriptions { get; private set; }


        public override string ToString()
        {
            // Beispiel:
            /* Informatik Praktikum - Prof
               Benötigtes Equipment: PC
            */
            return $"{Name} - {Professor}\nBenötigtes Equipment: {string.Join(", ", Equipment)}";
        }
    }
}
