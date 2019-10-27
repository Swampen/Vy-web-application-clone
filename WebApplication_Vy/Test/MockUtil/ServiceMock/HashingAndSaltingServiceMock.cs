using Moq;
using UTILS.Utils.Auth;

namespace Test.MockUtil.ServiceMock
{
    public class HashingAndSaltingServiceMock
    {
        public static HashingAndSaltingService GenerateSaltedHashMock()
        {
            var mockService = new Mock<HashingAndSaltingService>();
            mockService.Setup(mock => mock.GenerateSaltedHash(It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(new byte[12]);
            return mockService.Object;
        }
        
    }
}