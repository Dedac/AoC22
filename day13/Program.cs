internal class Day13
{
    private static void Day(string[] args)
    {
        Packet Build(string s)
        {
            Packet? c = null;
            var num = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                {
                    var np = new Packet() { Parent = c };
                    c = np;
                }
                else if (s[i] == ']' || s[i] == ',')
                {
                    if (num != "")
                    {
                        c?.Packets.Add(new Packet() { Parent = c, Value = int.Parse(num), Position = c?.Packets.Count() });
                        num = "";
                    }
                    if (s[i] == ']' && c?.Parent != null)
                    {
                        c.Parent.Packets.Add(c);
                        c = c?.Parent;
                    }
                }
                else
                    num += s[i];
            }
            return c ?? new Packet();
        }

        bool Ordered(Packet? l, Packet? r)
        {
            if (l == null) return true;
            if (r == null) return false;
            if (l.Empty && r.Empty) return Ordered(l.Next(), r.Next());
            if (l.Value > -1 && r.Value > -1)
            {
                if (l.Value == r.Value)
                    return Ordered(l.Next(), r.Next());
                else
                    return l.Value < r.Value;
            }
            if (l.Packets.Count > 0 && r.Packets.Count > 0)
                return Ordered(l.Packets.First(), r.Packets.First());

            if (r.Value > -1 && l.Packets.Count > 0)
            {
                r.Packets.Add(new Packet() { Parent = r, Value = r.Value });
                return Ordered(l.Packets.First(), r.Packets.First());
            }
            if (l.Value > -1 && r.Packets.Count > 0)
            {
                l.Packets.Add(new Packet() { Parent = l, Value = l.Value });
                return Ordered(l.Packets.First(), r.Packets.First());
            }
            if (l.Packets.Count == 0 && r.Packets.Count > 0) return true;
            if (r.Packets.Count == 0 && l.Packets.Count > 0) return false;
            if (!l.Empty && r.Empty) return false;
            if (l.Empty && !r.Empty) return true;

            throw new Exception("oops");
        }

        var packets = File.ReadLines(args[0]).SplitBy(s => s == "")
            .Select(a => (Build(a.First()), Build(a.Last()))).ToList();

        var sum = 0;
        for (int i = 0; i < packets.Count; i++)
            if (Ordered(packets[i].Item1, packets[i].Item2))
                sum += i + 1;

        Console.WriteLine(sum);
    }


    class Packet
    {
        public List<Packet> Packets = new List<Packet>();
        public Packet? Parent = null;
        public int Value = -1;
        public int? Position = 0;
        public Packet? Next()
        {
            var i = Position + 1 ?? int.MaxValue;
            if (i >= Parent?.Packets.Count) return null;
            return Parent?.Packets[i];
        }
        public bool Empty => Value == -1 && Packets.Count == 0;
    }
}