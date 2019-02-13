using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    // Nimmt einen String in der Form von HH:mm-HH:mm (Stunde/Minute) an
    // und generiert daraus zwei DateTime-Objekte From und To.
    public class TimeRange
    {
        // DateTime vor dem Bindestrich (-)
        public DateTime From { get; set; }

        // DateTime nach dem Bindestrich (-)
        public DateTime To { get; set; }


        // Handelt es sich um einen leeren String,
        // wird die Zeit in 00:00-00:00 umgewandelt (Fallback).
        public TimeRange(string dateRangeString)
        {
            if (string.IsNullOrEmpty(dateRangeString))
            {
                From = StringToTime("00:00");
                To = StringToTime("00:00");
            }
            else
            {
                string[] range = dateRangeString.Split('-');

                From = StringToTime(range[0]);
                To = StringToTime(range[1]);
            }
        }

        // Eigentliche Konvertierung eines Strings in ein DateTime-Objekt
        private DateTime StringToTime(string dateTimeString, string format="HH:mm")
        {
            // Alle Leerzeichen entfernen
            string fixedDateTimeString = Regex.Replace(dateTimeString, @"\s+", "");

            // String als DateTime-Objekt parsen und zurückgeben
            return DateTime.ParseExact(fixedDateTimeString, format, CultureInfo.InvariantCulture);
        }
    }


    // Kurs
    public class Course
    {
        // Name
        [JsonProperty("name")]
        public string Name { get; private set; }

        // Raum
        [JsonProperty("room")]
        public string Room { get; private set; }

        // Zeiträume
        [JsonProperty("time")]
        public string Time { get; private set; }

        // Daten
        [JsonProperty("date")]
        public string Date { get; private set; }

        // Tage als Integer
        [JsonProperty("days")]
        public List<int> Days { get; private set; }


        // Gibt die List der Tage als Liste von Strings aus
        public List<string> DaysAsStrings
        {
            get
            {
                Dictionary<int, string> days = new Dictionary<int, string>()
                {
                    { 0, "Sonntag" },
                    { 1, "Montag" },
                    { 2, "Dienstag" },
                    { 3, "Mittwoch" },
                    { 4, "Donnerstag" },
                    { 5, "Freitag" },
                    { 6, "Samstag" },
                };

                List<string> daysStrings = new List<string>();

                foreach (int day in Days)
                {
                    daysStrings.Add(days[day]);
                }
                
                return daysStrings;
            }
        }


        // Konvertiert die Zeiträume (Property: Time) als TimeRange-Objekte
        public List<TimeRange> Times
        {
            get
            {
                List<TimeRange> times = new List<TimeRange>();

                string[] dateRangeStrings = Time.Split(';');

                foreach (string dateRangeString in dateRangeStrings)
                {
                    times.Add(new TimeRange(dateRangeString));
                }

                return times;
            }
        }


        // String-Repräsentierung
        public override string ToString()
        {
            string name = FormatName(JSON.Root.Courses.WPVWeekly, "WPV, wöchentlich");

            if (name == null)
            {
                name = FormatName(JSON.Root.Courses.WPVDateDependent, "WPV");
            }

            if (name == null)
            {
                name = Name;
            }

            string description = $"{name}\nRaum: {Room}\nZeiten: {Time}";

            if (Date != null)
            {
                description = $"{description}\nDatum: {Date}";
            }

            return $"{description}\nTage: {string.Join(", ", DaysAsStrings)}\n";
        }

        private string FormatName(List<Course> courses, string postfix)
        {
            foreach (var course in courses)
            {
                if (IsSame(course))
                {
                    return $"{Name} ({postfix})";
                }
            }

            return null;
        }

        // Prüfen, ob die Informationen des Kurses mit denen eines anderen Kurses übereinstimmen
        // Nützlich, falls eine Kopie *dieses* Kurses angelegt wurde (neues C#-Objekt) und verglichen werden muss
        public bool IsSame(Course other)
        {
            if (Name != other.Name)
            {
                return false;
            }

            if (Room != other.Room)
            {
                return false;
            }

            if (Time != other.Time)
            {
                return false;
            }

            foreach (int day in Days)
            {
                if (!other.Days.Contains(day))
                {
                    return false;
                }
            }

            return true;
        }
    }


    // Studiengang
    public class Major
    {
        [JsonProperty("semester1")]
        public List<Course> Semester1 { get; private set; }

        [JsonProperty("semester2")]
        public List<Course> Semester2 { get; private set; }

        [JsonProperty("semester3")]
        public List<Course> Semester3 { get; private set; }

        [JsonProperty("semester4")]
        public List<Course> Semester4 { get; private set; }

        [JsonProperty("semester5")]
        public List<Course> Semester5 { get; private set; }
    }


    // Kurse
    public class Courses
    {
        [JsonProperty("WPV-weekly")]
        public List<Course> WPVWeekly { get; private set; }

        [JsonProperty("WPV-date-dependent")]
        public List<Course> WPVDateDependent { get; private set; }

        [JsonProperty("MIB")]
        public Major MIB { get; private set; }

        [JsonProperty("MKB")]
        public Major MKB { get; private set; }

        [JsonProperty("OMB")]
        public Major OMB { get; private set; }


        // Lade alle WPV's
        public List<Course> WPV
        {
            get
            {
                // Erstelle leere Liste von Kursen
                List<Course> courses = new List<Course>();

                // Füge alle wöchentlichen WPVs hinzu
                courses.AddRange(WPVWeekly);

                // Füge alle datumsabhängigen WPVs hinzu
                courses.AddRange(WPVDateDependent);

                // Gebe die Liste aller WPV-Kurse zurück
                return courses;
            }
        }


        // Lade alle Pflichtfächer
        public List<Course> Mandatory
        {
            get
            {
                // Erstelle leere Liste von Kursen
                List<Course> courses = new List<Course>();

                // Füge alle MIB-Kurse hinzu
                courses.AddRange(MIB.Semester1);
                courses.AddRange(MIB.Semester2);
                courses.AddRange(MIB.Semester3);
                courses.AddRange(MIB.Semester4);
                courses.AddRange(MIB.Semester5);

                // Füge alle MKB-Kurse hinzu
                courses.AddRange(MKB.Semester1);
                courses.AddRange(MKB.Semester2);
                courses.AddRange(MKB.Semester3);
                courses.AddRange(MKB.Semester4);
                courses.AddRange(MKB.Semester5);

                // Füge alle OMB-Kurse hinzu
                courses.AddRange(OMB.Semester1);
                courses.AddRange(OMB.Semester2);
                courses.AddRange(OMB.Semester3);
                courses.AddRange(OMB.Semester4);
                courses.AddRange(OMB.Semester5);

                // Gebe die Liste aller Kurse aus MIB, MKB und OMB zurück
                return courses;
            }
        }


        // Lade alle Kurse
        public List<Course> All
        {
            get
            {
                // Erstelle leere Liste von Kursen
                List<Course> courses = new List<Course>();

                // Lade alle Pflichtfächer
                courses.AddRange(Mandatory);

                // Lade alle WPVs
                courses.AddRange(WPV);

                // Gebe Liste aller Kurse zurück
                return courses;
            }
        }
    }
}
