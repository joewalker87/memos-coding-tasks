namespace Task01Duplicates
{
    internal static class DuplicateFinder
    {
        public static void Run()
        {
            int numbersCount = 1000000;
            int min = 0;
            int max = 50;
            Random randNum = new Random();
            int[] randomNumbersArray = Enumerable.Repeat(0, numbersCount).Select(i => randNum.Next(min, max)).ToArray();

            Dictionary<int, int> dictionaryOfDuplicates = randomNumbersArray.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .ToDictionary(x => x.Key, y => y.Count());

            Console.WriteLine("Výpis deseti nejčetnějších duplikátů:");

            foreach (var number in dictionaryOfDuplicates.OrderByDescending(x => x.Value).Take(10))
            {
                Console.WriteLine("Číslo " + number.Key + " se v poli vyskytuje " + number.Value + "x.");
            }

            var relativePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\VSECHNY-DUPLIKATY.txt");
            var fullPath = Path.GetFullPath(relativePath);

            using (StreamWriter file = new StreamWriter(fullPath))
                foreach (var entry in dictionaryOfDuplicates.OrderByDescending(x => x.Value))
                    file.WriteLine("{0};{1}", entry.Key, entry.Value);

            System.Diagnostics.Process.Start("notepad.exe", fullPath);
        }
    }
}
