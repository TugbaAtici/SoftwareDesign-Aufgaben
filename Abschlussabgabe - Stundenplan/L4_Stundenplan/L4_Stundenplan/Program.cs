using System;
using System.Collections.Generic;
using System.IO;

using L4_Stundenplan.Data;

namespace L4_Stundenplan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generiere Stundenplan...");
            //Lade all Pflichtfächer
            var timetable = GenerateTimetable();
            
            if (timetable != null)
            {
                Console.WriteLine("Stundenplan erfolgreich generiert.\n");
                Console.ReadLine();

                MainMenu(timetable);
            }
            else
            {
                Console.WriteLine("Stundenplan konnte nicht generiert werden!");
                Console.ReadLine();
            }
        }

        private static int ReadInput()
        {
            Console.Write("\n\nAuswahl: ");
            string input = Console.ReadLine();  // Eingabe als String, z.B. "3" oder "a"

            Console.WriteLine();

            // Versuche, input in einen Integer umzuwandeln
            // => aus "3" wird 3
            
            // Falls nicht möglich, gebe 0 zurück
            // => aus "a" wird 0
            int number;

            if (!Int32.TryParse(input, out number))
            {
                return 0;
            }

            return number;
        }

        //Überprüft, ob Eingabe übereinstimmt mit der zur Auswahl stehenden Zahlen
        public static bool CheckInput(int input, int min, int max)
        {
            if (input >= min && input <= max)
            {
                return true;
            }

            Console.WriteLine("Falsche Eingabe!");
            Console.WriteLine($"Gebe bitte eine Zahl von {min} bis {max} ein.");

            return false;
        }


        // Liest den Typ des Stundenplans ein
        public static int ReadType()
        {
            Console.WriteLine("[1] Kohorte");
            Console.WriteLine("[2] Dozenti");
            Console.WriteLine("[3] Raum");
            Console.WriteLine("[4] Alle Kurse\n");

            Console.WriteLine("[0] Abbrechen");

            return ReadInput();
        }

        // Liest den Studiengang ein
        public static int ReadCohort()
        {
            foreach (var cohortEntry in Timetable.CohortMap)
            {
                Console.WriteLine($"[{cohortEntry.Key}] {cohortEntry.Value}");
            }

            return ReadInput();
        }

        // Liest das Semester ein
        public static int ReadSemester(List<int> semesters)
        {
            for (int semesterCount = 0; semesterCount < semesters.Count; semesterCount++)
            {
                int semester = semesters[semesterCount];
                Console.WriteLine($"[{semesterCount + 1}] Semester {semester}");
            }

            return ReadInput();
        }

        // Liest den Professor ein

        // Gibt null bei Falscheingabe zurück
        public static Professor ReadProfessor(Timetable timetable)
        {
            var professors = timetable.Data.Professors;

            // Zeige alle Professoren nacheinander an,
            // mit 1 bis <Anzahl der Professoren> als ID
            for (int i = 0; i < professors.Count; i++)
            {
                var professor = professors[i];
                Console.WriteLine($"[{i + 1}] {professor.Name} ({professor.Email})");
            }

            int professorID = ReadInput();

            if (!CheckInput(professorID, 1, professors.Count))
            {
                return null;
            }

            // Gebe das C#-Objekts des Professors zurück
            return professors[professorID - 1];
        }

        // Liest den Raum ein
        // Gibt null bei Falscheingabe zurück
        private static Room ReadRoom(Timetable timetable)
        {
            //Greift auf die Räume in der JSON-Datei zurück 
            var rooms = timetable.Data.Rooms;

            // Zeige alle Räume nacheinander an,
            // mit 1 bis <Anzahl der Räume> als ID
            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                Console.WriteLine($"[{i + 1}] {room.Name}");
            }

            int roomID = ReadInput();

            if (!CheckInput(roomID, 1, rooms.Count))
            {
                return null;
            }

            // Gebe das C#-Objekts des Raums zurück
            return rooms[roomID - 1];
        }

        //generiere Stundenplan
        public static Timetable GenerateTimetable()
        {
            //JSON-Dateien werden eingelesen
            var timetable = new Timetable("data.json");

            //Stundenplan wird generiert
            foreach (string cohortName in Timetable.CohortMap.Values)
            {
                
                var cohort = timetable.GetCohortFromName(cohortName);
                var semesters = timetable.GetSemesters(cohort);

                foreach (int semester in semesters)
                {
                    //Pflichtkurs-Duplikate löschen, damit nicht 2x angezeigt wird
                    var courses = timetable.GetCoursesForSemester(cohort, semester);

                    foreach (var session in timetable.Sessions)
                    {
                        for (int i = 0; i < courses.Count; i++)
                        {
                            var course = courses[i];

                            //Wenn Kursname und Professor übereinstimmen mit einem Eintrag im bereits generierten Stundenplan
                            //-->Lösche Kurs                            
                            //Gleicher Kurs andere Kohorte
                            if (course.Name == session.Course.Name && course.Professor == session.Course.Professor)
                            {
                                courses.Remove(course);
                            }
                        }
                    }

                    //Generiert einen Teil des Stundenplans anhand der Kohorte + Semester
                    int generateResult = timetable.Generate(courses);

                    if (generateResult == 0)
                    {
                        //Console.WriteLine("Erfolg!!!\n");
                    }
                    else
                    {
                        Console.WriteLine("Konnte nicht erstellen.");

                        // nicht genügend Räume
                        if (generateResult == -1)
                        {
                            Console.WriteLine($"Grund: nicht genügend Räume");
                        }

                        // nicht genügend Blöcke
                        else if (generateResult == -2)
                        {
                            Console.WriteLine($"Grund: nicht genügend Blöcke");
                        }

                        return null;
                    }
                }
            }

            return timetable;
        }

        //Erstellt Main Menü
        public static void MainMenu(Timetable timetable)
        {
            //  1 = Kohorte
            //  2 = Dozenti
            //  3 = Raum
            //  4 = Alle Kurse
            //
            //  0 = Beenden
            int type = -1;


            // Eingabe auswerten
            while (type != 0)
            {
                // Text der Konsole löschen
                Console.Clear();
                Console.WriteLine();

                // Stundenplantyp auslesen
                type = ReadType();


                // Bei < 1 beenden
                if (type < 1)
                {
                    break;
                }


                // Kohorte
                if (type == 1)
                {
                    int cohortNumber = ReadCohort();

                    if (CheckInput(cohortNumber, 1, Timetable.CohortMap.Count))
                    {
                        string cohortName = Timetable.CohortMap[cohortNumber];
                        var cohort = timetable.GetCohortFromName(cohortName);

                        var semesters = timetable.GetSemesters(cohort);
                        int semesterInput = ReadSemester(semesters);

                        if (CheckInput(semesterInput, 1, semesters.Count))
                        {
                            int semester = semesters[semesterInput - 1];
                            var sessions = timetable.GetSessions(cohort, semester);

                            Console.WriteLine($"Stundenplan: {cohort.Name} / Semester {semester}\n");

                            foreach (var session in sessions)
                            {
                                Console.WriteLine(session.ToString());
                                Console.WriteLine();
                            }

                            timetable.GenerateWPV(sessions);

                            Console.WriteLine();
                            Console.WriteLine($"WPV-Stundenplan: {cohort.Name} / Semester {semester}\n");

                            foreach (var wpvSession in timetable.WPVSessions)
                            {
                                Console.WriteLine(wpvSession.ToString());
                                Console.WriteLine();
                            }
                        }
                    }
                }

                //Bei Input = 2 zeige Profs an
                else if (type == 2)
                {
                    var professor = ReadProfessor(timetable);

                    if (professor != null)
                    {
                        var sessions = timetable.GetSessions(professor);

                        Console.WriteLine($"Stundenplan: {professor.Name}\n");

                        foreach (var session in sessions)
                        {
                            Console.WriteLine(session.ToString());
                            Console.WriteLine();
                        }
                    }
                }

                //Bei Input = 3 zeige Räume an
                else if (type == 3)
                {
                    var room = ReadRoom(timetable);

                    if (room != null)
                    {
                        var sessions = timetable.GetSessions(room);

                        Console.WriteLine($"Stundenplan: {room.Name}\n");

                        foreach (var session in sessions)
                        {
                            Console.WriteLine(session.ToString());
                            Console.WriteLine();
                        }
                    }
                }

                //Bei Input = 4 zeige alle Kurse an
                else if (type == 4)
                {
                    Console.WriteLine($"Stundenplan für alle Kurse in jedem Semester\n");

                    foreach (var session in timetable.Sessions)
                    {
                        Console.WriteLine(session.ToString());
                        Console.WriteLine();
                    }

                    Console.WriteLine(timetable.Sessions.Count);
                }

                // Auf Eingabe des Benutzers warten
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
