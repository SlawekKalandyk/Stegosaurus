using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Stegosaurus.Core
{
    public interface IStenographyMethod
    {
        public Image<Rgba32> Encode(string message);
        public string Decode();
    }
}
