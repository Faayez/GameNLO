using GameNLO.Models;

namespace GameNLO.Services.GameEngines
{
    public class RandomGameModelFactory : IGameModelFactory
    {
        public GameModel CreateGameModel(DateTime endTime)
        {
            var game = new GameModel { EndTime = endTime };

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

                // Use Strategy Pattern to determine prize generation strategy
                IPrizeGenerationStrategy prizeStrategy = (i == 100) ? new MaxPrizeStrategy() : new DefaultPrizeStrategy();
                node.Prize = prizeStrategy.GeneratePrize(prizeRand);
            }

            return game;
        }
    }
}
