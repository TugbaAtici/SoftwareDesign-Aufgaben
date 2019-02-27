using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;


namespace L4_Stundenplan.Data
{
    public class Block
    {
        // DateTime vor dem Bindestrich (-)
        public DateTime Start { get; private set; }

        // DateTime nach dem Bindestrich (-)
        public DateTime End { get; private set; }

        public int DayNumber { get; private set; }
        public string DayString { get; private set; }

        public int BlockNumber {get; private set;}

        public bool IsOccupied {get; set;}


        // Handelt es sich um einen leeren String,
        // wird die Zeit in 00:00-00:00 umgewandelt (Fallback).
        public Block(int dayNumber, int blockNumber, string blockString)
        {
            if (string.IsNullOrEmpty(blockString))
            {
                Start = StringToTime("00:00");
                End = StringToTime("00:00");
            }
            else
            {
                string[] range = blockString.Split('-');

                Start = StringToTime(range[0]);
                End = StringToTime(range[1]);
            }

            DayNumber = dayNumber;
            DayString = DayNumberToString(dayNumber);

            BlockNumber = blockNumber;
        }

        public override string ToString()
        {
            string startString = Start.ToShortTimeString();
            string endString = End.ToShortTimeString();

            return $"Block {BlockNumber} ({DayString}, {startString}-{endString} Uhr)";
        }


        // Eigentliche Konvertierung eines Strings in ein DateTime-Objekt
        private static DateTime StringToTime(string dateTimeString, string format="HH:mm")
        {
            // Alle Leerzeichen entfernen
            string fixedDateTimeString = Regex.Replace(dateTimeString, @"\s+", "");

            // String als DateTime-Objekt parsen und zurückgeben
            return DateTime.ParseExact(fixedDateTimeString, format, CultureInfo.InvariantCulture);
        }

        public static List<Block> ConvertBlockStrings(List<string> blockStrings)
        {
            var blocks = new List<Block>();

            // 1-6 => Monday - Saturday
            for (int dayNumber = 1; dayNumber < 6; dayNumber++)
            {
                // Block 1-x
                for (int blockNumber = 1; blockNumber < blockStrings.Count; blockNumber++)
                {
                    var block = new Block(dayNumber, blockNumber, blockStrings[blockNumber - 1]);
                    blocks.Add(block);
                }
            }

            return blocks;
        }

        public static string DayNumberToString(int dayNumber)
        {
            var dayNumberMap = new Dictionary<int, string>()
            {
                {1, "Montag"},
                {2, "Dienstag"},
                {3, "Mittwoch"},
                {4, "Donnerstag"},
                {5, "Freitag"},
                {6, "Samstag"}
            };

            return dayNumberMap[dayNumber];
        }
    }
}
