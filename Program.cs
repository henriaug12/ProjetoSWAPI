using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
public class Program {
    static async Task getMovies(HttpClient client){
        try{
            var json = await client.GetStringAsync(
             "https://swapi.dev/api/films/");
            
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
        catch (Exception e)
        {
            MyExceptionHandler eHandler = new(e);
        }
        
    }
    static async Task getCharacters(HttpClient client){
        try{
            var json = await client.GetStringAsync(
             "https://swapi.dev/api/people/");
            
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
        catch (Exception e)
        {
            MyExceptionHandler eHandler = new(e);
        }
    }
    static async Task getPlanets(HttpClient client){
        try{
            var json = await client.GetStringAsync(
             "https://swapi.dev/api/planets/");
            
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
        catch (Exception e)
        {
            MyExceptionHandler eHandler = new(e);
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
                switch (option)
                {
                    case 1: Console.WriteLine("Please wait...\n");
                    await getMovies(client);
                    break;

                    case 2: Console.WriteLine("Please wait...\n");
                    await getCharacters(client);
                    break;

                    case 3: Console.WriteLine("Please wait...\n");
                    await getPlanets(client);
                    break;

                    case -1: 
                    break;
                    
                    default: Console.WriteLine("Number not within acceptable range");
                    break;
                }
            }
            else Console.WriteLine("Input was not an integer number");
        }
    }
}
