using GameNLO.Models;

namespace GameNLO.Services.GameEngines
{
    public class GameEngine
    {
        private const int _gameDuration = 5 * 60 * 1000;
        private readonly IGameModelFactory _gameModelFactory;
        private readonly Timer _timer;

        public GameModel? GameModel { get; private set; }

        public GameEngine(IGameModelFactory gameModelFactory)
        {
            _gameModelFactory = gameModelFactory;
            _timer = new Timer(callback: state => InitializeGame(), state: null, dueTime: 0, period: _gameDuration);
        }

        private void InitializeGame()
        {
            var endTime = DateTime.Now.AddMilliseconds(_gameDuration);
            GameModel = _gameModelFactory.CreateGameModel(endTime);
        }
    }
}
