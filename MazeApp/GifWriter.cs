using System.Drawing.Imaging;

namespace MazeApp
{
    public sealed class GifWriter : IDisposable
    {
        private readonly BinaryWriter _writer;
        private readonly int _repeat;
        private readonly byte[] _headerBuffer = new byte[11];
        private readonly byte[] _blockheadBuffer = new byte[8];

        private int _frames;

        public GifWriter(string path, int repeat = 0) 
            : this(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read), repeat)
        {
        }

        public GifWriter(Stream stream, int repeat = 0)
        {
            _writer = new BinaryWriter(stream);
            _frames = 0;
            _repeat = repeat;
        }

        /// <summary>
        /// Writes next frame
        /// </summary>
        /// <param name="image">Next frame</param>
        /// <param name="delay">Delay after this frame in milliseconds</param>
        public void WriteFrame(Image image, int delay = 100)
        {
            using (MemoryStream ms = new())
            {
                image.Save(ms, ImageFormat.Gif);
                if (_frames == 0)
                    InitHeader(ms, image.Width, image.Height);
                

                WriteGraphicControlBlock(ms, delay);
                WriteImageBlock(ms, image.Width, image.Height);
            }

            _frames++;
        }

        /// <summary>
        /// Saves GIF file
        /// </summary>
        public void Dispose()
        {
            _writer.Write((byte)0x3b); // File Trailer
            _writer.BaseStream.Dispose();
            _writer.Dispose();
        }

        private void WriteImageBlock(MemoryStream ms, int width, int height)
        {
            ms.Position = 789;
            ms.Read(_headerBuffer, 0, _headerBuffer.Length);
            _writer.Write(_headerBuffer[0]);
            _writer.Write((short)0); // X pos
            _writer.Write((short)0); // Y pos
            _writer.Write((short)width); // Width
            _writer.Write((short)height); // Height

            if (_frames != 0)
            {
                ms.Position = 10;
                _writer.Write((byte)(ms.ReadByte() & 0x3f | 0x80)); // Local color
                WriteColorTable(ms);
            }
            else
            {
                _writer.Write((byte)(_headerBuffer[9] & 0x07 | 0x07)); // Global color
            }
            _writer.Write(_headerBuffer[10]); // LZW Min code size

            ms.Position = 789 + _headerBuffer.Length;

            int dataLength = ms.ReadByte();
            while (dataLength > 0)
            {
                _writer.Write((byte)dataLength);
                for (int i = 0; i < dataLength; i++)
                    _writer.Write((byte)ms.ReadByte());
                dataLength = ms.ReadByte();
            }
            _writer.Write((byte)0); // Terminator
        }

        private void WriteGraphicControlBlock(MemoryStream ms, int delay)
        {
            ms.Position = 781; // Locating the source GCE
            ms.Read(_blockheadBuffer, 0, _blockheadBuffer.Length);
            _writer.Write(unchecked((short)0xf921)); // ID
            _writer.Write((byte)0x04); // Block size
            _writer.Write((byte)(_blockheadBuffer[3] & 0xf7 | 0x08)); // Settings disposal flag
            _writer.Write((short)(delay / 10)); // Frame delay
            _writer.Write(_blockheadBuffer[6]); // Transparent color index
            _writer.Write((byte)0); // Terminator
        }

        private void InitHeader(MemoryStream ms, int width, int height)
        {
            _writer.Write("GIF".ToCharArray()); // File type
            _writer.Write("89a".ToCharArray()); // File version
            _writer.Write((short)width);
            _writer.Write((short)height);
            ms.Position = 10L;
            _writer.Write((byte)ms.ReadByte()); // Global color table info
            _writer.Write((byte)0); // Bacground color index
            _writer.Write((byte)0); // Pixel aspect ratio
            WriteColorTable(ms);
            WriteExtensionHeader();
        }

        private void WriteExtensionHeader()
        {
            _writer.Write(unchecked((short)0xff21)); // App Extension Block ID
            _writer.Write((byte)0x0b); // App block length
            _writer.Write("NETSCAPE2.0".ToCharArray()); // App ID
            _writer.Write((byte)3); // App block length
            _writer.Write((byte)1);
            _writer.Write((short)_repeat); // Repeat count
            _writer.Write((byte)0); // Terminator
        }

        private void WriteColorTable(MemoryStream ms)
        {
            ms.Position = 13;
            for (int i = 0; i < 768; i++)
                _writer.Write((byte)ms.ReadByte());
        }
    }
}
