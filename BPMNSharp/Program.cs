using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BPMN;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;

namespace ReferenceVsValue
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = Model.Read(@"C:\Users\Fabian\Desktop\Coding Projects\BPMNSharp\Diagram\diagram.bpmn");

            var minX = ReturnMinValue(model, Coordinate.X);
            var minY = ReturnMinValue(model, Coordinate.Y);

            var moveX = -minX + 10;
            var moveY = -minY + 10;

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
                            tempRec.X += moveX;
                            tempRec.Y += moveY;
                            currentShape.Bounds[j] = tempRec;
                        }
                    }

                    for (int i = 0; i < plane.Edges.Count; i++)
                    {
                        var currentEdge = plane.Edges[i];

                        for (int j = 0; j < currentEdge.Points.Count; j++)
                        {
                            var tempEdge = currentEdge.Points[j];
                            tempEdge.X += moveX;
                            tempEdge.Y += moveY;
                            currentEdge.Points[j] = tempEdge;
                        }
                    }
                }
            }

            SaveAndOpenPicture(model);
        }

        private static void SaveAndOpenPicture(Model model)
        {
            Image img = model.GetImage(0, 2.0f);
            img.Save("diaNew.png", ImageFormat.Png);

            var filePath = @"C:\Users\Fabian\Desktop\Coding Projects\BPMNSharp\BPMNSharp\bin\Debug";
            bool pathOk = File.Exists(filePath);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = filePath,
                FileName = "diaNew.png"
            };

            Process.Start(startInfo);
        }

        public static int ReturnMinValue(Model model, Coordinate coordinate)
        {
            var min = -9999;

            var query = (from value in model.Diagrams
                        .SelectMany(x => x.Planes)
                        .SelectMany(y => y.Shapes)
                        .SelectMany(z => z.Bounds)
                         select value).ToList();

            if (coordinate == Coordinate.X)
            {
                min = (from x in query.Select(x => x.X)
                       select x).ToList().Min();

                return min;
            }

            if (coordinate == Coordinate.Y)
            {
                min = (from y in query.Select(y => y.Y)
                       select y).ToList().Min();

                return min;
            }

            return min;
        }

        public enum Coordinate
        {
            X = 1,
            Y = 2
        }
    }
}