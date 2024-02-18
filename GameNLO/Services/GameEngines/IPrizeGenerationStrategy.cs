namespace GameNLO.Services.GameEngines
{
    public interface IPrizeGenerationStrategy
    {
        int GeneratePrize(Random prizeRand);
    }
}
