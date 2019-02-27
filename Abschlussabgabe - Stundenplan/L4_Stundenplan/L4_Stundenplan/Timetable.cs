using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using L4_Stundenplan.Data;

namespace L4_Stundenplan
{
    public class Timetable
    {
        public TimetableData Data {get; private set;}

        public List<Session> Sessions {get;private set;}

        public List<WPVSession> WPVSessions {get; private set;}

        public static Dictionary<int, string> CohortMap {get; private set;} = new Dictionary<int, string>()
        {
            {1, "MIB"},
            {2, "MKB"},
            {3, "OMB"}
        };
        
        public Timetable(string jsonFile)
        {
            //Lese JSON-Datei ein + deserialisiere
            Data = JsonConvert.DeserializeObject<TimetableData>(File.ReadAllText(jsonFile));

            //Erstelle Pflichtfächer + WPV's als eine neue Liste einspeichern
            Sessions = new List<Session>();
            WPVSessions = new List<WPVSession>();
        }


        /* -----GETTERS------ */

        //Suche Professor anhand Name um an weitere Informationen zu gelangen (wie Verfügbarkeit)
        public Professor GetProfessorFromName(string professorName)
        {
            foreach (var professor in Data.Professors)
            {
                if (professor.Name == professorName)
                {
                    return professor;
                }
            }

            return null;
        }

        public Cohort GetCohortFromName(string cohortName)
        {
            foreach (var cohort in Data.Cohorts)
            {
                if (cohort.Name == cohortName)
                {
                    return cohort;
                }
            }

            // wird nie passieren, da Benutzereingabe nie < 1 oder > 3
            return null;
        }

        
        public List<Course> GetCoursesForCohort(string cohortName)
        {
            var courses = new List<Course>();

            foreach (var course in Data.Courses)
            {
                if (course.Cohorts.Contains(cohortName))
                {
                    courses.Add(course);
                }
            }

            return courses;
        }

        public List<Course> GetCoursesForSemester(Cohort cohort, int semester)
        {
            var cohortCourses = GetCoursesForCohort(cohort.Name);
            var semesterCourses = new List<Course>();

            foreach (var cohortCourse in cohortCourses)
            {
                if (cohortCourse.Semester == semester)
                {
                    semesterCourses.Add(cohortCourse);
                }
            }

            return semesterCourses;
        }

        public List<int> GetSemesters(Cohort cohort)
        {
            var count = new HashSet<int>();

            foreach (var course in Data.Courses)
            {
                if (course.Cohorts.Contains(cohort.Name))
                {
                    count.Add(course.Semester);
                }
            }

            return new List<int>(count);
        }

        public List<Session> GetSessions(Cohort cohort, int semester)
        {
            var sessions = new List<Session>();

            foreach (var session in Sessions)
            {
                if (session.Course.Cohorts.Contains(cohort.Name) && session.Course.Semester == semester)
                {
                    sessions.Add(session);
                }
            }

            return sessions;
        }

        public List<Session> GetSessions(Professor professor)
        {
            var sessions = new List<Session>();

            foreach (var session in Sessions)
            {
                if (session.Course.Professor == professor.Name)
                {
                    sessions.Add(session);
                }
            }

            return sessions;
        }

        public List<Session> GetSessions(Room room)
        {
            var sessions = new List<Session>();

            foreach (var session in Sessions)
            {
                if (session.Room.Name == room.Name)
                {
                    sessions.Add(session);
                }
            }

            return sessions;
        }


        /* ----- ROOM CHECKS ---------- */

        public bool IsRoomOccupied(Room room, Block block)
        {
            foreach (var session in Sessions)
            {
                if (session.Room != room)
                {
                    continue;
                }

                foreach (var sessionBlock in session.Blocks)
                {
                    if (block.DayNumber == sessionBlock.DayNumber && block.BlockNumber == sessionBlock.BlockNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsWPVRoomOccupied(Room room, Block block)
        {
            foreach (var wpvSession in WPVSessions)
            {
                if (wpvSession.Room != room)
                {
                    continue;
                }

                foreach (var wpvSessionBlock in wpvSession.Blocks)
                {
                    if (block.DayNumber == wpvSessionBlock.DayNumber && block.BlockNumber == wpvSessionBlock.BlockNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool HasRoomEquipment(Room room, List<string> items)
        {
            foreach (string item in items)
            {
                if (!room.Equipment.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }


        /* ----- PROFESSOR CHECKS ---------- */


        public bool IsProfessorAvailableAt(string professorName, int day, int blockNumber)
        {
            var professor = GetProfessorFromName(professorName);

            foreach (var blockDescription in professor.Unavailable)
            {
                if (blockDescription.Day == day && blockDescription.Block == blockNumber)
                {
                    return false;
                }
            }

            return true;
        }

        
        /* ----- GENERATION ---------- */

        //suche geeigneten Raum und geeignete Blöcke
        public int Generate(List<Course> courses)
        {
            var blocks = Data.Blocks;

            //Erstelle für jeden Kurs eine neue Session
            foreach (var course in courses)
            {
                var session = new Session()
                {
                    Course = course,
                    Room = null
                };

                int students = 0;
                //Zähle Studentenanzahl aus allen Studiengängen
                foreach (string cohortName in course.Cohorts)
                {
                    var cohort = GetCohortFromName(cohortName);
                    students += cohort.Students;
                }
                //Hole neue sortierte Liste aller Räume (Sortierung nach Kapazität)
                var rooms = new List<Room>(Data.Rooms);
                rooms.Sort((x, y) => x.Capacity.CompareTo(y.Capacity));

                //Prüfe für jeden Raum:
                //Reicht Kapazität aus
                //Hat der Raum das benötigte Equipment
                foreach (var room in rooms)
                {
                    if (room.Capacity < students)
                    {
                        continue;
                    }

                    if (!HasRoomEquipment(room, course.Equipment))
                    {
                        continue;
                    }

                    //Falls Kurs einen Raum und genügend Blöcke hat, dann mache mit dem nächsten Kurs weiter
                    if (session.Room != null && session.Blocks.Count == course.Blocks)
                    {
                        break;
                    }
                    
                    //Für jeden Block des Kurses
                    //Prüfe für jeden existierenden Block, ob der Professor Zeit hat, ob der Block belegt ist und ob der Raum an diesem Block belegt ist
                    //Falls Prof Zeit hat, Blöcke und Raum nicht belegt sind, merke in Session
                    for (int blockCount = 0; blockCount < course.Blocks; blockCount++)
                    {
                        foreach (var block in blocks)
                        {
                            if (IsProfessorAvailableAt(course.Professor, block.DayNumber, block.BlockNumber) && !block.IsOccupied && !IsRoomOccupied(room, block))
                            {
                                session.Room = room;
                                session.Blocks.Add(block);

                                block.IsOccupied = true;

                                if (!Sessions.Contains(session))
                                {
                                    Sessions.Add(session);
                                }

                                break;
                            }
                        }
                    }
                }

                //Falls kein Raum gefunden wurde, gebe -1 zurück (Mehr Kurse als Räume)
                if (session.Room == null)
                {
                    return -1;
                }

                //Falls kein Block gefunden wurde, gebe -2 zurück (Mehr Kurse als verfügbare Blöcke)
                if (session.Blocks.Count != course.Blocks)
                {
                    return -2;
                }
            }

            // Es wurden alle Kurse des Semesters behandelt
            // => jeder Kurs hat einen Raum und einen Block
            return 0;
        }


        /* ----- WPV ---------- */

        public bool AreBlocksOverlapping(List<Block> blocks, List<BlockDescription> blockDescriptions)
        {
            foreach (var block in blocks)
            {
                foreach (var blockDescription in blockDescriptions)
                {
                    if (block.DayNumber == blockDescription.Day && block.BlockNumber == blockDescription.Block)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool AreSessionsOverlapping(List<Session> sessions, WPV wpv)
        {
            foreach (var session in sessions)
            {
                if (AreBlocksOverlapping(session.Blocks, wpv.BlockDescriptions))
                {
                    return true;
                }
            }

            return false;
        }

        //Generiere WPV-Stundenplan
        public void GenerateWPV(List<Session> sessions)
        {
            WPVSessions.Clear();

            var rooms = Data.Rooms;
            
            //Prüfe, ob WPV mit Sessions überlappt
            foreach (var wpv in Data.WPV)
            {
                if (AreSessionsOverlapping(sessions, wpv))
                {
                    continue;
                }

                var wpvSession = new WPVSession()
                {
                    WPV = wpv,
                    Room = null
                };

                //Gehe jeden Raum durch und prüfe ob Equipment passt
                foreach (var room in rooms)
                {
                    if (!HasRoomEquipment(room, wpv.Equipment))
                    {
                        continue;
                    }

                    if (wpvSession.Room != null && wpvSession.Blocks.Count == wpv.BlockDescriptions.Count)
                    {
                        break;
                    }

                    foreach (var wpvBlockDescription in wpv.BlockDescriptions)
                    {
                        string blockString = Data.BlockStrings[wpvBlockDescription.Block - 1];
                        var wpvBlock = new Block(wpvBlockDescription.Day, wpvBlockDescription.Block, blockString);

                        //Prüfe, ob Raum mit Session besetzt ist
                        //Prüfe, ob Raum mit WPV's besetzt ist 
                        if (!IsRoomOccupied(room, wpvBlock) && !IsWPVRoomOccupied(room, wpvBlock))
                        {
                            wpvSession.Room = room;
                            wpvSession.Blocks.Add(wpvBlock);

                            if (!WPVSessions.Contains(wpvSession))
                            {
                                WPVSessions.Add(wpvSession);
                            }
                        }
                    }
                }
            }
        }
    }
}
