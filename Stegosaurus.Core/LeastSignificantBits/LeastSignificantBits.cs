using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Stegosaurus.Core.Extensions;
using System;
using System.IO;

namespace Stegosaurus.Core.LeastSignificantBits
{
    /// <summary>
    /// Requires lossless compression format (e.g. png).
    /// Hides a message inside chosen image using every color channel (R, G, B). A single pixel can contain 3 bits of information.
    /// Binary '00000000' is appended to mark the end of message.
    /// </summary>
    public class LeastSignificantBits : BaseStenographyMethod
    {
        public LeastSignificantBits(string filePath) : base(filePath)
        { }

        public LeastSignificantBits(Stream imageFile) : base(imageFile)
        { }

        public override Image<Rgba32> Encode(string message)
        {
            var binaryMessage = message.ToBinary() + BinaryMessageEnd;
            var bitIndex = 0;

            if (binaryMessage.Length > _image.Width * _image.Height * 3)
                throw new NotImplementedException();

            var end = false;
            for (var i = 0; i < _image.Width && !end; i++)
            {
                for (var j = 0; j < _image.Height; j++)
                {
                    var pixel = _image[i, j];

                    pixel.R.SetSpecificBit(byte.Parse(binaryMessage[bitIndex++].ToString()), 0);

                    end = bitIndex == binaryMessage.Length;
                    if (end)
                    {
                        _image[i, j] = pixel;
                        break;
                    }

                    pixel.G.SetSpecificBit(byte.Parse(binaryMessage[bitIndex++].ToString()), 0);
                    end = bitIndex == binaryMessage.Length;
                    if (end)
                    {
                        _image[i, j] = pixel;
                        break;
                    }

                    pixel.B.SetSpecificBit(byte.Parse(binaryMessage[bitIndex++].ToString()), 0);
                    end = bitIndex == binaryMessage.Length;
                    if (end)
                    {
                        _image[i, j] = pixel;
                        break;
                    }

                    _image[i, j] = pixel;
                }
            }

            return _image;
        }

        public override string Decode()
        {
            var result = "";

            var byteCounter = 0;
            var end = false;
            for (var i = 0; i < _image.Width && !end; i++)
            {
                for (var j = 0; j < _image.Height; j++)
                {
                    var pixel = _image[i, j];

                    result += pixel.R.GetSpecificBit(0).ToString();
                    byteCounter++;
                    end = CheckForMessageEnd(ref byteCounter, result);
                    if (end)
                        break;

                    result += pixel.G.GetSpecificBit(0).ToString();
                    byteCounter++;
                    end = CheckForMessageEnd(ref byteCounter, result);
                    if (end)
                        break;

                    result += pixel.B.GetSpecificBit(0).ToString();
                    byteCounter++;
                    end = CheckForMessageEnd(ref byteCounter, result);
                    if (end)
                        break;
                }
            }

            result = result.Substring(0, result.Length - BinaryMessageEnd.Length);

            return result.FromBinary();
        }

        private bool CheckForMessageEnd(ref int byteCounter, string result)
        {
            if (byteCounter == 8)
            {
                byteCounter = 0;
                var latestByte = result.Substring(result.Length - 8, 8);
                if (latestByte == BinaryMessageEnd)
                    return true;
            }

            return false;
        }
    }
}
