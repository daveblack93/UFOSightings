using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UfoOriginal.UfoOrderBenchmark;

public class DictionaryHackImpl
{
    public static Dictionary<string, int> Run()
    {
        using var reader = File.OpenText("ufo_sightings_original.csv");

        var dict = new Dictionary<string, int>();

        _ = reader.ReadLine();

        while (reader.ReadLine() is { Length: > 0 } line)
        {
            var span = line.AsSpan();

            var e = span.Split(',');
            Debug.Assert(e.MoveNext());
            Debug.Assert(e.MoveNext());
            Debug.Assert(e.MoveNext());
            var state = line[e.Current];
            Debug.Assert(e.MoveNext());
            var country = line[e.Current];

            var key = $"{state},{country}".ToUpperInvariant();

            //pointer to the original value
            //ref int count = ref CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out _);
            //count += 1;

            // i don't need the reference anymore
            CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out _) += 1;
        }

        return dict;
    }
}