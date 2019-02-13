using System;
using System.Collections.Generic;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    public class Professor
    {
        // Name (mit Titel)
        [JsonProperty("name")]
        public string Name { get; private set; }

        // EMail
        [JsonProperty("email")]
        public string EMail { get; private set; }

        // Angebotene Kurse als roher String wie er in der JSON steht
        [JsonProperty("offered_courses")]
        public List<string> OfferedCourseNames { get; private set; }


        // Lade alle Kurse zugehörig zum Prof
        public List<Course> OfferedCourses
        {
            get
            {
                // Erstelle leere Liste von Kursen
                List<Course> offeredCourses = new List<Course>();

                // Gehe ALLE Kurse durch (unabhängig von Studiengang/WPV)
                foreach (var course in JSON.Root.Courses.All)
                {
                    // Füge den Kurs der Liste hinzu, falls sich der Name des Kurses
                    // in den vom Professor angebotenen Kursen befindet

                    // ANNAHME:
                    //
                    // Kurs:
                    //  {
                    //      "name": "Grundlagen Interaktiver Systeme, Vorlesung",
                    //      "time": "07:45-09:15",
                    //      "room": "FU I0.13",
                    //      "days": [2]
                    //  }

                    // OfferedCourseNames eines Professors:
                    //  ["Grundlagen Interaktiver Systeme, Vorlesung"]

                    // => Füge die Informationen von "Grundlagen Interaktiver Systeme, Vorlesung" (Kurs) in die Liste hinzu

                    if (OfferedCourseNames.Contains(course.Name))
                    {
                        offeredCourses.Add(course);
                    }
                }

                return offeredCourses;
            }
        }
    }
}