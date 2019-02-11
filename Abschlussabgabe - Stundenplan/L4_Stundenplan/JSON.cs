
using System.IO;

using Newtonsoft.Json;

using L4_Stundenplan.Model;


namespace L4_Stundenplan
{
    public static class JSON
    {
        public static Root Root
        {
            get
            {
                return JsonConvert.DeserializeObject<Root>(File.ReadAllText("data.json"));
            }
        }
    }
}
