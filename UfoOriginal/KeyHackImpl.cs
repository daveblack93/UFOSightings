using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace UfoOriginal;

public class KeyHackImpl
{
    private static readonly ArrayPool<byte> BytePool = ArrayPool<byte>.Shared;

    public static Dictionary<string, int> Run()
    {
        var keys = new Dictionary<int, string>();
        var dict = new Dictionary<int, int>();
        //var buffer = new byte[1024 * 1024];
        var buffer = BytePool.Rent(1024 * 1024);

        try
        {
            var bufferSpan = buffer.AsSpan();

            using var stream = File.OpenRead("ufo_sightings_original.csv");

            int read = stream.Read(bufferSpan);
            bool skip = true;

            while (read > 0)
            {
                ReadOnlySpan<byte> chunk = bufferSpan.Slice(0, read);

                var lineRanges = chunk.Split((byte)'\n');
                if (skip)
                {
                    lineRanges.MoveNext();
                    skip = false;
                }

                Range last = default;
                while (lineRanges.MoveNext())
                {
                    last = lineRanges.Current;
                    var line = chunk[lineRanges.Current];
                    if (line.Length == 0)
                    {
                        break;
                    }

                    var key = GetIntKey(line);
                    if (key == 0) break;
                    CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out var exists) += 1;
                    if (!exists)
                    {
                        keys[key] = GetKey(line) ?? "";
                    }
                }

                var lastChunk = chunk[last];
                lastChunk.CopyTo(bufferSpan);
                read = lastChunk.Length + stream.Read(bufferSpan.Slice(lastChunk.Length));
            }
        }
        finally
        {
            BytePool.Return(buffer);
        }

        return dict.ToDictionary(k => keys[k.Key], v => v.Value);
    }

    private static string? GetKey(ReadOnlySpan<byte> line)
    {
        var ranges = line.Split((byte)',');
        if (!ranges.MoveNext()) return null;
        if (!ranges.MoveNext()) return null;
        if (!ranges.MoveNext()) return null;
        var state = line[ranges.Current];
        if (!ranges.MoveNext()) return null;
        var country = line[ranges.Current];

        var key = $"{Encoding.UTF8.GetString(state)},{Encoding.UTF8.GetString(country)}";
        return key;
    }

    private static int GetIntKey(ReadOnlySpan<byte> line)
    {
        var ranges = line.Split((byte)',');
        if (!ranges.MoveNext()) return 0;
        if (!ranges.MoveNext()) return 0;
        if (!ranges.MoveNext()) return 0;
        var state = line[ranges.Current];
        if (!ranges.MoveNext()) return 0;
        var country = line[ranges.Current];

        Span<byte> bytes = stackalloc byte[4];
        if (state.Length > 0)
        {
            state.CopyTo(bytes);
        }
        else
        {
            bytes[0] = (byte)'_';
            bytes[1] = (byte)'_';
        }

        if (country.Length > 0)
        {
            country.CopyTo(bytes.Slice(2));
        }
        else
        {
            bytes[2] = (byte)'_';
            bytes[3] = (byte)'_';
        }

        return MemoryMarshal.Read<int>(bytes);
    }
}