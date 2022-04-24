using Stegosaurus.Core.LeastSignificantBits;
using Xunit;

namespace Stegosaurus.Tests.CoreTests
{
    public class LeastSignificantBitsTests
    {
        [Fact]
        public void EncodingTest()
        {
            var filePath = @"C:\Users\slawek\Downloads\sample.png";
            var message = "Hello there";

            using var leastSignificantBits = new LeastSignificantBits(filePath);
            leastSignificantBits.Encode(message);
            var decoded = leastSignificantBits.Decode();

            Assert.Equal(message, decoded);
        }
    }
}
