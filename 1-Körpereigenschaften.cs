using System;

namespace A01_Koerpereigenschaften
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if enough arguments are provided.
            if (args.Length == 2)
            {
                string geometricShape = args[0];
                double size;

                // Check if the size is a parseable double, if so, store it in the variable size.
                if (double.TryParse(args[1], out size))
                {
                    switch (geometricShape)
                    {
                        case "w":
                            Console.WriteLine(GetCubeInfo(size));
                            break;
                        case "k":
                            Console.WriteLine(GetSphereInfo(size));
                            break;
                        case "o":
                            Console.WriteLine(GetOctahedronInfo(size));
                            break;
                        default:
                            Console.WriteLine("Kein g�ltiger K�rper.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Keine g�ltige Kantenl�nge/kein g�ltiger Durchmesser.");
                }
            }
            else
            {
                Console.WriteLine("Die Zahl der Argumente ist ung�ltig.");
            }
        }

        static double GetCubeSurface(double edgeLength)
        {
            return 6 * Math.Pow(edgeLength, 2);
        }
        static double GetCubeVolume(double edgeLength)
        {
            return Math.Pow(edgeLength, 3);
        }
        static string GetCubeInfo(double edgeLength)
        {
            return "W�rfel: A=" + Math.Round(GetCubeSurface(edgeLength), 2) + " | V=" + Math.Round(GetCubeVolume(edgeLength), 2);
        }

        static double GetSphereSurface(double diameter)
        {
            return Math.PI * Math.Pow(diameter, 2);
        }
        static double GetSphereVolume(double diameter)
        {
            return Math.PI * Math.Pow(diameter, 3) / 6;
        }
        static string GetSphereInfo(double diameter)
        {
            return "Kugel: A=" + Math.Round(GetSphereSurface(diameter), 2) + " | V=" + Math.Round(GetSphereVolume(diameter), 2);
        }

        static double GetOctahedronSurface(double edgeLength)
        {
            return 2 * Math.Sqrt(3) * Math.Pow(edgeLength, 2);
        }
        static double GetOctahedronVolume(double edgeLength)
        {
            return Math.Sqrt(2) * Math.Pow(edgeLength, 3) / 3;
        }
        static string GetOctahedronInfo(double edgeLength)
        {
            return "Oktaeder: A=" + Math.Round(GetOctahedronSurface(edgeLength), 2) + " | V=" + Math.Round(GetOctahedronVolume(edgeLength), 2);
        }
    }//beispiel
}