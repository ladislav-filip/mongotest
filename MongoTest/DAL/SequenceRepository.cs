using MongoDB.Driver;
using MongoTest.Models;

namespace MongoTest.DAL
{
    /// <summary>
    /// https://www.tutorialspoint.com/mongodb/mongodb_autoincrement_sequence.htm
    /// https://stackoverflow.com/questions/49500551/insert-a-document-while-auto-incrementing-a-sequence-field-in-mongodb
    /// 
    /// Implementace pro podporu sekvence generátoru
    /// </summary>
    public class SequenceRepository : ISequenceRepository
    {
        protected readonly IMongoDatabase m_database;
        protected readonly IMongoCollection<Sequence> m_collection;

        public SequenceRepository(IMongoDatabase database)
        {
            m_database = database;
            m_collection = m_database.GetCollection<Sequence>(typeof(Sequence).Name);
        }

        public int GetSequenceValue(string sequenceName)
        {
            var filter = Builders<Sequence>.Filter.Eq(s => s.SequenceName, sequenceName);
            var update = Builders<Sequence>.Update.Inc(s => s.SequenceValue, 1);

            var result = m_collection.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<Sequence, Sequence> { IsUpsert = true, ReturnDocument = ReturnDocument.After });

            return result.SequenceValue;
        }
    }
}
