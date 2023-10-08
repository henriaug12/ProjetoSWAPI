using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
public class Program {
    static async Task getMovies(HttpClient client){
        var json = await client.GetStringAsync(
         "https://swapi.dev/api/films/");
         
        JsonNode? filmListNode = JsonNode.Parse(json);
        if(filmListNode is not null){
            var results = JsonSerializer.Deserialize<List<Film>>(filmListNode["results"]);
        
            foreach(var movie in results){
                Console.WriteLine($"\nEpisódio {movie.episode_id}: {movie.title}\n\n"+
                    $"{movie.desc}\n\n");
            }
        }
        else Console.WriteLine("null json");
        
    }
    static async Task Main(string[] args){
        HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        await getMovies(client);
    }
}
