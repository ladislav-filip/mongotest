namespace MongoTest.DAL
{
    public interface ISequenceRepository
    {
        int GetSequenceValue(string sequenceName);
    }
}