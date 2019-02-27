using System.Collections.Generic;
using System.Text;

namespace L4_Stundenplan.Data
{
    public class Session
    {
        public Course Course {get; set;}

        public Room Room {get; set;}
        
        public List<Block> Blocks {get; private set;} = new List<Block>();

        public void Reset()
        {
            foreach (var block in Blocks)
            {
                block.IsOccupied = false;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var block in Blocks)
            {
                sb.Append(block.ToString());
            }

            return $"{Course.ToString()}\nRaum: {Room.ToString()}\n{string.Join(", ", sb)}";
        }
    }
}
