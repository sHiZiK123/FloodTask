using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Offline_task {
    class Program {
        static void Main(string[] args) {
            List<Point> allPoints = new List<Point>();
            List<Wall> allWalls = new List<Wall>();

            FileReader fileReader = new FileReader();
            fileReader.FileName = "points.txt";
            fileReader.ReadPointsAndWals();
            allWalls = fileReader.GetListOfWall;
            allPoints = fileReader.GetListOfPoints;  
            int maxY = allPoints.Max(a => a.Y) + 2;
            int maxX = allPoints.Max(a => a.X) + 2;
            Matrix matrix = new Matrix(maxY, maxX);
            Flood flood = new Flood(allWalls,allPoints,matrix);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t FLOOD\n");

            matrix.BuildWalls(allWalls, allPoints);
            matrix.SeparationAdjacentWalls(allWalls, allPoints);
            matrix.DrowMatrix();
                
            do {
                    flood.FloodWalls();
                    flood.FloodAir();
                    Console.WriteLine(Environment.NewLine);
                    matrix.DrowMatrix();
                    flood.FindWallsNumbers();
                   
                } while (flood.IsAirInMatrix());

                Console.ReadLine();
        }
    }
}
