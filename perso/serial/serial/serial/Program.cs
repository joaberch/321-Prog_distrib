using System.Text;
using System.Text.Json;

const string PATH = "C:\\Temp";
const string OTHERFILEPATH = "C:\\Users\\px59nyu\\Documents\\joachim.json";


Actor actor1 = new Actor()
{
    FirstName = "Ethan",
    LastName = "Itago",
    BirthDate = DateTime.Now,
    Country = "Eswatini",
    IsAlive = true,
};

Character character1 = new Character()
{
    FirstName = "Alain",
    LastName = "Didier",
    Description = "A faim",
    PlayedBy = actor1,
};

//data in code
string serialized = SerializeCharacter(character1);
SaveData(serialized, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "character");
Character deserializedcharacter = DeSerializecharacter(serialized);

string serializedOther = ReadFile(OTHERFILEPATH);
Character character2 = DeSerializecharacter(serializedOther);
Console.WriteLine($"Le personnage : {character2.FirstName} est joué par {character1.PlayedBy.FirstName}");

Console.ReadLine();

void SaveData(string serialized, string path, string fileName)
{
    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, $"{fileName}.json")))
    {
        outputFile.WriteLine(serialized);
    }
}

string SerializeCharacter(Character character)
{
    return JsonSerializer.Serialize(character);
}

Character DeSerializecharacter(string serialized)
{
    return JsonSerializer.Deserialize<Character>(serialized);
}

string ReadFile(string path)
{
    using StreamReader reader = new(path);
    return reader.ReadToEnd();
}

public class Character
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public Actor PlayedBy { get; set; }
}

public class Actor
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Country { get; set; }
    public bool IsAlive { get; set; }
}