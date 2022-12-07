internal class Day7 
{
    private static void Day(string[] args)
    {
        var dirSizes = new Dictionary<string, int>() { { "//", 0 } };
        var cd = "";

        void addSize(string dir, int size)
        {
            dirSizes[dir] += size;
            if (dir.Length > 2) addSize(dir.Remove(dir.LastIndexOf('/')), size);
        };

        foreach (var line in File.ReadLines(args[0]))
        {
            if (line.StartsWith("$ cd"))
            {
                if (line.Substring(5) == "..")
                    cd = cd.Remove(cd.LastIndexOf('/'));
                else
                    cd += "/" + line.Substring(5);
            }
            else if (line.StartsWith("dir"))
                dirSizes.Add($"{cd}/{line.Substring(4)}", 0);
            else if (!line.StartsWith('$'))
                addSize(cd, int.Parse(line.Split(' ')[0]));
        }
        Console.WriteLine(dirSizes.Skip(1).Sum(dir => dir.Value <= 100000 ? dir.Value : 0));
        Console.WriteLine(dirSizes.Where(d => d.Value > dirSizes["//"] - 40000000).OrderBy(v => v.Value).First().Value);
    }
}