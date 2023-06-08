using Grpc.Core;
using RandomNumber;

namespace TestGrpcServer;

public class RandomNumberService : RandomNumberGenerator.RandomNumberGeneratorBase
{
    private readonly Random random = new();
    private uint sequence;
    private const int SequenceBits = 16;
    private const int RandomValueBits = 16;
    private const int TimeStampShift = SequenceBits + RandomValueBits;

    public override Task<RandomNumberResponse> GetRandomNumber(RandomNumberRequest request, ServerCallContext context)
    {
        Interlocked.Increment(ref sequence);
        var response = new RandomNumberResponse()
        {
            Number = (ulong)DateTime.UtcNow.ToBinary() << TimeStampShift |
                     sequence << SequenceBits |
                     (uint)random.Next(0, 65535)
        };
        return Task.FromResult(response);
    }
}