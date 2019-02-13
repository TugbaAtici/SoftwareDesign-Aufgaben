using System.IO;

using Newtonsoft.Json;

using L4_Stundenplan.Model;


namespace L4_Stundenplan
{
    public static class JSON
    {
        public static Root Root { get; private set; }


        public static void Deserialize()
        {
            Root = JsonConvert.DeserializeObject<Root>(File.ReadAllText("data.json"));
        }
    }
}
