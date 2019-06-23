using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTest.Models
{
    /// <summary>
    /// Speciální datová třída pro řešení sekvence generátoru
    /// </summary>
    public class Sequence
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        public string SequenceName { get; set; }

        public int SequenceValue { get; set; }
    }
}
