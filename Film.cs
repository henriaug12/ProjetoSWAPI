using System.Text.Json.Serialization;

public class Film{
    public Film(string title, int episode_id, string desc){
        this.Title = title;
        this.Episode_id = episode_id;
        this.Desc = desc;
    }

    public Film(){
        this.Title = "";
        this.Episode_id = 0;
        this.Desc = "";
    }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("episode_id")]
    public int Episode_id { get; set; }
    [JsonPropertyName("opening_crawl")]
    public string Desc { get; set; }

}