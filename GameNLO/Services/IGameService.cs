using GameNLO.Models;
namespace GameNLO.Services
{
    public interface IGameService
    {
        bool IsUserEligible(Guid userId);

        decimal? OpenBox(Guid userId, int x, int y);
    }

}
