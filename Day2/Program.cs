using System.Collections;

class Collections_LINQ
{
    static void Main(String[] args)
    {
        // Object initializers
        var students = new Dictionary<string, double>()
        {
            ["Alex"] = 3
        };

        students["Alice"] = 3.9;
        students["Johb"] = 2.8;

        // Safe lookup - avoids KeyNotFoundException
        if(students.TryGetValue("Alice", out double gpa))
        {
            Console.WriteLine(gpa);
        }

        // Iterate
        foreach(var kvp in students)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // Check existence
        bool exists = students.ContainsKey("Alice");
    }
}


class MapKeyPair<K, V>
{
    public K Key{get; set;}
    public V Value {get; set;}
}

class CustomMap<K, V> : IEnumerable<MapKeyPair<K,V>>
{
    private List<MapKeyPair<K, V>> _map = []; 

    public V this[K key]
    {
        set
        {
            var pair = _map.FirstOrDefault(p => p.Key.Equals(key));
            if(pair != null) pair.Value = value;
            else _map.Add(new() {Key = key, Value = value});
            
        }
        get
        {
            var pair = _map.FirstOrDefault(p => p.Key.Equals(key)) ?? throw new KeyNotFoundException("No pair found");
            return pair.Value;
        }
    }

    public bool ContainsKey(K key)
    {
        return _map.Any(p => p.Key.Equals(key));
    }

    public bool ContainsValue(V value)
    {
        return _map.Any(p => p.Value.Equals(value));
    }

    public bool Remove(K key)
    {
        return _map.RemoveAll(p => p.Key.Equals(key)) > 0 ;
    }



    public IEnumerable<MapKeyPair<K,V>> GetEnumerator()
    {
        foreach(var pair in _map)
        {
            yield return pair;
        }
    }

    IEnumerator<MapKeyPair<K, V>> IEnumerable<MapKeyPair<K, V>>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}