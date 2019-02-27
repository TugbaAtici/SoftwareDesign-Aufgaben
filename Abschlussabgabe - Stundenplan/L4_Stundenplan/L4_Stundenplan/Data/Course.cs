using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Data
{
    public class Course
    {
        [JsonProperty("name")]
        public string Name {get; private set;}

        [JsonProperty("professor")]
        public string Professor {get; private set;}

        [JsonProperty("cohorts")]
        public List<string> Cohorts {get; private set;}

        [JsonProperty("blocks")]
        public int Blocks {get; private set;}

        [JsonProperty("equipment")]
        public List<string> Equipment {get; private set;}

        [JsonProperty("semester")]
        public int Semester {get; private set;}


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
