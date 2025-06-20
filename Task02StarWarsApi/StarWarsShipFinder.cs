using System.Text.Json;
using Task02StarWarsApi.Models;

namespace Task02StarWarsApi
{
    internal class StarWarsShipFinder
    {
        private const string TargetPlanetName = "Kashyyyk"; // zadná lod k hledani - Kashyyyk (mozno zmenit)

        public static async Task Run()
        {
            var client = CreateHttpClientIgnoringCert();
            var allPlanets = await FetchAllPlanetsAsync(client);
            var kashyyykPeopleIds = ExtractKashyyykPeopleIds(allPlanets);
            var kashyyykPeople = await FetchPeopleByIdsAsync(client, kashyyykPeopleIds);
            var kashyyykStarshipIds = ExtractStarshipIdsFromPeople(kashyyykPeople);
            var starshipNames = await GetStarshipNamesByIdsAsync(client, kashyyykStarshipIds);
            PrintStarshipNames(starshipNames);
        }

        private static HttpClient CreateHttpClientIgnoringCert()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (req, cert, chain, errors) => true;
            return new HttpClient(handler);
        }

        private static async Task<List<Planet>> FetchAllPlanetsAsync(HttpClient client)
        {
            string nextUrl = "https://swapi.dev/api/planets/";
            List<Planet> planets = new List<Planet>();

            while (nextUrl != null)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(nextUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var json = JsonSerializer.Deserialize<PlanetResponse>(responseBody);
                    planets.AddRange(json.results);

                    nextUrl = json.next;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při načítání {nextUrl}: {ex.Message}");
                    continue;
                }
            }

            return planets;
        }

        private static List<int> ExtractKashyyykPeopleIds(List<Planet> planets)
        {
            List<string> residentUrls = new List<string>();

            foreach (Planet planet in planets)
            {
                if (planet.name == TargetPlanetName)
                {
                    residentUrls.AddRange(planet.residents);
                }
            }

            List<int> peopleIds = residentUrls.Select(url => int.Parse(url.TrimEnd('/').Split('/').Last())).ToList();

            return peopleIds;
        }

        private static async Task<List<People>> FetchPeopleByIdsAsync(HttpClient client, List<int> kashyyykPeopleIds)
        {
            List<People> peoples = new List<People>();

            foreach (var id in kashyyykPeopleIds)
            {
                string nextUrl = $"https://swapi.dev/api/people/{id}/";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(nextUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var json = JsonSerializer.Deserialize<People>(responseBody);
                    peoples.Add(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při načítání {nextUrl}: {ex.Message}");
                    continue;
                }
            }

            return peoples;
        }

        private static List<int> ExtractStarshipIdsFromPeople(List<People> kashyyykPeople)
        {
            List<string> kashyyykResidents = new List<string>();

            foreach (People planet in kashyyykPeople)
                kashyyykResidents.AddRange(planet.starships);

            List<int> ids = kashyyykResidents.Select(url => int.Parse(url.TrimEnd('/').Split('/').Last())).ToList();

            return ids;
        }

        private static async Task<List<string>> GetStarshipNamesByIdsAsync(HttpClient client, List<int> kashyyykResidentStarshipIds)
        {
            List<string> starshipNames = new List<string>();

            foreach (var id in kashyyykResidentStarshipIds)
            {
                string nextUrl = $"https://swapi.dev/api/starships/{id}/";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(nextUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var json = JsonSerializer.Deserialize<Starship>(responseBody);
                    starshipNames.Add(json.name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při načítání {nextUrl}: {ex.Message}");
                    continue;
                }
            }

            return starshipNames.Distinct().ToList();
        }

        private static void PrintStarshipNames(List<string> starshipNames)
        {
            Console.WriteLine("Jména všech lodí jejichž piloti jsou z planety " + TargetPlanetName + ":");
            starshipNames.ForEach(name => Console.WriteLine(name));
        }
    }
}
