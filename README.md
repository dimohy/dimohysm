# FileMemory
`FileMemory` allows you to use a file as a memory by using Span<byte>. It is useful for handling large files like memory because it does not use additional heap memory.

## How to use
```csharp
using var fileMemory = FileMemory.Connect(filename);
var fileMemorySpan = fileMemory.AsReadonlyBytes();
```
After connecting the file with `FileMemory.Connect(filename)`, use `AsReadonlyBytes()` or `AsBytes()` to access the file of type `ReadonlySpan<byte>` or `Span<byte>` as memory can do. When you're done using it, you need to disconnect it with Dispose().

## Limitation
The maximum length of `Span<T>` is the maximum length of `int`, so files larger than 2Gbytes cannot be used. 

## Caution
Exception handling has not been done yet. Caution is required when using. 

## License
MIT License
