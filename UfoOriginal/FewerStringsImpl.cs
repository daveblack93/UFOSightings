using System.Diagnostics;

namespace UfoOriginal;

public class FewerStringsImpl
{
    public static Dictionary<string, int> Run()
    {
        using var reader = File.OpenText("ufo_sightings_original.csv");

        var dict = new Dictionary<string, int>();

        _ = reader.ReadLine();

        while (reader.ReadLine() is { Length: > 0 } line)
        {
            var span = line.AsSpan();

            var lastChar = span[^1];

            var e = span.Split(',');
            Debug.Assert(e.MoveNext());
            Debug.Assert(e.MoveNext());
            Debug.Assert(e.MoveNext());
            var state = line[e.Current];
            Debug.Assert(e.MoveNext());
            var country = line[e.Current];

            var key = $"{state},{country}".ToUpperInvariant();
            if (dict.TryGetValue(key, out var value))
            {
                dict[key] = value + 1;
            }
            else
            {
                dict.Add(key, 1);
            }
        }

        return dict;
    }
}