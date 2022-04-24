using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;

namespace Stegosaurus.Core
{
    public abstract class BaseStenographyMethod : IStenographyMethod, IDisposable
    {
        protected const string BinaryMessageEnd = "00000000";

        protected readonly Stream _imageFile;
        protected readonly Image<Rgba32> _image;

        protected BaseStenographyMethod(string filePath) : this(new FileStream(filePath, FileMode.Open, FileAccess.Read))
        { }

        protected BaseStenographyMethod(Stream imageFile)
        {
            _imageFile = imageFile;
            _image = Image.Load<Rgba32>(_imageFile);
        }

        public void Encode(string outputPath, string message)
        {
            var encodedImage = Encode(message);
            encodedImage.Save(outputPath);
        }

        public abstract Image<Rgba32> Encode(string message);
        public abstract string Decode();

        public virtual void Dispose()
        {
            _image?.Dispose();
            _imageFile?.Dispose();
        }
    }
}
