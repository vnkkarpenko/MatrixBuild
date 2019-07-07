using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MatrixBuild.Interfaces
{
    public interface IConvertService
    {
        MemoryStream MatrixToStream(int[,] data);
        int[,] FileToMatrix(Stream stream);
    }
}
