namespace Stegosaurus.Core.Extensions
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Set bit on position n
        /// </summary>
        /// <param name="data">Original byte value</param>
        /// <param name="newValue">New bit value</param>
        /// <param name="n">Bit position, from 0 to 7</param>
        public static void SetSpecificBit(this ref byte data, byte newValue, byte n)
        {
            var mask = 1 << n;
            data = (byte)(data & ~mask | (newValue << n) & mask);
        }

        /// <summary>
        /// Get bit on position n
        /// </summary>
        /// <param name="data">Byte value</param>
        /// <param name="n">Bit position, from 0 to 7</param>
        /// <returns></returns>
        public static byte GetSpecificBit(this byte data, byte n)
        {
            return (byte)(data & (1 << n));
        }
    }
}
