internal class Days
{
    private static void Day1(string[] args)
    {
        // No helper function
        // var elves = File.ReadAllText(args[0]).Split("\n\n");
        // var elvesByCals = elves.Select(
        //     elf => elf.Split('\n', StringSplitOptions.RemoveEmptyEntries)
        //     .Select((s) => int.Parse(s)).Sum());
        // Console.WriteLine(elvesByCals.OrderByDescending(i => i).Take(3).Sum());

        var lines = File.ReadLines(args[0]);
        var elves = lines.SplitBy((s) => s == "");
        var elvesByCals = elves.Select(elf => elf.Select((s) => int.Parse(s)).Sum());
        Console.WriteLine(elvesByCals.OrderByDescending(i => i).Take(3).Sum());
    }
}