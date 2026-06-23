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


    Advanced (LINQ):
        In LINQ, most methods use foreach - which use IEnumerable and yield return item => Making it only needs to perform on demand.

    <!-- Explain -->

    with yield, it waits until the current item is digested and new request comes.
    By using foreach, internally, it calls enumerator.MoveNext(), breaking yield and makes the foreach of the source continue to return new item.


    Therefore, when you are calling foreach to a list, it is 2 foreach talking to each other with the list respectively return the item when our foreach request more.

        <!-- foreach compiled to while -->
    foreach(var item : source) 
        ||
        \/
    var enumerator = source.GetEnumerator();
    while(enumerator.MoveNext()){
        var item = enumerator.Current;
        // Body of foreach
    }


    <!-- How enumerator.MoveNext() works -->

    1. it breaks the yield and waits until the source return item.
    2a - if item returns. Then it set enumerator.Current = item then return true;
    2b - if item does not return. Return false