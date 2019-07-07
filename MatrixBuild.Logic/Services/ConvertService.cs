using System.Collections.Generic;
using System.IO;
using MatrixBuild.Interfaces;

namespace MatrixBuild.Logic.Services
{
    public class ConvertService: IConvertService
    {
        public MemoryStream MatrixToStream(int[,] data)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            for (var e = 0; e <= data.GetUpperBound(data.Rank - 2); e++)
            {
                var content = "";
                for (var r = 0; r <= data.GetUpperBound(data.Rank - 1); r++)
                {
                    content += data[e, r].ToString() + ";";
                }

                writer.WriteLine(content);
            }

            writer.Flush();
            stream.Position = 0;

            return stream;
        }
        
        public int[,] FileToMatrix(Stream stream)
        {
            var data = new List<string[]>();
            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine()?.Split(';');
                    data.Add(line);
                }
            }

            var colCount = data[0].Length-1;
            var rowCount = data.Count;

            var matrix = new int[rowCount, colCount];
            for (var a = 0; a < rowCount; a++)
            {
                for (var r = 0; r < colCount; r++)
                {
                    int.TryParse($"{data[a][r]}", out var newval);
                    matrix[a, r] = newval;
                }
            }

            return matrix;
        }
    }
}
