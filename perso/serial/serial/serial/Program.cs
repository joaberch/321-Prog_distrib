using System.Text;
using System.Text.Json;

const string OTHERFILEPATH = "C:\\Users\\px59nyu\\Documents\\joachim.json";


Actor actor1 = new Actor()
{
    FirstName = "Ethan",
    LastName = "Itago",
    BirthDate = DateTime.Now,
    Country = "Eswatini",
    IsAlive = true,
};

Actor actor2 = new Actor()
{
    FirstName = "Bill",
    LastName = "Asalways",
    BirthDate = DateTime.Now,
    Country = "Kirghizistan",
    IsAlive = true,
};

Character gerard = new Character()
{
    FirstName = "Gérard",
    LastName = "Didier",
    Description = "A faim",
    PlayedBy = actor1,
};

Character maya = new Character()
{
    FirstName = "Maya",
    LastName = "Didier",
    Description = "N'a pas faim",
    PlayedBy = actor2,
};

Episode episode = new Episode()
{
    Title = "The beginning",
    DurationMinutes = 90,
    SequenceNumber = 1,
    Director = "Tarantino",
    Synopsis = "Le début du commencement",
    Characters = new List<Character> { gerard, maya }
};

//data in code
string serialized = SerializeCharacter(gerard);
SaveData(serialized, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "character");
Character deserializedcharacter = DeSerializecharacter(serialized);

//data in file
string serializedOther = ReadFile(OTHERFILEPATH);
Character character2 = DeSerializecharacter(serializedOther);
Console.WriteLine($"Le personnage : {character2.FirstName}");

//Episode
string serializedEpisode = SerializeEpisode(episode);
SaveData(serializedEpisode, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "episode");
Episode ep = DeserializeEpisode(serializedEpisode);
Console.WriteLine(ep.Title);

//wait
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

string SerializeEpisode(Episode episode)
{
    return JsonSerializer.Serialize(episode);
}

Episode DeserializeEpisode(string serialized)
{
    return JsonSerializer.Deserialize<Episode>(serialized);
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

public class Episode
{
    public string Title { get; set; }
    public int DurationMinutes { get; set; }
    public int SequenceNumber { get; set; }
    public string Director { get; set; }
    public string Synopsis { get; set; }
    public List<Character> Characters { get; set; } = new List<Character>();
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