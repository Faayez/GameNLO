
using GameNLO.Services;
using GameNLO.Services.GameEngines;
using NLOGame.Hubs;
namespace NLOGame;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        builder.Services.AddSignalR();

        builder.Services.AddSingleton<GameEngine>();
        builder.Services.AddSingleton<IGameModelFactory, RandomGameModelFactory>();
        builder.Services.AddSingleton<IPrizeGenerationStrategy, MaxPrizeStrategy>();
        builder.Services.AddSingleton<IPrizeGenerationStrategy, DefaultPrizeStrategy>();
        builder.Services.AddTransient<IGameService, GameService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapHub<StatusHub>("/StatusHub");

        app.Run();
    }
}