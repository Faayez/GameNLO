namespace GameNLO.Models
{
    public record GameModel
    {
        public BoxModel[,] Map { get; set; }

        public HashSet<UserModel> Users { get; set; }
        public DateTime EndTime { get; set; }

        public GameModel()
        {
            Map = new BoxModel[100, 100];
            Users = [];

            for (var i = 0; i < 100 * 100; i++)
            {
                Map[i / 100, i % 100] = new BoxModel();
            }
        }
    }
}
