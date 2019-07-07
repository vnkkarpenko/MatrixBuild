using MatrixBuild.Controllers;
using MatrixBuild.Logic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixBuild.Tests.Controllers
{
    [TestClass]
    public class MatrixApiControllerTests
    {
        [TestMethod]
        public void GetRandomMatrixTest()
        {
            var mock = new MatrixService();
            var controller = new MatrixApiController(mock, new ConvertService());
            
            // Act
            var result = controller.GetRandomMatrix();
            var currentMatrix = mock.GetCurrentMatrix();

            // Assert
            Assert.AreEqual(result, currentMatrix);
        }

        [TestMethod]
        public void GetRotateMatrix()
        {
            var mock = new MatrixService();
            var controller = new MatrixApiController(mock, new ConvertService());
            
            // Act
            var result = controller.GetRotateMatrix();
            var currentMatrix = mock.GetCurrentMatrix();

            // Assert
            Assert.AreEqual(result, currentMatrix);
        }
    }
}