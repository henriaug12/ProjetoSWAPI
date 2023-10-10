using System.Text.Json.Serialization;
public class Planet{
    public Planet(string name, string diameter, string population){
        this.Name = name;
        this.Diameter = diameter;
        this.Population = population;
    }

    public Planet(){
        this.Name = "";
        this.Diameter = "";
        this.Population = "";
    }
    [JsonPropertyName("name")]
    public string Name { get; set;}
    [JsonPropertyName("diameter")]
    public string Diameter { get; set;}
    [JsonPropertyName("population")]
    public string Population { get; set;}
}