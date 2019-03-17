using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSortByKrovushka
{
    public class ProgramQuickSort
    {
        static void Main(string[] args)
        {
        }

        public static void QuickSort(double[] array, long start, long end)
        {
            double middle = array[(end - start) / 2 + start];// ищем средний элемент 
            double temp;
            long i = start, j = end;
            while (i <= j)
            {
                while (array[i] < middle && i <= end) ++i;
                while (array[j] > middle && j >= start) --j;
                if (i <= j)
                {
                    temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    ++i; --j;
                }
            }
            if (j > start) QuickSort(array, start, j);
            if (i < end) QuickSort(array, i, end);
        }
    }
}
