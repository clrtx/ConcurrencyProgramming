using System.Diagnostics;

const int writerCount = 3;
const int readerCount = 2;

//NoSync();
LockSync();
RWSync();

MutexSync();
CasNoSync();

void NoSync()
{
    string? buffer = null;

    
    var writers = new List<Thread>();
    var readers = new List<Thread>();
    
    var unicode = (int)'A';
    
    var writersFinished = false;
    
    for (var i = 0; i < writerCount; i++)
    {
        var acc = unicode + i;
        
        writers.Add(new Thread(() => Write(acc)));
    }
    
    for (var i = 0; i < readerCount; i++)
    {
        var acc = i + 1;
        
        readers.Add(new Thread(() => Read(acc)));
    }
    
    void Write(int acc)
    {
        var messages = new List<string>();
        
        var writerMessageCount = 5;
    
        var i = 1;
        for (; i <= writerMessageCount; i++)
        {
            var message = new string((char)acc, 5);
            message += i.ToString();
            messages.Add(message);
        }
    
        i = 0;
    
        while (i < writerMessageCount)
        {
            if (buffer is null)
            {
                buffer = messages[i++];
            }
        }
    }
    
    void Read(int acc)
    {
        var messages = new List<string>();
        
        while (!writersFinished)  
        {
            if (buffer != null)
            {
                messages.Add($"{acc} {buffer}");
                buffer = null;
            }
        }
        // messages.ForEach(Console.WriteLine);
    }

    var stopwatch = Stopwatch.StartNew();
    
    foreach (var reader in readers)
    {
        reader.Start();
    }
    
    foreach (var writer in writers)
    {
        writer.Start();
    }
    
    while (writers.Any(item => item.IsAlive)){}
    
    writersFinished = true;
    
    while (readers.Any(item => item.IsAlive)){}

    Console.WriteLine(stopwatch.ElapsedMilliseconds);
}

void LockSync()
{
    string? buffer = null;
    
    var writers = new List<Thread>();
    var readers = new List<Thread>();

    var writerLock = new object();
    var readerLock = new object();
    
    var unicode = (int)'A';
    
    var writersFinished = false;
    
    for (var i = 0; i < writerCount; i++)
    {
        var acc = unicode + i;
        
        writers.Add(new Thread(() => Write(acc)));
    }
    
    for (var i = 0; i < readerCount; i++)
    {
        var acc = i + 1;
        
        readers.Add(new Thread(() => Read(acc)));
    }
    
    void Write(int acc)
    {
        var messages = new List<string>();
        
        var writerMessageCount = 5;
    
        var i = 1;
        for (; i <= writerMessageCount; i++)
        {
            var message = new string((char)acc, 5);
            message += i.ToString();
            messages.Add(message);
        }
    
        i = 0;
    
        while (i < writerMessageCount)
        {
            lock (writerLock)
            {
                if (buffer is null)
                {
                    buffer = messages[i++];
                }
            }
        }
    }
    
    void Read(int acc)
    {
        var messages = new List<string>();
        
        while (!writersFinished)  
        {
            if (buffer != null)
            {
                lock (readerLock)
                {
                    if (buffer != null)
                    {
                        messages.Add($"{acc} {buffer}");
                        buffer = null;
                    }
                }
            }
        }
        // messages.ForEach(Console.WriteLine);
    }
    

    var stopwatch = Stopwatch.StartNew();
    
    foreach (var reader in readers)
    {
        reader.Start();
    }
    
    foreach (var writer in writers)
    {
        writer.Start();
    }
    
    while (writers.Any(item => item.IsAlive)){}
    
    writersFinished = true;
    
    while (readers.Any(item => item.IsAlive)){}

    Console.WriteLine(stopwatch.ElapsedMilliseconds);
}

void RWSync()
{
    string? buffer = null;
    
    var writers = new List<Thread>();
    var readers = new List<Thread>();

    var readerWriterLockSlim = new ReaderWriterLockSlim();
    
    var unicode = (int)'A';
    
    var writersFinished = false;
    
    for (var i = 0; i < writerCount; i++)
    {
        var acc = unicode + i;
        
        writers.Add(new Thread(() => Write(acc)));
    }
    
    for (var i = 0; i < readerCount; i++)
    {
        var acc = i + 1;
        
        readers.Add(new Thread(() => Read(acc)));
    }
    
    void Write(int acc)
    {
        var messages = new List<string>();
        
        var writerMessageCount = 5;
    
        var i = 1;
        for (; i <= writerMessageCount; i++)
        {
            var message = new string((char)acc, 5);
            message += i.ToString();
            messages.Add(message);
        }
    
        i = 0;
    
        while (i < writerMessageCount)
        {
            readerWriterLockSlim.EnterWriteLock();
            if (buffer is null)
            {
                buffer = messages[i++];
            }
            readerWriterLockSlim.ExitWriteLock();
        }
    }
    
    void Read(int acc)
    {
        var messages = new List<string>();
        
        while (!writersFinished)  
        {
            if (buffer != null)
            {
                readerWriterLockSlim.EnterWriteLock();
                if (buffer != null)
                {
                    messages.Add($"{acc} {buffer}");
                    buffer = null;
                }
           
                readerWriterLockSlim.ExitWriteLock();
            }
        }
        // messages.ForEach(Console.WriteLine);
    }

    var stopwatch = Stopwatch.StartNew();
    
    foreach (var reader in readers)
    {
        reader.Start();
    }
    
    foreach (var writer in writers)
    {
        writer.Start();
    }
    
    while (writers.Any(item => item.IsAlive)){}
    
    writersFinished = true;
    
    while (readers.Any(item => item.IsAlive)){}

    Console.WriteLine(stopwatch.ElapsedMilliseconds);
}

void MutexSync()
{
    string? buffer = null;
    
    var writers = new List<Thread>();
    var readers = new List<Thread>();

    var mutex = new Mutex();
    
    var unicode = (int)'A';
    
    var writersFinished = false;
    
    for (var i = 0; i < writerCount; i++)
    {
        var acc = unicode + i;
        
        writers.Add(new Thread(() => Write(acc)));
    }
    
    for (var i = 0; i < readerCount; i++)
    {
        var acc = i + 1;
        
        readers.Add(new Thread(() => Read(acc)));
    }
    
    void Write(int acc)
    {
        var messages = new List<string>();
        
        var writerMessageCount = 5;
    
        var i = 1;
        for (; i <= writerMessageCount; i++)
        {
            var message = new string((char)acc, 5);
            message += i.ToString();
            messages.Add(message);
        }
    
        i = 0;
    
        while (i < writerMessageCount)
        {
            mutex.WaitOne();
            if (buffer is null)
            {
                buffer = messages[i++];
            }
            mutex.ReleaseMutex();
        }
    }
    
    void Read(int acc)
    {
        var messages = new List<string>();
        
        while (!writersFinished)  
        {
            if (buffer != null)
            {
                mutex.WaitOne();
                if (buffer != null)
                {
                    messages.Add($"{acc} {buffer}");
                    buffer = null;
                }
           
                mutex.ReleaseMutex();
            }
        }
        // messages.ForEach(Console.WriteLine);
    }
    

    var stopwatch = Stopwatch.StartNew();
    
    foreach (var reader in readers)
    {
        reader.Start();
    }
    
    foreach (var writer in writers)
    {
        writer.Start();
    }
    
    while (writers.Any(item => item.IsAlive)){}
    
    writersFinished = true;
    
    while (readers.Any(item => item.IsAlive)){}

    Console.WriteLine(stopwatch.ElapsedMilliseconds);
}

void CasNoSync()
{
    string? buffer = null;
    
    var writers = new List<Thread>();
    var readers = new List<Thread>();
    
    var unicode = (int)'A';
    
    var writersFinished = false;
    
    for (var i = 0; i < writerCount; i++)
    {
        var acc = unicode + i;
        
        writers.Add(new Thread(() => Write(acc)));
    }
    
    for (var i = 0; i < readerCount; i++)
    {
        var acc = i + 1;
        
        readers.Add(new Thread(() => Read(acc)));
    }
    
    void Write(int acc)
    {
        var messages = new List<string>();
        
        var writerMessageCount = 5;
    
        var i = 1;
        for (; i <= writerMessageCount; i++)
        {
            var message = new string((char)acc, 5);
            message += i.ToString();
            messages.Add(message);
        }
    
        i = 0;
    
        while (i < writerMessageCount)
        {
            if (Interlocked.CompareExchange(ref buffer, messages[i], null) == null) i++;
        }
    }
    
    void Read(int acc)
    {
        var messages = new List<string>();
        
        while (!writersFinished)
        {
            var bufferValue = Interlocked.Exchange(ref buffer, null);
            {
                if (bufferValue != null)
                {
                    messages.Add($"{acc} {bufferValue}");
                }
                
            }
        }
        // messages.ForEach(Console.WriteLine);
    }
    

    var stopwatch = Stopwatch.StartNew();
    
    foreach (var reader in readers)
    {
        reader.Start();
    }
    
    foreach (var writer in writers)
    {
        writer.Start();
    }
    
    while (writers.Any(item => item.IsAlive)){}
    
    writersFinished = true;
    
    while (readers.Any(item => item.IsAlive)){}

    Console.WriteLine(stopwatch.ElapsedMilliseconds);
}

