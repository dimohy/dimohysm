# FileMemory
`FileMemory` allows you to use a file as a memory by using Span<byte>. It is useful for handling large files like memory because it does not use additional heap memory.

## How to use
```csharp
            using var mmfSpan = FileMemory.Connect(filename);
            var fileMemorySpan = mmfSpan.AsReadOnlyBytes();
```
After connecting the file with `FileMemory.Connect(filename)`, use `AsReadonlyBytes()` or `AsBytes()` to access the file in the form of `ReadonlySpan<byte>` or `Span<byte>` as memory. can.
When you are done using it, you need to disconnect it with `Dispose()`. 

## Limitation
The maximum length of `Span<T>` is the maximum length of `int`, so files larger than 2Gbytes cannot be used. 

## Caution
Exception handling has not been done yet. Caution is required when using. 
