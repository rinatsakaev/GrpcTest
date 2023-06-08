namespace TestGrpcServer;

public class SequenceService
{
    private uint sequence;
    public int Bitness => 16;
    private uint MaxSequenceValue => (uint)Math.Pow(2, Bitness);
    private readonly object lockObject = new();
    public uint GetNextSequence()
    {
        lock (lockObject)
        {
            sequence = (sequence + 1) % MaxSequenceValue;
            return sequence;
        }
    }
}