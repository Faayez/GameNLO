using GameNLO.Models;
namespace GameNLO.Services
{
    public interface IGameService
    {
        bool IsUserEligible(Guid userId);

        decimal? OpenBox(UserModel userModel, int x, int y);
    }
}
