using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickSortByKrovushka
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod3()
        {
            var array = new double[3] { 3, 1, 2 };
            ProgramQuickSort.QuickSort(array, 0, array.Length - 1);

            Assert.IsTrue(array[0] < array[1]);
            Assert.IsTrue(array[1] < array[2]);
        }

        [TestMethod]
        public void TestMethod100()
        {
            var array = new double[100];
           
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 9;
            }
            Assert.IsTrue(array[5] == array[10]);
        }
        [TestMethod]
        public void TestMethod1000()
        {
            var array = new double[1000];
            var rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(0, 1000);
            }

            ProgramQuickSort.QuickSort(array, 0, array.Length - 1);

            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(array[rand.Next(0,300)] < array[rand.Next(300, 1000)]);
            }
        }

        [TestMethod]
        public void TestMethodEmpity()
        {
            var array = new double[10];

            ProgramQuickSort.QuickSort(array, 0, array.Length - 1);

            Assert.IsTrue(array[0] == array[1]);
        }
    }
}
