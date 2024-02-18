using Microsoft.AspNetCore.SignalR;
using GameNLO.Extensions;
using GameNLO.Services;
using GameNLO.Hubs;


namespace NLOGame.Hubs;

public class StatusHub(IGameService gameService) : Hub<IStatusHubClient>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        var result = gameService.IsUserEligible(GetUserId());

        await Clients.Caller.ValidateUser(result, result ? "" : "You have played this round. You are not allowed to play again.");
    }

    public async Task SendCommand(int latitude, int longitude)
    {
        var result = gameService.IsUserEligible(GetUserId());
        if (!result)
        {
            await Clients.Caller.ValidateUser(result, result ? "" : "You have played this round. You are not allowed to play again.");
            throw new UnauthorizedAccessException();
        }

        var prize = gameService.OpenBox(GetUserId(), latitude, longitude);
        if (prize.HasValue)
        {
            await Clients.Caller.SendMessage($"You won {prize.Value:C0}. Congratulations 🏆🎉", "success");
        }
        else
        {
            await Clients.Caller.SendMessage("Sorry We hope you win next time", "error");
        }

        await Clients.Others.UpdateStatus($"The box in ({latitude},{longitude}) position has opened", latitude, longitude);
    }

    private Guid GetUserId()
    {
        var httpContext = Context.GetHttpContext() ?? throw new Exception();
        return httpContext.GetOrCreateUserId();
    }
}
