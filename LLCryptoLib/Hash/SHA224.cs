using System;

namespace LLCryptoLib.Hash
{
    /// <summary>Computes the SHA224 hash for the input data using the managed library.</summary>
    public class SHA224 : BlockHashAlgorithm
    {
        #region Table

        private static readonly uint[] K =
        {
            0x428A2F98, 0x71374491, 0xB5C0FBCF, 0xE9B5DBA5,
            0x3956C25B, 0x59F111F1, 0x923F82A4, 0xAB1C5ED5,
            0xD807AA98, 0x12835B01, 0x243185BE, 0x550C7DC3,
            0x72BE5D74, 0x80DEB1FE, 0x9BDC06A7, 0xC19BF174,
            0xE49B69C1, 0xEFBE4786, 0x0FC19DC6, 0x240CA1CC,
            0x2DE92C6F, 0x4A7484AA, 0x5CB0A9DC, 0x76F988DA,
            0x983E5152, 0xA831C66D, 0xB00327C8, 0xBF597FC7,
            0xC6E00BF3, 0xD5A79147, 0x06CA6351, 0x14292967,
            0x27B70A85, 0x2E1B2138, 0x4D2C6DFC, 0x53380D13,
            0x650A7354, 0x766A0ABB, 0x81C2C92E, 0x92722C85,
            0xA2BFE8A1, 0xA81A664B, 0xC24B8B70, 0xC76C51A3,
            0xD192E819, 0xD6990624, 0xF40E3585, 0x106AA070,
            0x19A4C116, 0x1E376C08, 0x2748774C, 0x34B0BCB5,
            0x391C0CB3, 0x4ED8AA4A, 0x5B9CCA4F, 0x682E6FF3,
            0x748F82EE, 0x78A5636F, 0x84C87814, 0x8CC70208,
            0x90BEFFFA, 0xA4506CEB, 0xBEF9A3F7, 0xC67178F2
        };

        #endregion

        private readonly uint[] accumulator;


        /// <summary>Initializes a new instance of the SHA224 class.</summary>
        public SHA224()
            : base(64)
        {
            lock (this)
            {
                this.HashSizeValue = 224;
                this.accumulator = new uint[8];
                this.Initialize();
            }
        }


        /// <summary>Initializes the algorithm.</summary>
        public override void Initialize()
        {
            lock (this)
            {
                this.accumulator[0] = 0xC1059ED8;
                this.accumulator[1] = 0x367CD507;
                this.accumulator[2] = 0x3070DD17;
                this.accumulator[3] = 0xF70E5939;
                this.accumulator[4] = 0xFFC00B31;
                this.accumulator[5] = 0x68581511;
                this.accumulator[6] = 0x64F98FA7;
                this.accumulator[7] = 0xBEFA4FA4;
                base.Initialize();
            }
        }


        /// <summary>Process a block of data.</summary>
        /// <param name="inputBuffer">The block of data to process.</param>
        /// <param name="inputOffset">Where to start in the block.</param>
        protected override void ProcessBlock(byte[] inputBuffer, int inputOffset)
        {
            lock (this)
            {
                uint[] workBuffer = new uint[64];
                uint a = this.accumulator[0];
                uint b = this.accumulator[1];
                uint c = this.accumulator[2];
                uint d = this.accumulator[3];
                uint e = this.accumulator[4];
                uint f = this.accumulator[5];
                uint g = this.accumulator[6];
                uint h = this.accumulator[7];
                uint T1, T2;
                int i;

                uint[] temp = Utilities.ByteToUInt(inputBuffer, inputOffset, this.BlockSize, EndianType.BigEndian);
                Array.Copy(temp, 0, workBuffer, 0, temp.Length);
                for (i = 16; i < 64; i++)
                {
                    workBuffer[i] = this.Sig1R(workBuffer[i - 2]) + workBuffer[i - 7] + this.Sig0R(workBuffer[i - 15]) +
                                    workBuffer[i - 16];
                }

                for (i = 0; i < 64; i++)
                {
                    T1 = h + this.Sig1(e) + this.Ch(e, f, g) + K[i] + workBuffer[i];
                    T2 = this.Sig0(a) + this.Maj(a, b, c);

                    h = g;
                    g = f;
                    f = e;
                    e = d + T1;
                    d = c;
                    c = b;
                    b = a;
                    a = T1 + T2;
                }

                this.accumulator[0] += a;
                this.accumulator[1] += b;
                this.accumulator[2] += c;
                this.accumulator[3] += d;
                this.accumulator[4] += e;
                this.accumulator[5] += f;
                this.accumulator[6] += g;
                this.accumulator[7] += h;
            }
        }


        /// <summary>Process the last block of data.</summary>
        /// <param name="inputBuffer">The block of data to process.</param>
        /// <param name="inputOffset">Where to start in the block.</param>
        /// <param name="inputCount">How many bytes need to be processed.</param>
        /// <returns>The hash code as an array of bytes</returns>
        protected override byte[] ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            lock (this)
            {
                byte[] temp;
                int paddingSize;
                ulong size;
                uint[] final = new uint[7];

                // Figure out how much padding is needed between the last byte and the size.
                paddingSize = (int) (((ulong) inputCount + (ulong) this.Count)%(ulong) this.BlockSize);
                paddingSize = this.BlockSize - 8 - paddingSize;
                if (paddingSize < 1)
                {
                    paddingSize += this.BlockSize;
                }

                // Create the final, padded block(s).
                temp = new byte[inputCount + paddingSize + 8];
                Array.Copy(inputBuffer, inputOffset, temp, 0, inputCount);
                temp[inputCount] = 0x80;
                size = ((ulong) this.Count + (ulong) inputCount)*8;
                Array.Copy(Utilities.ULongToByte(size, EndianType.BigEndian), 0, temp, inputCount + paddingSize, 8);

                // Push the final block(s) into the calculation.
                this.ProcessBlock(temp, 0);
                if (temp.Length == this.BlockSize*2)
                {
                    this.ProcessBlock(temp, this.BlockSize);
                }

                Array.Copy(this.accumulator, final, 7);
                return Utilities.UIntToByte(final, EndianType.BigEndian);
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private uint Ch(uint x, uint y, uint z)
        {
            return (x & y) ^ (~x & z);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private uint Maj(uint x, uint y, uint z)
        {
            return (x & y) ^ (x & z) ^ (y & z);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private uint Sig0(uint x)
        {
            return Utilities.RotateRight(x, 2) ^ Utilities.RotateRight(x, 13) ^ Utilities.RotateRight(x, 22);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private uint Sig1(uint x)
        {
            return Utilities.RotateRight(x, 6) ^ Utilities.RotateRight(x, 11) ^ Utilities.RotateRight(x, 25);
        }

        private uint Sig0R(uint x)
        {
            return Utilities.RotateRight(x, 7) ^ Utilities.RotateRight(x, 18) ^ this.R(3, x);
        }

        private uint Sig1R(uint x)
        {
            return Utilities.RotateRight(x, 17) ^ Utilities.RotateRight(x, 19) ^ this.R(10, x);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private uint R(int offset, uint x)
        {
            return x >> offset;
        }
    }
}