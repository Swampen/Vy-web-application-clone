using BLL.Service.Contracts;
using Moq;

namespace Test.MockUtil.ServiceMock
{
    public static class ZipSearchServiceMock
    {
        public static IZipSearchService GetPostalTownMock()
        {
            var mockService = new Mock<IZipSearchService>();
            mockService.Setup(mock => mock.GetPostaltown("0000")).Returns("test");
            return mockService.Object;
        }
    }
}