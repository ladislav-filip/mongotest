namespace MongoTest.Services
{
    public abstract class BaseService<TCollection>
    {
        protected readonly IMongoDatabase _database;
        private IMongoCollection<TCollection> _collection;

        protected BaseService(IMongoDatabase database)
        {
            this._database = database;
        }

        protected virtual string GetCollectionName()
        {
            var name = GetType().Name.Replace("Service", "").ToLower();
            return name;
        }

        protected IMongoCollection<TCollection> Collection
        {
            get
            {
                if (_collection == null)
                {
                    var colectionName = GetCollectionName();
                    _collection = _database.GetCollection<TCollection>(colectionName);
                }
                return _collection;
            }
        }
    }
}
