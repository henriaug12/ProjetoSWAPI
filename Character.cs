using System.Text.Json.Serialization;
public class Character{
    public Character(string name, string gender, string birth_year){
        this.Name = name;
        this.Gender = gender;
        this.Birth_year = birth_year;
    }
    public Character(){
        this.Name = "";
        this.Gender = "";
        this.Birth_year = "";
    }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set;}
    [JsonPropertyName("birth_year")]
    public string Birth_year { get; set;}

}