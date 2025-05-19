namespace UfoOriginal;

public class BasicImpl
{
    public static Dictionary<string, int> Run()
    {
        using var reader = File.OpenText("ufo_sightings_original.csv");

        var dict = new Dictionary<string, int>();

        _ = reader.ReadLine();

        while (reader.ReadLine() is {Length: > 0} line)
        {
            var fields = line.Split(',');
            if (fields.Length > 3)
            {
                var key = $"{fields[2]},{fields[3]}".ToUpperInvariant();
                if (dict.TryGetValue(key, out var value))
                {
                    dict[key] = value + 1;
                }
                else
                {
                    dict.Add(key, 1);
                }
            }
        }

        return dict;
    }
}