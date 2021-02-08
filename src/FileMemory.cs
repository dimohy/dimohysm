using System;
using System.IO;
using System.IO.MemoryMappedFiles;


namespace Dimohysm
{
    public sealed class FileMemory : IDisposable
    {
        private MemoryMappedFile mmf;
        private MemoryMappedViewAccessor accessor;
        private readonly unsafe byte* memoryMappedAddress;
        private readonly long fileLength;

        private bool disposedValue;


        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    accessor.SafeMemoryMappedViewHandle.ReleasePointer();
                    accessor.Dispose();
                    mmf.Dispose();
                }

                accessor = null;
                mmf = null;
                
                disposedValue = true;
            }
        }

        ~FileMemory()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public unsafe Span<byte> AsBytes()
        {
            return new Span<byte>(memoryMappedAddress, (int)fileLength);
        }

        public unsafe ReadOnlySpan<byte> AsReadOnlyBytes()
        {
            return new ReadOnlySpan<byte>(memoryMappedAddress, (int)fileLength);
        }

        private FileMemory(string filepath)
        {
            fileLength = new FileInfo(filepath).Length;
            mmf = MemoryMappedFile.CreateFromFile(filepath);
            accessor = mmf.CreateViewAccessor();
            
            unsafe
            {
                accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref memoryMappedAddress);
                memoryMappedAddress += accessor.PointerOffset;
            }
        }

        public static FileMemory Connect(string filepath)
        {
            return new FileMemory(filepath);
        }
    }
}
