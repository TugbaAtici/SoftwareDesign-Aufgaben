using Newtonsoft.Json;


namespace L4_Stundenplan.Data
{
    public class BlockDescription
    {
        [JsonProperty("day")]
        public int Day { get; private set; }

        [JsonProperty("block")]
        public int Block {get; private set; }
    }
}