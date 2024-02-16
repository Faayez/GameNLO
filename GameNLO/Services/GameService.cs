using GameNLO.Models;

namespace GameNLO.Services
{
    public class GameService(GameEngine engine) : IGameService
    {
        private readonly GameModel gameModel = engine.GameModel;
        private readonly object boxLock = new object();
        

        public bool IsUserEligible(Guid userId)
        {
            return !gameModel.Users.Any(p => p.Id == userId);
        }

        public decimal? OpenBox(UserModel userModel, int x, int y)
        {
            lock (boxLock) // To avoid multiple users opening same box
            {
                var node = gameModel.Map[x, y];
                if (node.IsShown)
                {
                    throw new UnauthorizedAccessException();
                }

                node.RequestedUserId = userModel.Id;
                gameModel.Users.Add(userModel);
                return node.Prize;
            }
        }
    }
}
