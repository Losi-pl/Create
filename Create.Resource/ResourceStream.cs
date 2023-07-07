namespace Create.Resource;

/// <summary>
/// <see cref="Stream"/> do pliku w repozytorium <see cref="SingleFileResources"/>
/// </summary>
public sealed class ResourceStream : Stream
{
    SingleFileResources base_;
    ResourceFile file;

    long pozition, length, offset;
    
    internal ResourceStream(SingleFileResources resources, ResourceFile file, long lenght, long offset)
    {
        base_ = resources;
        this.file = file;
        length = lenght;
        this.offset = offset;
    }

    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => length;
    public override long Position 
    { 
        get => pozition; 
        set
        {
            if (value >= length || value < 0)
                throw new IndexOutOfRangeException("Index outside file size");
            pozition = value;
        }
    }
    public override void Flush() => throw new NotSupportedException();

    public override int Read(byte[] buffer, int offset, int count)
    {
        lock(base_.TaskLock)
        {
            base_.Stream.Position = this.offset + pozition;
            if (pozition + count > length)
                count = (int)(count - ((pozition + count) - length));
            int wyn = base_.Stream.Read(buffer, offset, count);
            if (count > length - pozition)
                wyn = 0;
            pozition += count;
            return wyn;
        }
    }
    public override int Read(Span<byte> buffer)
    {
        int count = buffer.Length;
        lock (base_.TaskLock)
        {
            base_.Stream.Position = this.offset + pozition;
            if (pozition + count > length)
                count = (int)(count - ((pozition + count) - length));
            int wyn = base_.Stream.Read(buffer);
            if (count > length - pozition)
                wyn = 0;
            pozition += count;
            return wyn;
        }
    }
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => 
        Task.Run(() => Read(buffer, offset, count), cancellationToken);
    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        return new ValueTask<int>(Task.Run(() =>
        {
            int count = buffer.Length;
            lock (base_.TaskLock)
            {
                base_.Stream.Position = this.offset + pozition;
                if (pozition + count > length)
                    count = (int)(count - ((pozition + count) - length));
                var ver_t = base_.Stream.ReadAsync(buffer, cancellationToken);
                ver_t.AsTask().Wait();
                int wyn = ver_t.Result;
                if (count > length - pozition)
                    wyn = 0;
                pozition += count;
                return wyn;
            }
        }));
    }
    public override int ReadByte()
    {
        Span<byte> bytes = stackalloc byte[1];
        var l = Read(bytes);
        if (l == 0)
            return -1;
        return bytes[0];
    }
    public override void CopyTo(Stream destination, int bufferSize)
    {
        Position = 0;
        Span<byte> buffer = stackalloc byte[bufferSize];
        for(int i = 0; i < length / bufferSize; i++)
        {
            Read(buffer);
            destination.Write(buffer);
        }
        if(length % bufferSize > 0)
        {
            buffer = buffer[0..(int)(length % bufferSize)];
            Read(buffer);
            destination.Write(buffer);
        }
    }
    public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
        return Task.Run(() => CopyTo(destination, bufferSize), cancellationToken);
    }

    public override string ToString() => file.ToString();
    public override bool CanTimeout => base_.Stream.CanTimeout;

    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state) => throw new NotSupportedException();
    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state) => throw new NotSupportedException();
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
}
