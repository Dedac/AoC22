var lines = File.ReadLines(args[0]);
var elves = lines.SplitBy((s) => s == "");
var elvesByCals = elves.Select(elf => elf.Select((s) => int.Parse(s)).Sum());
Console.WriteLine(elvesByCals.OrderByDescending(i => i).Take(3).Sum());