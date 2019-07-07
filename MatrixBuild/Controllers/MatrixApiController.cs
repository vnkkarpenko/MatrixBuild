using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using MatrixBuild.Interfaces;

namespace MatrixBuild.Controllers
{
    public class MatrixApiController : ApiController
    {
        private readonly IMatrix _matrixservce;
        private readonly IConvertService _convertservce;

        public MatrixApiController(IMatrix matrixservce, IConvertService convertservce)
        {
            _matrixservce = matrixservce;
            _convertservce = convertservce;
        }
  
        [HttpGet]
        [Route("api/GetRandomMatrix/{row:int}/{col:int}")]
        public int[,] GetRandomMatrix(int row = 5, int col = 5)
        {
            _matrixservce.RandomMatrix(row, col);

            return _matrixservce.GetCurrentMatrix();
        }

        [HttpGet]
        [Route("api/GetRotateMatrix")]
        public int[,] GetRotateMatrix()
        {
            return _matrixservce.RotateAndGetMatrix();
        }

        [HttpPost]
        [Route("api/PostImportMatrix")]
        public int[,] ImportMatrix()
        {
            var file = HttpContext.Current.Request.Files["fileUpload"];
            var matrix = _convertservce.FileToMatrix(file?.InputStream);

            return _matrixservce.SetAndGetCurrentMatrix(matrix);
        }

        [HttpPost]
        [Route("api/PostExportMatrix")]
        public HttpResponseMessage ExportMatrix()
        {
            var currMatrix = _matrixservce.GetCurrentMatrix();
            var matrixToStream = _convertservce.MatrixToStream(currMatrix);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(matrixToStream)
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {FileName = "MatrixExport.csv"};

            return result;
        }
    }
}