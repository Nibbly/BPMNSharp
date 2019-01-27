using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BPMN;

namespace ReferenceVsValue
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = Model.Read(@"C:\Users\Fabian\Desktop\Coding Projects\BPMNSharp\Diagram\diagram.bpmn");

            for (int h = 0; h < model.Diagrams.Count; h++)
            {
                var dia = model.Diagrams[h];

                for (int m = 0; m < dia.Planes.Count; m++)
                {
                    var plane = dia.Planes[m];

                    for (int i = 0; i < plane.Shapes.Count; i++)
                    {
                        var currentShape = plane.Shapes[i];

                        for (int j = 0; j < currentShape.Bounds.Count; j++)
                        {
                            var tempRec = currentShape.Bounds[j];
                            tempRec.X = 15;
                            currentShape.Bounds[j] = tempRec;
                        }
                    }
                }
            }

            //DisplayRecList(recList);


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

        public static void DisplayRecList(List<Rectangle> recList)
        {
            for (int i = 0; i < recList.Count; i++)
            {
                var tempRec = recList[i];

                Console.WriteLine($"List-Hash: {recList.GetHashCode()}");

                Console.WriteLine($"Rec {i} (old): {recList[i].ToString()}\tHash: {recList[i].GetHashCode()}");

                recList[i] = tempRec;
            
                Console.WriteLine($"Rec {i} (new): {recList[i].ToString()}\tHash: {recList[i].GetHashCode()}\n");
            }
        }
    }
}