using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_task {
    class Wall {
        int firstPoint;
        int secondPoint;

        public int FirstPoint { get { return firstPoint; } set { firstPoint = value; } }
        public int SecondPoint { get { return secondPoint; } set { secondPoint = value; } }

        public void GetMaxAndMinArgumets(ref int max_y, ref  int min_y, List<Point> allPoints) {
            if (allPoints[firstPoint].X == allPoints[secondPoint].X)
                if (allPoints[firstPoint].Y > allPoints[secondPoint].Y) {
                    min_y = allPoints[secondPoint].Y;
                    max_y = allPoints[firstPoint].Y;
                }
                else {
                    min_y = allPoints[firstPoint].Y;
                    max_y = allPoints[secondPoint].Y;
                }
        }

        public void GetMaxAndMinValues(ref int max_x, ref  int min_x, List<Point> allPoints) {
            if (allPoints[firstPoint].Y == allPoints[secondPoint].Y)
                if (allPoints[firstPoint].X > allPoints[secondPoint].X) {
                    min_x = allPoints[secondPoint].X;
                    max_x = allPoints[firstPoint].X;
                }
                else {
                    min_x = allPoints[firstPoint].X;
                    max_x = allPoints[secondPoint].X;
                }
        }
    }
}
