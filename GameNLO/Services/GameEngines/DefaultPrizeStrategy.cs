namespace GameNLO.Services.GameEngines
{
    public class DefaultPrizeStrategy : IPrizeGenerationStrategy
    {
        public int GeneratePrize(Random prizeRand)
        {
            return prizeRand.Next(1, 99) * 250; // normal prizes are between 250 to 24750
        }
    }
}
