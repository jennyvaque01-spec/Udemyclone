namespace UdemyClone.Shared
{
    public class Cache<T>
    {
        private readonly Dictionary<string, T> _data = [];

        public void Add(string key, T value) => _data[key] = value;

        public T? Get(string key)
        {

            return _data.TryGetValue(key, out var value) ? value : default;
        }

        public List<T> Get() => [.. _data.Values];

        public bool Delete(string key) => _data.Remove(key);
    }
}