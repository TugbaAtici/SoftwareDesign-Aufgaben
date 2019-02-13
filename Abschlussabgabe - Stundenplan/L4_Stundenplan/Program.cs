using System;
using System.Collections.Generic;

using L4_Stundenplan.Model;

namespace L4_Stundenplan
{
    class Program
    {
        static void Main(string[] args)
        {
            // JSON-Datei einlesen
            JSON.Deserialize();

            // Prüfe auf Doppelbelastungen
            bool allGood = AllGood();

            if (!allGood)
            {
                // Überprüfung fehlgeschlagen
                Console.WriteLine("Überlappung!!!");
                Console.ReadLine();
            }
            else
            {
                // Überprüfung erfolgreich
                Console.WriteLine("Daten des Stundenplans wurden geladen.\n");
                Console.ReadLine();

                // Hauptmenü
                MainMenu();
            }
        }

        //Prüfe Daten auf Doppelbelastung
        private static bool AllGood()
        {
            // Lade alle Pflichtfächer
            List<Course> mandatoryCourses = JSON.Root.Courses.Mandatory;


            // Lade ersten Kurs
            Course courseA = mandatoryCourses[0];


            // Vergleiche ersten Kurs mit allen anderen
            for (int b = 1; b < mandatoryCourses.Count; b++)
            {
                // Hole Kurs an Stelle b
                // => wenn b = 1, courseB = zweiter Kurs
                Course courseB = mandatoryCourses[b];
            

                // verschiedene Kurse, gleicher Raum
                if (courseA.Room == courseB.Room)
                {
                    // Überprüfe auf selbe Zeit
                    bool sameTimes = false;
                        
                    // Vergleiche ALLE Zeitspannen von Kurs A mit ALLEN Zeitspannen von Kurs B
                    // Falls Zeitspanne A = Zeitspanne B => Kurse finden zur selben Zeit im selben Raum statt
                    // => Doppelbelastung möglich (aber der Tag wurde noch nicht geprüft)
                    foreach (var timeRangeA in courseA.Times)
                    {
                        foreach (var timeRangeB in courseB.Times)
                        {
                            if (timeRangeA.To == timeRangeB.To && timeRangeA.From == timeRangeB.From)
                            {
                                sameTimes = true;
                                break;
                            }
                        }
                    }

                    // Falls Doppelbelastung möglich (anhand der Zeitspannen von Kurs A und Kurs B),
                    // prüfe ob die Kurse am selben Tag stattfinden
                    if (sameTimes)
                    {
                        bool sameDays = true;

                        foreach (int dayA in courseA.Days)
                        {
                            foreach (int dayB in courseB.Days)
                            {
                                if (dayA != dayB)
                                {
                                    sameDays = false;
                                    break;
                                }
                            }
                        }


                        if (sameDays)
                        {
                            // Annahme 1:
                            // Kurs A = "BWL" (MKB)
                            // Kurs B = "Informatik" (MIB)
                            // => Kurse finden im selben Raum zur selben Zeit am selben Tag statt
                            // => Überlappung!

                            // Annahme 2:
                            // Kurs A = "BWL" (MIB)
                            // Kurs B = "BWL" (MKB)
                            // => keine Überlappung
                            if (courseA.Name != courseB.Name)
                            {
                                Console.WriteLine($"iCourse ({courseA.ToString()}) = jCourse ({courseB.ToString()}");
                                return false;
                            }
                        }
                    }
                }
            }

            // Keine Überlappung!
            return true;
        }

        // Zeigt das Hauptmenü in einem While-Loop an,
        // bis der Benutzer eine ungültige Wahl trifft (0 oder < 0).
        private static void MainMenu()
        {
            /* Stundenplantyp */
            //  1 = Kohorte
            //  2 = Dozenti
            //  3 = Raum
            //  4 = Alle Kurse
            //
            //  0 = Beenden
            int type = -1;


            // Eingabe auswerten
            // Erste Raute im Hauptmenü-Diagramm
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
                    PrintCohort();
                }


                // Dozenti
                else if (type == 2)
                {
                    // Professor anhand seiner ID auslesen
                    var professor = ReadProfessor();

                    // Angebotene Kurse des Professors ausgeben, falls keine Falscheingabe gemacht wurde
                    if (professor != null)
                    {
                        Console.WriteLine($"Angebotene Kurse von {professor.Name} ({professor.EMail}):\n");

                        PrintCourses(professor.OfferedCourses);
                    }
                    else
                    {
                        Console.WriteLine("Falsche Eingabe!");
                        Console.WriteLine($"Gebe bitte eine Zahl von 1 bis {JSON.Root.Professors.Count} ein.");
                    }
                }


                // Raum
                else if (type == 3)
                {
                    // Raum anhand seiner ID auslesen
                    var room = ReadRoom();

                    // Raumbeschreibung und Kurse des Raumes ausgeben, falls keine Falscheingabe gemacht wurde
                    if (room != null)
                    {
                        Console.WriteLine($"{room.ToString()}:\n");

                        PrintCourses(room.Courses);
                    }
                    else
                    {
                        Console.WriteLine("Falsche Eingabe!");
                        Console.WriteLine($"Gebe bitte eine Zahl von 1 bis {JSON.Root.Rooms.Count} ein.");
                    }
                }


                // Alle Kurse
                else if (type == 4)
                {
                    PrintCourses(JSON.Root.Courses.All);
                }

                else
                {
                    Console.WriteLine("Falsche Eingabe!");
                    Console.WriteLine("Gebe bitte eine Zahl von 0 bis 4 ein.");
                }


                // Auf Eingabe des Benutzers warten
                Console.WriteLine();
                Console.ReadLine();
            }
        }


        // Liest eine Zahl von der Tastatur ein
        // Gibt 0 zurück, falls die eingelesene Zeile nicht in einen Integer umgewandelt werden kann
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


        // Liest den Typ des Stundenplans ein
        private static int ReadType()
        {
            Console.WriteLine("[1] Kohorte");
            Console.WriteLine("[2] Dozenti");
            Console.WriteLine("[3] Raum");
            Console.WriteLine("[4] Alle Kurse\n");

            Console.WriteLine("[0] Abbrechen");

            return ReadInput();
        }


        // Liest den Studiengang ein
        private static int ReadMajor()
        {
            Console.WriteLine("[1] MIB");
            Console.WriteLine("[2] MKB");
            Console.WriteLine("[3] OMB");

            return ReadInput();
        }


        // Lest das Semester ein
        private static int ReadSemester()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"[{i + 1}] Semester {i + 1}");
            }

            return ReadInput();
        }


        // Liest den Professor ein

        // Gibt null bei Falscheingabe zurück
        private static Professor ReadProfessor()
        {
            // Zeige alle Professoren nacheinander an,
            // mit 1 bis <Anzahl der Professoren> als ID
            for (int i = 0; i < JSON.Root.Professors.Count; i++)
            {
                var professor = JSON.Root.Professors[i];
                Console.WriteLine($"[{i + 1}] {professor.Name} ({professor.EMail})");
            }

            int professorID = ReadInput();


            // Gebe null bei Falscheingabe zurück
            if (professorID < 1 || professorID > JSON.Root.Professors.Count)
            {
                return null;
            }

            // Gebe das C#-Objekts des Professors zurück
            return JSON.Root.Professors[professorID - 1];
        }


        // Liest den Raum ein

        // Gibt null bei Falscheingabe zurück
        private static Room ReadRoom()
        {
            // Zeige alle Räume nacheinander an,
            // mit 1 bis <Anzahl der Räume> als ID
            for (int i = 0; i < JSON.Root.Rooms.Count; i++)
            {
                var room = JSON.Root.Rooms[i];
                Console.WriteLine($"[{i + 1}] {room.Name} ({room.Description})");
            }

            int roomID = ReadInput();


            // Gebe null bei Falscheingabe zurück
            if (roomID < 1 || roomID > JSON.Root.Rooms.Count)
            {
                return null;
            }

            // Gebe das C#-Objekts des Raumes zurück
            return JSON.Root.Rooms[roomID - 1];
        }


        // Liest den Studiengang und das Semester für den ausgewählten Studiengang ein
        // und gibt eine Liste aller Kurse des Studiengangs aus
        private static void PrintCohort()
        {
            // Studiengang einlesen
            int major = ReadMajor();

            // Auf Falscheingabe prüfen
            if (major < 1 || major > 3)
            {
                Console.WriteLine("Falsche Eingabe!");
                Console.WriteLine("Gebe bitte eine Zahl von 1 bis 3 ein.");
                return;
            }

            // Semester einlesen
            int semester = ReadSemester();

            // Auf falscheingabe prüfen
            if (semester < 1 || semester > 5)
            {
                Console.WriteLine("Falsche Eingabe!");
                Console.WriteLine("Gebe bitte eine Zahl von 1 bis 5 ein.");
                return;
            }

            // Semester für MIB ausgeben
            if (major == 1)
            {
                PrintSemester(JSON.Root.Courses.MIB, semester);
            }

            // Semester für MKB ausgeben
            else if (major == 2)
            {
                PrintSemester(JSON.Root.Courses.MKB, semester);
            }

            // Semester für OMB ausgeben
            else if (major == 3)
            {
                PrintSemester(JSON.Root.Courses.OMB, semester);
            }
        }


        // Gibt eine Liste der Kurse des Semesters des Studiengangs aus
        private static void PrintSemester(Major major, int semester)
        {
            List<Course> semesterCourses;

            if (semester == 1)
            {
                semesterCourses = major.Semester1;
            }

            else if (semester == 2)
            {
                semesterCourses = major.Semester2;
            }

            else if (semester == 3)
            {
                semesterCourses = major.Semester3;
            }

            else if (semester == 4)
            {
                semesterCourses = major.Semester4;
            }

            else
            {
                semesterCourses = major.Semester5;
            }


            //liste alle Kurse zum ausgewählten Studiengang+Semester auf
            PrintCourses(semesterCourses);

            //liste passende WPV's auf
            Console.WriteLine("\n\nZur Auswahl stehende Wahlpflichtfächer:\n");
            PrintWPV(semesterCourses);
        }


        // Gibt eine Liste von Kursen aus
        private static void PrintCourses(List<Course> courses)
        {
            Console.WriteLine("------------------------------------\n");
            
            foreach (var course in courses)
            {
                Console.WriteLine(course.ToString());
            }

            Console.WriteLine("\n------------------------------------");
        }

        //liste in den Stundenplan passende WPV's auf
        private static void PrintWPV(List<Course> semester)
        {
            // Kopie aller WPVs
            List<Course> wpv = new List<Course>(JSON.Root.Courses.WPV);


            // Gehe alle Kurse des Semesters durch (z.B. 1. Semester MIB)
            foreach (var semesterCourse in semester)
            {
                // Gehe alle WPV-Kurse durch
                foreach (var wpvCourse in JSON.Root.Courses.WPV)
                {
                    // Gehe alle Tage durch, an denen der Semester-Kurs stattfindet
                    foreach (int semesterCourseDay in semesterCourse.Days)
                    {
                        // Findet der Semester-Kurs am selben Tag statt, wie der WPV-Kurs...
                        if (wpvCourse.Days.Contains(semesterCourseDay))
                        {
                            //prüfe auf Überlappung
                            // ... prüfe ob die Zeiten der 2 Kurse sich überlappen
                            foreach (var semesterCourseTime in semesterCourse.Times)
                            {
                                // Zeitspanne des Semester-Kurses
                                // z.B. 11:30 - 9:00 = 1,5h
                                TimeSpan semesterCourseSpan = semesterCourseTime.To - semesterCourseTime.From;

                                // Prüfe WPV-Zeitspannen und vergleiche sie mit der Zeitspanne des Semester-Kurses
                                foreach (var wpvCourseTime in wpvCourse.Times)
                                {
                                    // Zeitspanne des WPV-Kurses
                                    // z.B. 11:00 - 10:00 = 1h
                                    TimeSpan wpvCourseSpan = wpvCourseTime.To - wpvCourseTime.From;


                                    // Finde heraus, welche From-Zeit (Beginn des Kurses) kleiner ist (Semester oder WPV?)
                                    DateTime fromEarlier;
                                    DateTime fromLater;

                                    // Zeitspanne des früher beginnenden Kurses
                                    TimeSpan span;


                                    // Ordne from1 dem früher beginnenden Kurs zu
                                    // Ordne from2 dem später beginnenden Kurs zu
                                    // Ordne die Zeitspanne derjenigen des früher beginnenden Kurses zu
                                    if (semesterCourseTime.From < wpvCourseTime.From)
                                    {
                                        fromEarlier = semesterCourseTime.From;
                                        fromLater = wpvCourseTime.From;

                                        span = semesterCourseSpan;
                                    }
                                    else
                                    {
                                        fromEarlier = wpvCourseTime.From;
                                        fromLater = semesterCourseTime.From;

                                        span = wpvCourseSpan;
                                    }


                                    // Addiere Beginn des früher beginnenden Kurses mit dessen Zeitspanne
                                    DateTime endTime = fromEarlier + span;


                                    // Liegt die Endzeit des früher beginnenden Kurses zwischen
                                    // Anfang und Ende des später beginnenden Kurses?
                                    if (endTime >= fromLater)
                                    {
                                        // Finde Index innerhalb der Kopie
                                        int index = IndexOfCourse(wpv, wpvCourse);

                                        // -1 heißt, dass der Kurs nicht (mehr) in der Kopie enthalten ist.
                                        // Falls enthalten wird der Kurs aus der Kopie gelöscht.
                                        if (index > -1)
                                        {
                                            wpv.RemoveAt(index);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            PrintCourses(wpv);
        }


        // Finde den Index eines Kurses der originalen Liste innerhalb der Kopie der Liste
        private static int IndexOfCourse(List<Course> courses, Course courseToCheck)
        {
            // Laufe alle Kurse der originalen Liste per Index durch
            // i = 0 bis Anzahl der Kurse
            for (int i = 0; i < courses.Count; i++)
            {
                // Hole Kurs aus Index
                Course course = courses[i];

                // Prüfe ob Kurs die exakt selben Informationen hat, wie der aus der Kopie
                if (course.IsSame(courseToCheck))
                {
                    // Falls ja, gebe den Index zurück
                    return i;
                }
            }

            // Kurs wurde nicht gefunden, gebe -1 als Fehler zurück
            return -1;
        }
    }
}
