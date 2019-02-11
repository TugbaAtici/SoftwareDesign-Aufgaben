using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;


namespace L4_Stundenplan.Model
{
    public class Professor
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("email")]
        public string EMail { get; private set; }

        [JsonProperty("offered_courses")]
        public List<string> _OfferedCourses { get; private set; }

        private void AddOfferedCourses(ref List<Course> offeredCourses, List<Course> courses)
        {
            foreach (var course in courses)
            {
                if (_OfferedCourses.Contains(course.Name))
                {
                    offeredCourses.Add(course);
                }
            }
        }

        public List<Course> OfferedCourses
        {
            get
            {
                List<Course> offeredCourses = new List<Course>();

                // WPV
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.WPVWeekly);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.WPVDateDependent);

                // MIB
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MIB.Semester1);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MIB.Semester2);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MIB.Semester3);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MIB.Semester4);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MIB.Semester5);
                
                // MKB
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MKB.Semester1);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MKB.Semester2);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MKB.Semester3);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MKB.Semester4);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.MKB.Semester5);

                // OMB
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.OMB.Semester1);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.OMB.Semester2);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.OMB.Semester3);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.OMB.Semester4);
                AddOfferedCourses(ref offeredCourses, JSON.Root.Courses.OMB.Semester5);

                return offeredCourses;
            }
        }
        
        public List<TimeRange> CourseTimes
        {
            get
            {
                List<TimeRange> courseTimes = new List<TimeRange>();

                foreach (var course in OfferedCourses)
                {
                    courseTimes.AddRange(course.Times);
                }

                return courseTimes;
            }
        }

        // 7:45-20:00
        public List<TimeRange> AvailableTimes
        {
            get
            {
                List<TimeRange> courseTimes = CourseTimes;
                courseTimes.Sort((x, y) => DateTime.Compare(x.From, y.From));

                List<TimeRange> availableTimes = new List<TimeRange>();

                

                return availableTimes;
            }
        }
    }
}