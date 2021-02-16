using NUnit.Framework;


namespace WordAppTests
{
    [TestFixture]
    public class HashFunctionsTests
    {
        [Test]        
        public void HashTest_IsItWorking()
        {
            //Arrange
            string input = "1234";
            //Act
            string result = WordApp.HashingFunctions.Hash(input);
            //Assert
            Assert.IsNotNull(result);           
        }
        [Test]
        public void HashTest_IsItNotReturnigTheSameString()
        {
            //Arrange
            string input = "1234";
            //Act
            string result = WordApp.HashingFunctions.Hash(input);
            //Assert
            Assert.AreNotSame(input, result);
        }
    }
}
