using GameNLO.Models;
namespace GameNLO.Services
{
    public class GameEngine
    {
        public readonly Timer Timer;

        public GameModel GameModel;

        public GameEngine()
        {
            IntialGame(null);

            Timer = new Timer(callback: IntialGame, state: this, dueTime: 0, period: 60 * 1000);
        }

        private void IntialGame(object? state)
        {
            var game = new GameModel();
            var positionRand = new Random();
            var prizeRand = new Random();

            for (var i = 1; i <= 100; i++)
            {
                var node = game.Map[positionRand.Next(0, 100), positionRand.Next(0, 100)];
                if (node.Prize.HasValue)
                {
                    i--;
                    continue;
                }

                if (i == 100)
                {
                    node.Prize = 25000; // To make sure there's only one 25000 prize
                }
                else
                {
                    node.Prize = prizeRand.Next(1, 99) * 250; // normal prizes are between 250 to 24750
                }
            }

            GameModel = game;
        }
    }
}
