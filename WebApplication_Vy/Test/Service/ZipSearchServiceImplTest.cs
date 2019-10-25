using BLL.Service.Implementation;
using NUnit.Framework;
using Test.MockUtil.RepositoryMock;
using Test.MockUtil.ServiceMock;

namespace Test.Service
{
    [TestFixture]
    public class ZipSearchServiceImplTest
    {
        [Test]
        [TestCase("0")]
        [TestCase("00")]
        [TestCase("000")]
        [TestCase("00000")]
        [TestCase("00x0")]
        [TestCase("xxxx")]
        public void GetPostalTown_invalidStringShouldReturnEmptyString(string value)
        {
            //Arrange
            var zipService = new ZipSearchServiceImpl(VyRepositoryMock.FindZipCodeMock());
            
            //Act
            var actual = zipService.GetPostaltown(value);
            
            //Assert
            Assert.AreEqual("", actual);
        }

        [Test]
        public void GetPostalTown_nullValueShouldReturnEmptyString()
        {
            //Arrange
            var zipService = new ZipSearchServiceImpl(VyRepositoryMock.FindZipCodeMock());
            
            //Act
            var actual = zipService.GetPostaltown("1234");
            
            //Assert
            Assert.AreEqual("", actual);
        }

        [Test]
        [TestCase("0000")]
        [TestCase("9999")]
        public void GetPostalTown_shouldReturnZipcodeString(string value)
        {
            //Arrange
            var zipService = new ZipSearchServiceImpl(VyRepositoryMock.FindZipCodeMock());
            
            //Act
            var actual = zipService.GetPostaltown(value);
            
            //Assert
            Assert.AreEqual("Gjerdrum", actual);
        }
    }
}