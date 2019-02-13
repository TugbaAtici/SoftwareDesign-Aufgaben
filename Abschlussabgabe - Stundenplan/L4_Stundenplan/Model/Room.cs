using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    // Ausstattungsgegenstand eines Raumes
    public class RoomItem
    {
        // Beamer, Computer, ...
        [JsonProperty("type")]
        public string Type { get; private set; }

        // Anzahl des Gegenstands
        [JsonProperty("amount")]
        public int Amount { get; private set; }


        // String-Repräsentierung
        public override string ToString()
        {
            return $"{Type} ({Amount}x)";
        }
    }
    

    public class Room
    {
        // Name
        [JsonProperty("name")]
        public string Name { get; private set; }

        // Beschreibung
        [JsonProperty("description")]
        public string Description { get; private set; }

        // Kapazität
        [JsonProperty("capacity")]
        public int Capacity { get; private set; }

        // Ausstattungsgegenstände
        [JsonProperty("equipment")]
        public List<RoomItem> Equipment { get; private set; } = new List<RoomItem>();


        // lade alle Kurse zugehörig zum Raum
        public List<Course> Courses
        {
            get
            {
                // Erstelle leere Liste von Kursen
                List<Course> roomCourses = new List<Course>();

                // Gehe jeden Kurs in allen Kursen durch
                foreach (var course in JSON.Root.Courses.All)
                {
                    // Füge den Kurs der Liste hinzu, falls der Kurs in *diesem* Raum stattfindet

                    // ANNAHME:

                    // Kurs:
                    // {
                    //    "name": "Grundlagen Mediengestaltung, Praktikum",
                    //    "time": "14:00-15:30",
                    //    "room": "FU C0.02",
                    //    "days": [4]
                    // }

                    // *dieser* Raum:
                    // {
                    //      "name": "FU C0.02",
                    //      "description": "Hierbei handelt es sich um einen Raum im C-Bau",
                    //      "capacity": 50,
                    //      "equipment": [
                    //          {
                    //              "type": "beamer",
                    //              "amount": 1
                    //          }
                    //      ]
                    // }

                    // => Füge die Informationen des Kurses "Grundlagen Mediengestaltung, Praktikum" der Liste hinzu
                    if (course.Room.Contains(Name))
                    {
                        roomCourses.Add(course);
                    }
                }

                return roomCourses;
            }
        }


        // String-Repräsentierung
        public override string ToString()
        {
            // Leere Liste von Strings
            var sb = new StringBuilder();

            foreach (var item in Equipment)
            {
                sb.Append(item.ToString());
            }

            // => {"Beamer (1x)", "TV (2x)", ...}

            // string.Join(", ", sb)
            // => "Beamer (1x), TV (2x)"

            return $"{Name} ({Description}):\nKapazität: {Capacity}\nEquipment: {string.Join(", ", sb)}";
        }
    }
}
