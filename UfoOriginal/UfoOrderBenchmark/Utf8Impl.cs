using System.Runtime.InteropServices;
using System.Text;

namespace UfoOriginal.UfoOrderBenchmark;

public class Utf8Impl
{
    public static Dictionary<string, int> Run()
    {
        var dict = new Dictionary<string, int>();
        var buffer = new byte[1024 * 1024];
        var bufferSpan = buffer.AsSpan();

        using var stream = File.OpenRead("ufo_sightings_original.csv");

        int read = stream.Read(bufferSpan);
        bool skip = true;

        while (read > 0)
        {
            ReadOnlySpan<byte> chunk = bufferSpan.Slice(0, read);

            // To debug the content of the line: Encoding.UTF8.GetString(chunk[lineRanges.Current])
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
                var key = GetKey(line);
                if (key == null) break;
                CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out _) += 1;
            }

            var lastChunk = chunk[last];
            lastChunk.CopyTo(bufferSpan);
            read = lastChunk.Length + stream.Read(bufferSpan.Slice(lastChunk.Length));
        }

        return dict;
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
}