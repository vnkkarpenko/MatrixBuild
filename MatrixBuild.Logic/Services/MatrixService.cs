using System;
using MatrixBuild.Interfaces;

namespace MatrixBuild.Logic.Services
{
    public sealed class MatrixService: IMatrix
    {
        private static int[,] MatrixData { get; set; }

        public int[,] GetCurrentMatrix()
        {
            return MatrixData;
        }

        public int[,] SetAndGetCurrentMatrix(int[,] data)
        {
            MatrixData = data;

            return GetCurrentMatrix();
        }

        public void RandomMatrix(int row = 5, int col = 5)
        {
            const int lower = 1;
            const int upper = 9;

            var matrix = new int[row, col];
            var rand = new Random();

            for (var a = 0; a < row; a++)
            {
                for (var b = 0; b < col; b++)
                {
                    matrix[a, b] = rand.Next(lower, upper+1);
                }
            }

            SetAndGetCurrentMatrix(matrix);
        }

        public int[,] RotateAndGetMatrix()
        {
            var width = MatrixData.GetLength(0);
            var height = MatrixData.GetLength(1);
            var nMatrix = new int[height, width];
            var nRow = 0;

            for (var a = width - 1; a >= 0; a--)
            {
                for (var b = 0; b < height; b++)
                {
                    nMatrix[b, nRow] = MatrixData[a, b];
                }
                nRow++;
            }

            return SetAndGetCurrentMatrix(nMatrix);
        }
    }
}
