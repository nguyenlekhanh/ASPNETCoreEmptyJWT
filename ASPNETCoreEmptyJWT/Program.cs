using System.Security.Claims;

Dictionary<string, List<string>> gamesMap = new()
{
    {"player1", new List<string>(){"Street Fighter II", "Minecraft"}},
    {"player2", new List<string>(){"Forza Horizon 5", "Final Fantasy XIV", "FIFA 23"}}
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();


app.MapGet("/playergames", () => gamesMap)
    .RequireAuthorization(policy =>
    {
        policy.RequireRole("admin");
    });

app.MapGet("/mygames", (ClaimsPrincipal user) =>
{
    ArgumentNullException.ThrowIfNull(user.Identity?.Name);

    var username = user.Identity.Name;

    if(!gamesMap.ContainsKey(username))
    {
        return Results.Empty;
    }

    return Results.Ok(username); ;
})
.RequireAuthorization(policy =>
{
    policy.RequireRole("player");
});

app.Run();
