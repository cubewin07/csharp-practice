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


    How it applies to LINQ (Chained)
        - Because of extension method, the last method initiates first.
        - If it uses foreach, source is called with method MoveNext().
        - Because at first, everything is not started, the first item is gotten by calling source.MoveNext() on the upstream enumerator.
        - This happens until it reaches the list which will return the first item. 
        - Then function of the first LINQ method is executed and yield return the item if pass. It waits until new request to get more item from the list
        - The item reaches the last method if every execution passess. After that it requests more.
        - The loop keeps running until one of the chain failed and dont have any item to pass. From here, source.MoveNext() on the caller start getting false, stopping the loop.

    <!-- Explains upstream and downstream (the magic) -->
            Ex:
        students.Where(s => s.GPA > 3).Select(s => s.Name)

        source in Select is Where(students, function) => calling source.MoveNext() in Select requests new item from Where which then call its source.MoveNext(), eventually reaching students.

        students here is the source of every source and the upstream of every method.
        while Select() is the downstream of every method.

        Upstream means closer to the source
        Downstream means closer to the consumer