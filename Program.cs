var lines = File.ReadAllLines(args[0]).AsEnumerable();
var elves = lines.SplitBy((s) => s == "");
var elvesByCals = elves.Select(elf => elf.Select((s) => int.Parse(s)).Sum());
Console.WriteLine(elvesByCals.OrderByDescending(i => i).Take(3).Sum());