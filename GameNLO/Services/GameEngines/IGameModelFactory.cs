using GameNLO.Models;

namespace GameNLO.Services.GameEngines
{
    public interface IGameModelFactory
    {
        GameModel CreateGameModel(DateTime endTime);
    }
}
