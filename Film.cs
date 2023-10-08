using System.Text.Json.Serialization;

public class Film{
    public Film(string title, int episode_id, string desc){
        this.title = title;
        this.episode_id = episode_id;
        this.desc = desc;
    }

    public Film(){
        this.title = "";
        this.episode_id = 0;
        this.desc = "";
    }
    public string title { get; set; }
    public int episode_id { get; set; }
    [JsonPropertyName("opening_crawl")]
    public string desc { get; set; }

}