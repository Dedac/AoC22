var monkeys = new List<Monkey>();
var rounds = 10000;

var execOp = (string[] op, double val) =>
{  
    var a = op[3] == "old" ? val : double.Parse(op[3]);
    var b = op[5] == "old" ? val : double.Parse(op[5]);
    return op[4] == "+" ? a+b : a*b;
};

foreach (var lines in File.ReadLines(args[0]).SplitBy(s => s == ""))
{
    var m = lines.Select(l => l.Split(':')[1]).ToArray();
    monkeys.Add(new Monkey()
    {
        Items = m[1].Split(',').Select(s => double.Parse(s)).ToList(),
        Op = (m[2].Split(' ')),
        Test = double.Parse(m[3].Split(' ').Last()),
        MTrue = int.Parse(m[4].Split(' ').Last()),
        MFalse = int.Parse(m[5].Split(' ').Last())
    });
}

var worryDiv = monkeys.Aggregate((double)1,(x,m) => x * m.Test);

for(int i=0; i<rounds; i++)
{
    for(int j=0; j<monkeys.Count; j++)
    {
        foreach(var item in monkeys[j].Items)
        {
            monkeys[j].Inspections++;
            var worry = execOp(monkeys[j].Op,item) % worryDiv; 
            var toMonkey = worry % monkeys[j].Test == 0 ? monkeys[j].MTrue : monkeys[j].MFalse;
            monkeys[toMonkey].Items.Add(worry);
        }
        monkeys[j].Items = new List<double>();
    }
}

Console.WriteLine(string.Join(",", monkeys.Select(m=>m.Inspections)));
Console.WriteLine(monkeys.OrderByDescending(m => m.Inspections).Take(2)
                         .Aggregate(1,(double v,Monkey m) => m.Inspections * v));

class Monkey{
    public List<double> Items = new List<double>();
    public string[] Op = new string[0];
    public double Test;
    public int MTrue;
    public int MFalse;
    public int Inspections;
}