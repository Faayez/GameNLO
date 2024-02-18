namespace GameNLO.Services.GameEngines
{
    public class MaxPrizeStrategy: IPrizeGenerationStrategy
    {
        public int GeneratePrize(Random prizeRand)
        {
            return 25000; // Max prize
        }
    }
}
