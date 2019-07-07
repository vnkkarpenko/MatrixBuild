namespace MatrixBuild.Interfaces
{
    public interface IMatrix
    {
        int[,] GetCurrentMatrix();
        int[,] SetAndGetCurrentMatrix(int[,] data);
        int[,] RotateAndGetMatrix();

        void RandomMatrix(int row, int col);
    }
}
