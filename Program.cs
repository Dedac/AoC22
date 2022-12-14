using System.Text.Json;

JsonElement BuildJson(string s) => JsonSerializer.Deserialize<JsonElement>(s);

int Ordered(JsonElement l, JsonElement r)
{
    if (l.ValueKind == JsonValueKind.Number && r.ValueKind == JsonValueKind.Number)
    {
        if (l.GetInt32() == r.GetInt32()) return 0;
        return l.GetInt32() < r.GetInt32() ? -1 : 1;
    }
    if (l.ValueKind == JsonValueKind.Number)
        l = BuildJson($"[{l.GetInt32()}]");
    if (r.ValueKind == JsonValueKind.Number)
        r = BuildJson($"[{r.GetInt32()}]");

    var lA = l.EnumerateArray();
    var rA = r.EnumerateArray();
    for (int i = 0; i < Math.Min(l.GetArrayLength(), r.GetArrayLength()); i++)
    {
        lA.MoveNext();
        rA.MoveNext();
        var o = Ordered(lA.Current, rA.Current);
        if (o != 0) return o;
    }
    if (l.GetArrayLength() == r.GetArrayLength()) return 0;
    return l.GetArrayLength() < r.GetArrayLength() ? -1 : 1;
}

var obj = File.ReadLines(args[0]).SplitBy(s => s == "")
    .SelectMany(a => a.Select(b => BuildJson(b))).ToList();

var sumJ = 0;
for (int i = 0; i < obj.Count / 2; i++)
    if (Ordered(obj[2 * i], obj[2 * i + 1]) == -1)
        sumJ += i + 1;
Console.WriteLine(sumJ);

var distress1 = BuildJson("[[2]]");
var distress2 = BuildJson("[[6]]");

obj.Add(distress1);
obj.Add(distress2);

obj.Sort((JsonElement l, JsonElement r) => Ordered(l, r));
Console.WriteLine((obj.IndexOf(distress1) + 1) * (obj.IndexOf(distress2) + 1));