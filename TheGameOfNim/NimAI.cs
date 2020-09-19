using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimGame
{
    public class NimAi
    {
        public NimAi() { }

        public void GetMove(heap[] Heaps, out int HeapIndex, out int ObjectsToRemove)
        {
            HeapIndex = FindIndexOfMax(Heaps);
            ObjectsToRemove = Heaps[HeapIndex].Count - ExclusiveOr(Heaps[HeapIndex].Count, NimSum(Heaps));

            if (ObjectsToRemove == 0)
                ObjectsToRemove = 1;
        }

        private int FindIndexOfMax(heap[] Heaps)
        {
            int Max = -1;
            int MaxIndex = 0;

            for (int i = 0; i < Heaps.Length; i++)
            {
                if (Heaps[i].Count > Max)
                {
                    Max = Heaps[i].Count;
                    MaxIndex = i;
                }
            }

            return MaxIndex;
        }

        private int NimSum(heap[] Heaps)
        {
            int nimsum = 0;

            foreach (heap heap in Heaps)
            {
                nimsum = ExclusiveOr(nimsum, heap.Count);
            }

            return nimsum;
        }

        private int ExclusiveOr(int numOne, int numTwo)
        {
            int[] BinaryIntOne = BinaryConverter.IntToBinary(numOne);
            int[] BinaryIntTwo = BinaryConverter.IntToBinary(numTwo);
            int length = Math.Min(BinaryIntOne.Length, BinaryIntTwo.Length);
            int Maxlength = Math.Max(BinaryIntOne.Length, BinaryIntTwo.Length);
            int[] result = new int[Maxlength];

            for (int i = 0; i < length; i++)
            {
                // If they are both 0 or both 1, the result must be 0.
                if ((BinaryIntOne[i] == 1 && BinaryIntTwo[i] == 1) ||
                   (BinaryIntOne[i] == 0 && BinaryIntTwo[i] == 0))
                {
                    result[i] = 0;
                }
                else // One bit is 1, the other bit is 0.
                {
                    result[i] = 1;
                }
            }

            if (length < Maxlength)
            {
                int[] longest;
                if (BinaryIntOne.Length > BinaryIntTwo.Length)
                    longest = BinaryIntOne;
                else
                    longest = BinaryIntTwo;

                for (int i = length; i < Maxlength; i++)
                    result[i] = longest[i];
            }

            return BinaryConverter.BinaryToInteger(result);
        }
    }
}
