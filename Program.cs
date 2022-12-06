var signal = File.ReadAllText(args[0]);
for (int i = 0; i<signal.Length;i++)
  if (signal.Skip(i).Take(14).Distinct().Count() == 14) 
  Console.WriteLine(i+14);