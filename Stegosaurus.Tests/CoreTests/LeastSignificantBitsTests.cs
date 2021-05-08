using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var outputPath = @"C:\Users\slawek\Downloads\encoded.png";
            var message = "Hello there";
            using (var leastSignificantBits = new LeastSignificantBits(filePath))
            {
                leastSignificantBits.Encode(outputPath, message);
                Assert.Equal(message, leastSignificantBits.Decode());
            }

            using (var decoder = new LeastSignificantBits(outputPath))
            {
                var result = decoder.Decode();
                Assert.Equal(message, result);
            }
        }
    }
}
