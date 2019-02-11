using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    public class TimeRange
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }


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

        private DateTime StringToTime(string dateTimeString, string format="HH:mm")
        {
            string fixedDateTimeString = Regex.Replace(dateTimeString, @"\s+", "");
            return DateTime.ParseExact(fixedDateTimeString, format, CultureInfo.InvariantCulture);
        }
    }

    public class Course
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("room")]
        public string Room { get; private set; }

        [JsonProperty("time")]
        public string Time { get; private set; }

        [JsonProperty("date")]
        public string Date { get; private set; }

        [JsonProperty("days")]
        public List<int> Days { get; private set; }

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

        public List<TimeRange> Dates
        {
            get
            {
                List<TimeRange> dates = new List<TimeRange>();

                if (Date != null)
                { 
                    string[] dateRangeStrings = Date.Split(';');

                    foreach (string dateRangeString in dateRangeStrings)
                    {
                        dates.Add(new TimeRange(dateRangeString));
                    }
                }

                return dates;
            }
        }

        public override string ToString()
        {
            return $"{Name}\nRaum: {Room}\nZeiten: {Time}\nTage: {string.Join(", ", DaysAsStrings)}\n";
        }
    }

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
    }
}
