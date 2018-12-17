using System.IO;

namespace Webmaster442.Applib.IO
{
    /// <summary>
    /// A Stream that provided a constant zero when readed, and drops all input when written
    /// </summary>
    public class ZeroStream : Stream
    {
        /// <summary>
        /// Creates a new instance of ZeroStream
        /// </summary>
        public ZeroStream()
        {
            //Position = 0;
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
        /// Constantly returns long.MaxValue
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
        /// Read data
        /// </summary>
        /// <param name="buffer">buffer to fill</param>
        /// <param name="offset">start offset</param>
        /// <param name="count">count of bytes to fill</param>
        /// <returns>The buffer filled with 0</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            for (int i=offset; i<count; i++)
            {
                buffer[i] = 0;
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
