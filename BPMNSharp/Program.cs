using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ReferenceVsValue
{
    class Program
    {
        static void Main(string[] args)
        {
            var recList = ReturnRecList();
            var query = (from rec in recList
                         select rec).ToList();

            var equal = recList == query;

            DisplayRecList(true, query);
            DisplayRecList(false, recList);

            Console.ReadLine();
        }

        public static List<Rectangle> ReturnRecList()
        {
            var rec1 = new Rectangle() { X = 100, Y = 200 };
            var rec2 = new Rectangle() { X = 999, Y = 333 };
            List<Rectangle> recList = new List<Rectangle>();
            recList.Add(rec1);
            recList.Add(rec2);

            return recList;
        }

        public static void DisplayRecList(bool offset, List<Rectangle> recList)
        {
            for (int i = 0; i < recList.Count; i++)
            {
                var tempRec = recList[i];
                var compHashes = tempRec.GetHashCode() == recList[i].GetHashCode();

                Console.WriteLine($"List-Hash: {recList.GetHashCode()}");

                Console.WriteLine($"Rec {i} (old): {recList[i].ToString()}\tHash: {recList[i].GetHashCode()}");

                if (offset)
                    tempRec.Offset(10, 1);

                compHashes = tempRec.GetHashCode() == recList[i].GetHashCode();

                recList[i] = tempRec;
                compHashes = tempRec.GetHashCode() == recList[i].GetHashCode();

                Console.WriteLine($"Rec {i} (new): {recList[i].ToString()}\tHash: {recList[i].GetHashCode()}\n");
            }
        }
    }

    public class Plane
    {
        public List<Shape> Shapes { get; set; }
    }

    public class Shape
    {
        public List<Rectangle> Bounds { get; set; }
    }
}