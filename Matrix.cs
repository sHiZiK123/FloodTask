using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_task {
    class Matrix {
        static Cell[,] matrix;
        static int maxY;
        static int maxX;

        public int GetMaxX { get { return maxY; } }
        public int GetMaxY { get { return maxX; } }
        public Cell[,] GetMatrix { get { return matrix; } }

        public Matrix(int _maxX, int _maxY) {
            maxY = _maxX;
            maxX = _maxY;
            matrix = new Cell[maxY,maxX];

            for (int i = 0; i < maxY; i++)
                for (int j = 0; j < maxX; j++) {
                    matrix[i, j] = new Cell();
                    matrix[i, j].CellState = States.Air;
                }
        }

        void RefreshMaxValues(List<Point> allPoints) {
            maxY = allPoints.Max(a => a.Y) + 2;
            maxX = allPoints.Max(a => a.X) + 2;

            matrix = new Cell[maxY, maxX];

            for (int i = 0; i < maxY; i++)
                for (int j = 0; j < maxX; j++) {
                    matrix[i, j] = new Cell();
                    matrix[i, j].CellState = States.Air;
                }
        }

        void MakeWaterBorder() {
            for (int i = 0; i < maxY; i++)
                for (int j = 0; j < maxX; j++) {
                    if (i == 0 || j == 0 || j == maxX - 1 || i == maxY - 1) {
                        matrix[i, j].CellState = States.Water;
                    }
                }
        }
        
        public void BuildWalls(List<Wall> allWalls, List<Point> allPoints) {
            RefreshMaxValues(allPoints);
            foreach (var c in allWalls) {
                if (allPoints[c.FirstPoint].X == allPoints[c.SecondPoint].X) {
                    if (allPoints[c.FirstPoint].Y > allPoints[c.SecondPoint].Y) {
                        for (int i = allPoints[c.SecondPoint].Y; i <= allPoints[c.FirstPoint].Y; i++) {
                            matrix[i, allPoints[c.FirstPoint].X].CellState = States.Wall;
                        }
                    }

                    if (allPoints[c.FirstPoint].Y < allPoints[c.SecondPoint].Y)
                        for (int i = allPoints[c.FirstPoint].Y; i <= allPoints[c.SecondPoint].Y; i++) {
                            matrix[i, allPoints[c.FirstPoint].X].CellState = States.Wall;
                        }
                }


                if (allPoints[c.FirstPoint].Y == allPoints[c.SecondPoint].Y) {
                    if (allPoints[c.FirstPoint].X > allPoints[c.SecondPoint].X) {
                        for (int i = allPoints[c.SecondPoint].X; i <= allPoints[c.FirstPoint].X; i++) {
                            matrix[allPoints[c.FirstPoint].Y, i].CellState = States.Wall;
                        }
                    }

                    if (allPoints[c.FirstPoint].X < allPoints[c.SecondPoint].X)
                        for (int i = allPoints[c.FirstPoint].X; i <= allPoints[c.SecondPoint].X; i++) {
                            matrix[allPoints[c.FirstPoint].Y, i].CellState = States.Wall;
                        }
                }

            }
        }

        public void SeparationAdjacentWalls(List<Wall> allWalls, List<Point> allPoints) {
            for (int i = 0; i < allWalls.Count; i++) {
                int min_y = 0;
                int max_y = 0;
                int min_x = 0;
                int max_x = 0;

                allWalls[i].GetMaxAndMinArgumets(ref max_y, ref min_y, allPoints);
                allWalls[i].GetMaxAndMinValues(ref max_x, ref min_x, allPoints);

                for (int j = min_y + 1; j < max_y; j++) {
                    if (GetMatrix[j, allPoints[allWalls[i].FirstPoint].X + 1].CellState == States.Wall) {
                        for (int k = 0; k < allPoints.Count; k++) {
                            if (allPoints[k].X > allPoints[allWalls[i].FirstPoint].X) ++allPoints[k].X;
                        }
                    }
                    maxX = allPoints.Max(a => a.X) + 2;
                    maxY = allPoints.Max(a => a.Y) + 2;
                    BuildWalls(allWalls, allPoints);
                }
          
                for (int j = min_x + 1; j < max_x; j++) {
                    if (GetMatrix[allPoints[allWalls[i].FirstPoint].Y + 1, j].CellState == States.Wall) {
                        for (int k = 0; k < allPoints.Count; k++) {
                            if (allPoints[k].Y > allPoints[allWalls[i].FirstPoint].Y) ++allPoints[k].Y;
                        }
                    }
                    maxX = allPoints.Max(a => a.X) + 2;
                    maxY = allPoints.Max(a => a.Y) + 2;
                    BuildWalls(allWalls, allPoints);
                }

            }
            MakeWaterBorder();
        }

        public void DrowMatrix() {
            for (int i = 0; i < maxY; i++) {
                for (int j = 0; j < maxX; j++) {
                    if (matrix[i, j].CellState == States.Air) Console.Write("  ");
                    if (matrix[i, j].CellState == States.Wall) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("# ");
                        Console.ResetColor();
                    }
                    if (matrix[i, j].CellState == States.Water) {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("~ ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
