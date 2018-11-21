using System;
using System.IO;

namespace Webmaster442.Applib.IO
{
    /// <summary>
    /// A Random byte source stream. Write operations do nothing on this stream
    /// </summary>
    public class RandomStream : Stream
    {
        private Random _generator;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public RandomStream()
        {
            _generator = new Random();
        }


        /// <summary>
        /// Returns true
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Returns true
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        /// Returns true
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        /// Returns long.MaxValue
        /// </summary>
        public override long Length
        {
            get { return long.MaxValue; }
        }

        /// <summary>
        /// Gets or sets the current position. The current postion is irrelevant.
        /// </summary>
        public override long Position
        {
            get;
            set;
        }

        /// <summary>
        /// Write all buffers. Irrelevant. Does nothing.
        /// </summary>
        public override void Flush()
        {
            return;
        }

        /// <summary>
        /// Fill the buffer with random numbers
        /// </summary>
        /// <param name="buffer">buffer to fill</param>
        /// <param name="offset">start offset</param>
        /// <param name="count">count of bytes to fill</param>
        /// <returns>The buffer filled with random numbers</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                buffer[i] = (byte)_generator.Next(0, 255);
            }
            return count;
        }

        /// <summary>
        /// Seek to position. Irrelevant.
        /// </summary>
        /// <param name="offset">Offset to position to</param>
        /// <param name="origin">Seek origin</param>
        /// <returns>Current position</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = 0;
                    Position += offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position += offset;
                    break;
            }

            return Position;
        }

        /// <summary>
        /// Irrelevant. Does nothing.
        /// </summary>
        /// <param name="value">Not used</param>
        public override void SetLength(long value)
        {
            return;
        }

        /// <summary>
        /// Irrelevant. Does nothing.
        /// </summary>
        /// <param name="buffer">Not used</param>
        /// <param name="offset">Not used</param>
        /// <param name="count">Not used</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            return;
        }
    }
}
