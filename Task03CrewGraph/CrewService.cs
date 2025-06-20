using Task03CrewGraph.Models;

namespace Task03CrewGraph
{
    public static class CrewService
    {
        public static void Run()
        {
            var crewTree = CreateHierarchy(); // vytvoreni grafu

            string captainName = "Jean Luc Pickard"; // kapitan
            string memberNameSub = "William Riker"; // zadany clovek pro ktereho se najdou vsichni podrizeni
            string memberNameVirus = "Wesley Crusher"; // osoba X nakazena virem

            PrintSubordinatesOfIndividual(crewTree, memberNameSub); // vypis vsech podrizenych zadaneho cloveka
            PrintVirusedMembers(crewTree, memberNameVirus, captainName); // vypis clenu, kteri se mohou nakazit, nez se nakazi kapitan lodi
        }

        #region Metoda pro vytvoreni grafu
        public static CrewMember CreateHierarchy()
        {
            var jean = new CrewMember { Name = "Jean Luc Pickard", Parent = null };

            var william = new CrewMember { Name = "William Riker", Parent = jean };
            var deanna = new CrewMember { Name = "Deana Troi", Parent = jean };
            var jordi = new CrewMember { Name = "Jordi La Forge", Parent = jean };
            jean.Subordinates.AddRange([william, deanna, jordi]);

            var mrData = new CrewMember { Name = "Mr. Data", Parent = jordi };
            var miles = new CrewMember { Name = "Miles O'Brien", Parent = jordi };
            jordi.Subordinates.AddRange([mrData, miles]);

            var lwaxana = new CrewMember { Name = "Lwaxana Troi", Parent = deanna };
            var reginald = new CrewMember { Name = "Reginald Barkley", Parent = deanna };
            deanna.Subordinates.AddRange([lwaxana, reginald]);

            var worf = new CrewMember { Name = "Worf son of Mog", Parent = william };
            var guinan = new CrewMember { Name = "Guinan", Parent = william };
            var beverly = new CrewMember { Name = "Beverly Crusher", Parent = william };
            william.Subordinates.AddRange([worf, guinan, beverly]);

            var tasha = new CrewMember { Name = "Tasha Yar", Parent = worf };
            var kehleyr = new CrewMember { Name = "K'Ehleyr", Parent = worf };
            worf.Subordinates.AddRange([tasha, kehleyr]);

            var wesley = new CrewMember { Name = "Wesley Crusher", Parent = beverly };
            var alyssa = new CrewMember { Name = "Alyssa Ogawa", Parent = beverly };
            beverly.Subordinates.AddRange([wesley, alyssa]);

            var alexander = new CrewMember { Name = "Alexander Rozhenko", Parent = kehleyr };
            kehleyr.Subordinates.Add(alexander);

            var julian = new CrewMember { Name = "Julian Bashir", Parent = alyssa };
            alyssa.Subordinates.Add(julian);

            return jean;
        }

        #endregion

        #region Metody pro prvni ukol - vypisy podrizenych

        public static void PrintSubordinatesOfIndividual(CrewMember crewTree, string name)
        {
            Console.WriteLine(); Console.WriteLine("=== Úkol 1 - Vypis podrizenych ===");

            CrewMember foundMember = FindSubordinate(crewTree, name);

            if (foundMember == null)
            {
                Console.WriteLine(name + " nenalezen.");
                return;
            }
            else
            {
                Console.WriteLine("Výpis všech podřízených pro clena " + name + ":");
            }

            PrintAllSubordinates(foundMember);
        }

        public static CrewMember FindSubordinate(CrewMember crewMember, string name)
        {
            if (crewMember.Name == name)
            {
                return crewMember;
            }

            foreach (var item in crewMember.Subordinates)
            {
                var found = FindSubordinate(item, name);
                if (found != null)
                    return found;
            }

            return null;
        }

        public static void PrintAllSubordinates(CrewMember crewMember, int gapCount = 0)
        {
            string gapForPrint = "";

            for (int i = 0; i < gapCount; i++)
                gapForPrint += "  ";

            Console.WriteLine(gapForPrint + crewMember.Name);

            foreach (var item in crewMember.Subordinates)
                PrintAllSubordinates(item, gapCount + 1);
        }

        #endregion

        #region Metody pro druhy ukol - Virus

        private static void PrintVirusedMembers(CrewMember crewTree, string infectedName, string captainName)
        {
            Console.WriteLine(); Console.WriteLine("=== Úkol 2 - Sireni viru ===");

            CrewMember foundMember = FindSubordinate(crewTree, infectedName);

            List<CrewMember> pathToCaptain = new List<CrewMember>();

            FindAllParentsToCaptain(foundMember, pathToCaptain, captainName);

            if (pathToCaptain.Any())
            {
                Console.WriteLine("Osoba " + infectedName + " byla nakazena virem. Clenove posadky, kteri se mohou nakazit, nez bude nakazen kapitan jsou:");

                foreach (var item in pathToCaptain)
                    Console.WriteLine(item.Name);
            }
            else
            {
                Console.WriteLine("V ceste mezi " + infectedName + " a kapitanem nejsou zadni clenove.");
            }
        }

        private static void FindAllParentsToCaptain(CrewMember crewMember, List<CrewMember> path, string captainName)
        {
            if (crewMember.Parent != null && crewMember.Parent.Name != captainName)
            {
                path.Add(crewMember.Parent);
                FindAllParentsToCaptain(crewMember.Parent, path, captainName);
            }
        }

        #endregion
    }

}
