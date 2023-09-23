Dictionary<string, List<string>> gamesMap = new()
{
    {"player1", new List<string>(){"Street Fighter II", "Minecraft"}},
    {"player2", new List<string>(){"Forza Horizon 5", "Final Fantasy XIV", "FIFA 23"}}
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/playergames", () => gamesMap);

app.Run();
