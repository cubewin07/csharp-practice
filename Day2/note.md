New things learnt:
    1. Indexer syntax (Special properties) 
        ex: public V this[K key] { set; get;}

        Because we dont use argument here, C# knows it is used for properties like auto-implemeted properties public String Name {set; get;}

    2. LINQ
        Only work with List or any class that implements (:) IEnumerable<T> because almost LINQ use IEnumerable<T> source

            Ex: (Need statis because the method belongs to the source not the object)

        <!-- Where(Func) -->
            public static IEnumerator<T> Where<T> (this IEnumerator<T> source, Func<T, bool> predicate){
                foreach(var item in source){
                    if(predicate(item))
                        yield return item;
                }
            }

        At compiler time, the one who call this method becomes the source:
            students.Where(s => s.GPA > 3) 
            => Where(
                students, 
                s => s.GPA > 3
            );
        
        <!-- Chained -->
        When chained, become of the extension method, the one who call the method becomes the source, making the item need to be passed from first chain:

            Ex:
        students.Where(s => s.GPA > 3).Select(s => s.Name) 
        => Select(
            Where(students, s => s.GPA > 3), 
            s => s.Name
        )

        => Chained LINQ becomes one chained method.