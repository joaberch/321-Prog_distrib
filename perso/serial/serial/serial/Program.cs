using System.Text;
using System.Text.Json;

const string OTHERFILEPATH = "C:\\Users\\px59nyu\\Documents\\joachim.json";

// Définition des acteurs
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

// Définition des personnages
Character gerard = new Character()
{
    FirstName = "Gérard",
    LastName = "Didier",
    Description = "Un homme affamé en quête de nourriture",
    PlayedBy = actor1,
};

Character maya = new Character()
{
    FirstName = "Maya",
    LastName = "Didier",
    Description = "Une femme calme et réfléchie, souvent en opposition avec Gérard",
    PlayedBy = actor2,
};

// Liste des épisodes de la saison 1
List<Episode> season1 = new List<Episode>
{
    new Episode()
    {
        Title = "The Beginning",
        DurationMinutes = 90,
        SequenceNumber = 1,
        Director = "Tarantino",
        Synopsis = "Gérard et Maya se rencontrent dans une petite ville, où leurs chemins se croisent de manière inattendue.",
        Characters = new List<Character> { gerard, maya }
    },
    new Episode()
    {
        Title = "First Conflict",
        DurationMinutes = 85,
        SequenceNumber = 2,
        Director = "Tarantino",
        Synopsis = "Un conflit éclate entre Gérard et Maya au sujet de la nourriture. Gérard commence à douter des intentions de Maya.",
        Characters = new List<Character> { gerard, maya }
    },
    new Episode()
    {
        Title = "Unexpected Allies",
        DurationMinutes = 75,
        SequenceNumber = 3,
        Director = "Nolan",
        Synopsis = "Après une série de mésaventures, Gérard et Maya sont forcés de faire équipe pour survivre.",
        Characters = new List<Character> { gerard, maya }
    },
    new Episode()
    {
        Title = "Dark Secrets",
        DurationMinutes = 95,
        SequenceNumber = 4,
        Director = "Fincher",
        Synopsis = "Les secrets du passé de Gérard sont révélés, mettant à mal la confiance entre les deux personnages.",
        Characters = new List<Character> { gerard, maya }
    },
    new Episode()
    {
        Title = "The Betrayal",
        DurationMinutes = 80,
        SequenceNumber = 5,
        Director = "Tarantino",
        Synopsis = "Maya trahit Gérard, le laissant seul dans une situation critique.",
        Characters = new List<Character> { gerard, maya }
    },
    new Episode()
    {
        Title = "The Chase",
        DurationMinutes = 100,
        SequenceNumber = 6,
        Director = "Spielberg",
        Synopsis = "Gérard, déterminé à retrouver Maya, se lance dans une course effrénée pour la rattraper avant qu'il ne soit trop tard.",
        Characters = new List<Character> { gerard, maya }
    },
    new Episode()
    {
        Title = "Reconciliation",
        DurationMinutes = 90,
        SequenceNumber = 7,
        Director = "Tarantino",
        Synopsis = "Gérard et Maya se réconcilient après un affrontement dramatique. Ils se rendent compte qu'ils ont besoin l'un de l'autre.",
        Characters = new List<Character> { gerard, maya }
    },
    new Episode()
    {
        Title = "The End of the Journey",
        DurationMinutes = 110,
        SequenceNumber = 8,
        Director = "Nolan",
        Synopsis = "Dans un final épique, Gérard et Maya découvrent ce qui les a réellement unis tout au long de cette aventure.",
        Characters = new List<Character> { gerard, maya }
    }
};


//data in code
string serialized = Serialize(gerard);
SaveData(serialized, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "character");
Character deserializedcharacter = Deserialize<Character>(serialized);

//data in file
string serializedOther = ReadFile(OTHERFILEPATH);
Character character2 = Deserialize<Character>(serializedOther);
Console.WriteLine($"Le personnage : {character2.FirstName}");

//Episode
string serializedEpisode = Serialize(season1.FirstOrDefault());
SaveData(serializedEpisode, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "episode");
Episode ep = Deserialize<Episode>(serializedEpisode);
Console.WriteLine(ep.Title);

//Season
string serializedSeason = Serialize(season1);
SaveData(serializedSeason, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "season");
List<Episode> eps = Deserialize<List<Episode>>(serializedSeason);

eps.ForEach(ep =>
{
    Console.WriteLine($"{ep.Title}. Durée : {ep.DurationMinutes}. Numéro d'épisode : {ep.SequenceNumber}. Director : {ep.Director}. Synopsis : {ep.Synopsis}\n\n");
});

//wait
Console.ReadLine();

void SaveData(string serialized, string path, string fileName)
{
    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, $"{fileName}.json")))
    {
        outputFile.WriteLine(serialized);
    }
}

string Serialize(dynamic obj)
{
    return JsonSerializer.Serialize(obj);
}

static T Deserialize<T>(string serialized)
{
    return JsonSerializer.Deserialize<T>(serialized);
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