using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
public class Program {
    static async Task GetFilms(HttpClient client){
        try{
            var url = "https://swapi.dev/api/films/";
            var json = await client.GetStringAsync(url);
            
            JsonNode? filmListNode = JsonNode.Parse(json);
            if(filmListNode is not null){
                var results = JsonSerializer.Deserialize<List<Film>>(filmListNode["results"]);
            
                foreach(var movie in results){
                    Console.WriteLine($"Episode {movie.Episode_id}: {movie.Title}\n\n"+
                        $"{movie.Desc}\n");
                }
            }
            else Console.WriteLine("Json node was null");
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException();
        }
        
    }
    static async Task GetCharacters(HttpClient client){
        try{
            var url = "https://swapi.dev/api/people/";
            var json = await client.GetStringAsync(url);
            
            JsonNode? characterListNode = JsonNode.Parse(json);
            if(characterListNode is not null){
                var results = JsonSerializer.Deserialize<List<Character>>(characterListNode["results"]);
            
                foreach(var character in results){
                    Console.WriteLine($"{character.Name}: {character.Gender}"+
                        $", born in {character.Birth_year}\n");
                }
            }
            else Console.WriteLine("Json node was null");
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException();
        }
    }
    static async Task GetPlanets(HttpClient client){
        try{
            var url = "https://swapi.dev/api/planets/";
            var json = await client.GetStringAsync(url);
            
            JsonNode? planetListNode = JsonNode.Parse(json);
            if(planetListNode is not null){
                var results = JsonSerializer.Deserialize<List<Planet>>(planetListNode["results"]);
                
                foreach(var planet in results){
                    Console.WriteLine($"{planet.Name} is {planet.Diameter} km wide and has "+
                        $"{planet.Population} inhabitants");
                }
            }
            else Console.WriteLine("Json node was null");
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException();
        }
    }
    
    static async Task AllCharacters(HttpClient client){
        try{
            var url = "https://swapi.dev/api/people/";
            var json = await client.GetStringAsync(url);
            
            await CountPages(client, url);

            JsonNode? characterListNode = JsonNode.Parse(json);
            if(characterListNode is not null){
            
                var results = JsonSerializer.Deserialize<List<Character>>(characterListNode["results"]);
                var nextUrl = JsonSerializer.Deserialize<string>(characterListNode["next"]);

                while(nextUrl is not null){
                    json = await client.GetStringAsync(nextUrl);
                    characterListNode = JsonNode.Parse(json);
                    results.AddRange(JsonSerializer.Deserialize<List<Character>>(characterListNode["results"]));
                    nextUrl = JsonSerializer.Deserialize<string>(characterListNode["next"]);
                }
                
                foreach(var character in results){
                    Console.WriteLine($"{character.Name}: {character.Gender}"+
                        $", born in {character.Birth_year}");
                } 
            }
            else Console.WriteLine("Json node was null");
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException();
        }
    }

    static async Task CountPages(HttpClient client,string url){
        try{
            var json = await client.GetStringAsync(url);
            JsonNode? pageCountNode = JsonNode.Parse(json);
            if(pageCountNode is not null){
                var previousUrl = JsonSerializer.Deserialize<string>(pageCountNode["previous"]);
                while(previousUrl is not null){
                    json = await client.GetStringAsync(previousUrl);
                    pageCountNode = JsonNode.Parse(json);
                    previousUrl = JsonSerializer.Deserialize<string>(pageCountNode["previous"]);
                }
                int count = 1;
                var nextUrl = JsonSerializer.Deserialize<string>(pageCountNode["next"]);
                while(nextUrl is not null){
                    count += 1;
                    json = await client.GetStringAsync(nextUrl);
                    pageCountNode = JsonNode.Parse(json);
                    nextUrl = JsonSerializer.Deserialize<string>(pageCountNode["next"]);
                }
                Console.WriteLine($"{count} paginas");
            }
        }
        catch(NullReferenceException){
            throw new NullReferenceException();
        }
    }

    static async Task Main(string[] args){
        HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        int option = 0;
        while(option!=-1){
            Console.WriteLine("Type 1 to see information about the Star Wars films\n"+
                            "Type 2 to see information about the Star Wars characters\n"+
                            "Type 3 to see information about the Star Wars planets\n"+
                            "Type -1 to exit\n");
            var input = Console.ReadLine();
            if (int.TryParse(input, out option))
            {
                if(option != -1){
                    Console.WriteLine("Please wait...\n");
                    switch (option)
                    {
                        case 1: await GetFilms(client);
                        break;

                        case 2: await GetCharacters(client);
                        break;

                        case 3: await GetPlanets(client);
                        break;
                        
                        default: Console.WriteLine("Number not within acceptable range");
                        break;
                    }
                }
                else Console.WriteLine("Exiting...");
            }
            else Console.WriteLine("Input was not an integer number");
        }
    }
}
