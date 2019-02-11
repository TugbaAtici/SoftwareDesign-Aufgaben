using System;
using System.Collections.Generic;

using L4_Stundenplan.Model;


namespace L4_Stundenplan
{
    class Program
    {
        static void Main(string[] args)
        {

            bool allGood = AllGood();

            if (!allGood)
            {
                Console.WriteLine("Es finden keine Doppelbelastungen statt.");
            }
            else
            {
                Console.WriteLine("Daten des Stundenplans wurden geladen.\n");

                MainMenu();
            }
        }

        private static bool AllGood()
        {
            List<Course> allCourses = new List<Course>();

            allCourses.AddRange(JSON.Root.Courses.WPVWeekly);
            allCourses.AddRange(JSON.Root.Courses.WPVDateDependent);

            allCourses.AddRange(JSON.Root.Courses.MIB.Semester1);
            allCourses.AddRange(JSON.Root.Courses.MIB.Semester2);
            allCourses.AddRange(JSON.Root.Courses.MIB.Semester3);
            allCourses.AddRange(JSON.Root.Courses.MIB.Semester4);
            allCourses.AddRange(JSON.Root.Courses.MIB.Semester5);

            allCourses.AddRange(JSON.Root.Courses.MKB.Semester1);
            allCourses.AddRange(JSON.Root.Courses.MKB.Semester2);
            allCourses.AddRange(JSON.Root.Courses.MKB.Semester3);
            allCourses.AddRange(JSON.Root.Courses.MKB.Semester4);
            allCourses.AddRange(JSON.Root.Courses.MKB.Semester5);

            allCourses.AddRange(JSON.Root.Courses.OMB.Semester1);
            allCourses.AddRange(JSON.Root.Courses.OMB.Semester2);
            allCourses.AddRange(JSON.Root.Courses.OMB.Semester3);
            allCourses.AddRange(JSON.Root.Courses.OMB.Semester4);
            allCourses.AddRange(JSON.Root.Courses.OMB.Semester5);

            for (int i = 0; i < allCourses.Count; i++)
            {
                Course iCourse = allCourses[i];

                for (int  j = 1; j < allCourses.Count; j++)
                {
                    // gleiches Kurs-Object
                    if (i == j)
                    {
                        continue;
                    }

                    Course jCourse = allCourses[j];

                    if (iCourse.Room == jCourse.Room)
                    {
                        bool sameTimes = false;

                        foreach (var iTimeRange in iCourse.Times)
                        {
                            foreach (var jTimeRange in jCourse.Times)
                            {
                                if (iTimeRange.To == jTimeRange.To && iTimeRange.From == jTimeRange.From)
                                {
                                    sameTimes = true;
                                    break;
                                }
                            }
                        }

                        if (sameTimes)
                        {
                            bool sameDays = true;

                            foreach (int iDay in iCourse.Days)
                            {
                                foreach (int jDay in jCourse.Days)
                                {
                                    if (iDay != jDay)
                                    {
                                        sameDays = false;
                                        break;
                                    }
                                }
                            }

                            if (sameDays)
                            {
                                if (iCourse.Name != jCourse.Name)
                                {
                                    Console.WriteLine($"iCourse ({iCourse.ToString()}) = jCourse ({jCourse.ToString()}");
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        private static void MainMenu()
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.Clear();

                choice = ReadType();

                if (choice <= 0)
                {
                    break;
                }

                if (choice == 1)
                {
                    int major = ReadMajor();

                    if (major < 1 || major > 3)
                    {
                        continue;
                    }

                    int semester = ReadSemester();

                    if (semester < 1 || semester > 5)
                    {
                        continue;
                    }

                    if (major == 1)
                    {
                        PrintSemester(JSON.Root.Courses.MIB, semester);
                    }

                    else if (major == 2)
                    {
                        PrintSemester(JSON.Root.Courses.MKB, semester);
                    }

                    else if (major == 3)
                    {
                        PrintSemester(JSON.Root.Courses.OMB, semester);
                    }

                    Console.WriteLine();
                    Console.ReadLine();
                }
            }
        }

        private static int ReadInput()
        {
            Console.Write("\n\nAuswahl: ");
            string input = Console.ReadLine();

            Console.WriteLine();

            int number;

            if (!Int32.TryParse(input, out number))
            {
                return 0;
            }

            return number;
        }

        private static int ReadType()
        {
            Console.WriteLine("[1] Semesterstundenplan");
            Console.WriteLine("[2] Gesamter Stundenplan\n");
            Console.WriteLine("[0] Abbrechen");

            return ReadInput();
        }

        private static int ReadMajor()
        {
            Console.WriteLine("[1] MIB");
            Console.WriteLine("[2] MKB");
            Console.WriteLine("[3] OMB");

            return ReadInput();
        }

        private static int ReadSemester()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Semester [{i + 1}]");
            }

            return ReadInput();
        }

        private static void PrintSemester(Major major, int semester)
        {
            if (semester == 1)
            {
                PrintCourses(major.Semester1);
            }

            else if (semester == 2)
            {
                PrintCourses(major.Semester2);
            }

            else if (semester == 3)
            {
                PrintCourses(major.Semester3);
            }

            else if (semester == 4)
            {
                PrintCourses(major.Semester4);
            }

            else if (semester == 5)
            {
                PrintCourses(major.Semester5);
            }
        }

        private static void PrintCourses(List<Course> courses)
        {
            Console.WriteLine("------------------------------------\n");
            
            foreach (var course in courses)
            {
                Console.WriteLine(course.ToString());
            }

            Console.WriteLine("\n------------------------------------");
        }
    }
}
