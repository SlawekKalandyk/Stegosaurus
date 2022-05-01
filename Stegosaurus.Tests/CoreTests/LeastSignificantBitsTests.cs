using Stegosaurus.Core.LeastSignificantBits;
using Xunit;

namespace Stegosaurus.Tests.CoreTests
{
    public class LeastSignificantBitsTests
    {
        [Fact]
        public void EncodingTest()
        {
            var filePath = @"..\..\..\TestImages\sample.png";
            var message = "Hello there";

            using var leastSignificantBits = new LeastSignificantBits(filePath);
            leastSignificantBits.Encode(message);
            var decodedMessage = leastSignificantBits.Decode();

            Assert.Equal(message, decodedMessage);
        }
    }
}
