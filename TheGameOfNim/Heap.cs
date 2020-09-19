using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimGame
{
    public class heap
    {
        private int count;

        public heap()
        {
            count = 0;
        }

        public heap(int count)
        {
            Count = count;
        }

        public int Count
        {
            get { return count; }
            set
            {
                if (value >= 0)
                    count = value;
                else
                    throw new ArgumentException(
                        String.Format("Count must be positive and less than {0}", int.MaxValue));
            }
        }

        // Return the integer to binary count.
        public int[] Binary
        {
            get { return BinaryConverter.IntToBinary(count); }
        }

        public override string ToString()
        {
            int[] binary = Binary;
            int PositionOfFirstOne = -1;
            StringBuilder builder = new StringBuilder();
            string output;

            // Find the first 1 in order to achieve minimum length.
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == 1)
                {
                    PositionOfFirstOne = i;
                    break;
                }
            }

            // Place the bits in a string whilst making sure there's at least a 0 in there.
            if (PositionOfFirstOne >= 0)
            {
                for (int i = PositionOfFirstOne; i < binary.Length; i++)
                {
                    builder.Append(binary[i]);
                }
            }
            else
                builder.Append(0);

            // Insert plenty of spaces so it can be read properly.
            for (int i = builder.Length - 4; i > 0; i -= 4)
                builder.Insert(i, " ");


            return builder.ToString();
        }
    }
}
