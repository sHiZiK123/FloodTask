using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_task {
    class Flood {
        static Matrix matrix;
        static List<Wall> allWalls;
        static List<Point> allPoints;
        int indexAfterValue;
        int indexAfterArgument;
        int indexBeforValue;
        int indexBeforAargument;

        public Flood(List<Wall> _allWalls, List<Point> _allPoints, Matrix _matrix){
            matrix = _matrix;
            allWalls = _allWalls;
            allPoints = _allPoints;
        }

        void FloodRightAndLeftWalls(int max_y, int min_y, int i) {
            for (int p = min_y + 1; p < max_y; p++) {
                if (matrix.GetMatrix[p, indexAfterValue].CellState == States.Water &&
                    (matrix.GetMatrix[p, indexBeforValue].CellState == States.Air 
                    || matrix.GetMatrix[p, indexBeforValue].CellState == States.Wall)) {
                    for (int j = min_y + 1; j < max_y; j++) {
                        matrix.GetMatrix[j, allPoints[allWalls[i].FirstPoint].X].CellState = States.Water;
                    }
                }
                if (matrix.GetMatrix[p, indexBeforValue].CellState == States.Water
                    && (matrix.GetMatrix[p, indexAfterValue].CellState == States.Air
                    || matrix.GetMatrix[p, indexAfterValue].CellState== States.Wall)) {
                    for (int j = min_y + 1; j < max_y; j++) {
                        matrix.GetMatrix[j, allPoints[allWalls[i].FirstPoint].X].CellState = States.Water;
                    }
                }
            }
        }

        void FloodTopAndBottomWalls(int max_x, int min_x, int i) {
            for (int p = min_x + 1; p < max_x; p++) {
                if (matrix.GetMatrix[indexBeforAargument, p].CellState == States.Water
                    && (matrix.GetMatrix[indexAfterArgument, p].CellState == States.Air
                    || matrix.GetMatrix[indexAfterArgument, p].CellState == States.Wall)) {
                    for (int j = min_x + 1; j < max_x; j++) {
                        matrix.GetMatrix[allPoints[allWalls[i].FirstPoint].Y, j].CellState = States.Water;
                    }
                }

                if (matrix.GetMatrix[indexAfterArgument, p].CellState == States.Water
                    && (matrix.GetMatrix[indexBeforAargument, p].CellState == States.Air
                    || matrix.GetMatrix[indexBeforAargument, p].CellState == States.Wall)) {
                    for (int j = min_x + 1; j < max_x; j++) {
                        matrix.GetMatrix[allPoints[allWalls[i].FirstPoint].Y, j].CellState = States.Water;
                    }
                }
            }
        }

        public void FloodWalls() {
            for (int i = 0; i < allWalls.Count; i++) {
                Point tmp = allPoints[allWalls[i].FirstPoint];
                indexAfterValue = tmp.X + 1;
                indexAfterArgument = tmp.Y + 1;
                indexBeforValue = tmp.X - 1;
                indexBeforAargument = tmp.Y - 1;

                int min_y = 0;
                int max_y = 0;
                int min_x = 0;
                int max_x = 0;

                allWalls[i].GetMaxAndMinArgumets(ref max_y, ref min_y, allPoints);
                allWalls[i].GetMaxAndMinValues(ref max_x, ref min_x, allPoints);

                FloodRightAndLeftWalls(max_y, min_y, i);
                FloodTopAndBottomWalls(max_x, min_x, i);
            }
        }

        public void FloodAir() {
            for (int i = 0; i < allWalls.Count; i++) {
                int min_y = 0;
                int max_y = 0;
                allWalls[i].GetMaxAndMinArgumets(ref max_y, ref min_y, allPoints);

                for (int j = min_y + 1; j < max_y; ++j) {
                    if (matrix.GetMatrix[j, allPoints[allWalls[i].FirstPoint].X].CellState == States.Water) {
                        for (int k = allPoints[allWalls[i].FirstPoint].X; k < matrix.GetMaxY; k++) {
                            if (matrix.GetMatrix[j, k].CellState == States.Wall) 
                                break;
                            matrix.GetMatrix[j, k].CellState = States.Water;
                        }
                        for (int k = allPoints[allWalls[i].FirstPoint].X; k > 0; k--) {
                            if (matrix.GetMatrix[j, k].CellState == States.Wall) break;
                            matrix.GetMatrix[j, k].CellState = States.Water;
                        }
                    }
                }
            }
        }

        public void FindWallsNumbers() {
            for (int i = 0; i < allWalls.Count; i++) {
                int min_y = 0;
                int max_y = 0;
                int min_x = 0;
                int max_x = 0;
                bool floded = false;

                allWalls[i].GetMaxAndMinArgumets(ref max_y, ref min_y, allPoints);
                allWalls[i].GetMaxAndMinValues(ref max_x, ref min_x, allPoints);

                if (allPoints[allWalls[i].FirstPoint].X == allPoints[allWalls[i].SecondPoint].X) {
                    for (int j = min_y; j < max_y; j++) {
                        if (matrix.GetMatrix[j, allPoints[allWalls[i].FirstPoint].X].CellState == States.Water) 
                            floded = true;
                    }
                    if (!floded) Console.Write("{0} ", i + 1);
                }
                floded = false;
                if (allPoints[allWalls[i].FirstPoint].Y == allPoints[allWalls[i].SecondPoint].Y) {
                    for (int j = min_x; j < max_x; j++) {
                        if (matrix.GetMatrix[allPoints[allWalls[i].FirstPoint].Y, j].CellState == States.Water) 
                            floded = true;
                    }
                    if (!floded) Console.Write("{0} ", i + 1);
                }
            }
        }

        public bool IsAirInMatrix() {
            foreach (var parametr in matrix.GetMatrix)
                if (parametr.CellState == States.Air) return true;
            return false;
        }
    }
}
