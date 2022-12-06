internal class Day6
{
    private static void Day(string[] args)
    {
        var signal = File.ReadAllText(args[0]);

        var getIndex = (int size) =>
        {
            for (int i = size; i < signal.Length; i++)
                if (signal[(i - size)..i].Distinct().Count() == size)
                    return i;
            return -1;
        };
        Console.WriteLine(getIndex(4));
        Console.WriteLine(getIndex(14));
    }
}