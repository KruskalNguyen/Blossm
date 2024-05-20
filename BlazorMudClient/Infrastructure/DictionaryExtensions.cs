namespace BlazorMudClient.Infrastructure
{
    public static class DictionaryExtensions
    {
        public static TValue PopValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey keyName) where TKey : notnull
        {
            var value = dictionary[keyName];
            dictionary.Remove(keyName);
            return value;
        }
    }
}
