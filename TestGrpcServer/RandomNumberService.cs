using Grpc.Core;
using RandomNumber;

namespace TestGrpcServer;

public class RandomNumberService : RandomNumberGenerator.RandomNumberGeneratorBase
{
    private readonly Random random = new();
    private readonly SequenceService sequenceService;
    private readonly int timeStampShift;
    private const int RandomValueBits = 16;
    private static int MaxRandomValue => (int)Math.Pow(2, RandomValueBits);
    public RandomNumberService(SequenceService sequenceService)
    {
        this.sequenceService = sequenceService;
        timeStampShift = sequenceService.Bitness + RandomValueBits;
    }

    public override Task<RandomNumberResponse> GetRandomNumber(RandomNumberRequest request, ServerCallContext context)
    {
        var response = new RandomNumberResponse()
        {
            Number = (ulong)DateTime.UtcNow.ToBinary() << timeStampShift |
                     sequenceService.GetNextSequence() << sequenceService.Bitness |
                     (uint)random.Next(0, MaxRandomValue)
        };
        return Task.FromResult(response);
    }
}