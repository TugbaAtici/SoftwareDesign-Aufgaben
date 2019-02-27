using System.Collections.Generic;
using System.Text;

namespace L4_Stundenplan.Data
{
    public class WPVSession
    {
        public WPV WPV {get; set;}

        public Room Room {get; set;}
        
        public List<Block> Blocks {get; private set;} = new List<Block>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var block in Blocks)
            {
                sb.Append(block.ToString());
            }

            return $"{WPV.ToString()}\nRaum: {Room.ToString()}\n{string.Join(", ", sb)}";
        }
    }
}
