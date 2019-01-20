/*  This file is part of SevenZipSharp.

    SevenZipSharp is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SevenZipSharp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with SevenZipSharp.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace SevenZip.Sdk.Compression.RangeCoder
{
    internal struct BitTreeEncoder
    {
        private readonly BitEncoder[] Models;
        private readonly int NumBitLevels;

        public BitTreeEncoder(int numBitLevels)
        {
            NumBitLevels = numBitLevels;
            Models = new BitEncoder[1 << numBitLevels];
        }

        public void Init()
        {
            for (uint i = 1; i < 1 << NumBitLevels; i++)
                Models[i].Init();
        }

        public void Encode(Encoder rangeEncoder, uint symbol)
        {
            uint m = 1;
            for (var bitIndex = NumBitLevels; bitIndex > 0;)
            {
                bitIndex--;
                var bit = (symbol >> bitIndex) & 1;
                Models[m].Encode(rangeEncoder, bit);
                m = (m << 1) | bit;
            }
        }

        public void ReverseEncode(Encoder rangeEncoder, uint symbol)
        {
            uint m = 1;
            for (uint i = 0; i < NumBitLevels; i++)
            {
                var bit = symbol & 1;
                Models[m].Encode(rangeEncoder, bit);
                m = (m << 1) | bit;
                symbol >>= 1;
            }
        }

        public uint GetPrice(uint symbol)
        {
            uint price = 0;
            uint m = 1;
            for (var bitIndex = NumBitLevels; bitIndex > 0;)
            {
                bitIndex--;
                var bit = (symbol >> bitIndex) & 1;
                price += Models[m].GetPrice(bit);
                m = (m << 1) + bit;
            }
            return price;
        }

        public uint ReverseGetPrice(uint symbol)
        {
            uint price = 0;
            uint m = 1;
            for (var i = NumBitLevels; i > 0; i--)
            {
                var bit = symbol & 1;
                symbol >>= 1;
                price += Models[m].GetPrice(bit);
                m = (m << 1) | bit;
            }
            return price;
        }

        public static uint ReverseGetPrice(BitEncoder[] Models, uint startIndex,
            int NumBitLevels, uint symbol)
        {
            uint price = 0;
            uint m = 1;
            for (var i = NumBitLevels; i > 0; i--)
            {
                var bit = symbol & 1;
                symbol >>= 1;
                price += Models[startIndex + m].GetPrice(bit);
                m = (m << 1) | bit;
            }
            return price;
        }

        public static void ReverseEncode(BitEncoder[] Models, uint startIndex,
            Encoder rangeEncoder, int NumBitLevels, uint symbol)
        {
            uint m = 1;
            for (var i = 0; i < NumBitLevels; i++)
            {
                var bit = symbol & 1;
                Models[startIndex + m].Encode(rangeEncoder, bit);
                m = (m << 1) | bit;
                symbol >>= 1;
            }
        }
    }

    internal struct BitTreeDecoder
    {
        private readonly BitDecoder[] Models;
        private readonly int NumBitLevels;

        public BitTreeDecoder(int numBitLevels)
        {
            NumBitLevels = numBitLevels;
            Models = new BitDecoder[1 << numBitLevels];
        }

        public void Init()
        {
            for (uint i = 1; i < 1 << NumBitLevels; i++)
                Models[i].Init();
        }

        public uint Decode(Decoder rangeDecoder)
        {
            uint m = 1;
            for (var bitIndex = NumBitLevels; bitIndex > 0; bitIndex--)
                m = (m << 1) + Models[m].Decode(rangeDecoder);
            return m - ((uint) 1 << NumBitLevels);
        }

        public uint ReverseDecode(Decoder rangeDecoder)
        {
            uint m = 1;
            uint symbol = 0;
            for (var bitIndex = 0; bitIndex < NumBitLevels; bitIndex++)
            {
                var bit = Models[m].Decode(rangeDecoder);
                m <<= 1;
                m += bit;
                symbol |= bit << bitIndex;
            }
            return symbol;
        }

        public static uint ReverseDecode(BitDecoder[] Models, uint startIndex,
            Decoder rangeDecoder, int NumBitLevels)
        {
            uint m = 1;
            uint symbol = 0;
            for (var bitIndex = 0; bitIndex < NumBitLevels; bitIndex++)
            {
                var bit = Models[startIndex + m].Decode(rangeDecoder);
                m <<= 1;
                m += bit;
                symbol |= bit << bitIndex;
            }
            return symbol;
        }
    }
}