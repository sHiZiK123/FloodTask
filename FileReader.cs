using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_task {
    class FileReader {
        int pointsNumber=0;
        int wallsNumber=0;
        List<Point> allPoints = new List<Point>();
        List<Wall> allWalls = new List<Wall>();
        string fileName;

        public string FileName { get { return fileName; } set { fileName = value; } }
        public List<Point> GetListOfPoints { get { return allPoints; } }
        public List<Wall> GetListOfWall { get { return allWalls; } }

        public void ReadPointsAndWals() {
            StreamReader sr = File.OpenText(fileName);
            string[] arrString;

            pointsNumber = int.Parse(sr.ReadLine());
            for (int i = 1; i <= pointsNumber; i++) {
                Point pt = new Point();
                arrString = sr.ReadLine().Split(',');
                pt.X = Int32.Parse(arrString[0]);
                pt.Y = Int32.Parse(arrString[1]);
                allPoints.Add(pt);
            }

            wallsNumber = int.Parse(sr.ReadLine());
            for (int i = 1; i <= wallsNumber; i++) {
                Wall wl = new Wall();
                arrString = sr.ReadLine().Split(',');
                wl.FirstPoint = Int32.Parse(arrString[0]) - 1;
                wl.SecondPoint = Int32.Parse(arrString[1]) - 1;
                allWalls.Add(wl);
            }
        }
    }
}
