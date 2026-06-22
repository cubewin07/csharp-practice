1. Class is a blueprint and it defines how an object will look like and behave
2. Object is an instance of a class but holds its data and many objects with different data can be from the same class and they are saved in Heap memory. Alongside with only one copy of class on the heap memory.

Misunderstanding:
1. We can have nested-class, that nested-class belongs to the parent. However, we cannot have a new class in a method.
2. Static properties, methods and nested-class belongs to the class and only have one while non-static belong to each object (instance) - every object gets its own independent copy.
    a. Many object shared the same static properties, methods or nested-class.
    b. Static code has no implicit object reference (no this), so it cannot access instance memebers directly by name (it does not now which object's member it should use). Therefore, it can only touch instance data if object reference is provided explicitly - typically as a method parameter - never automatically
    c. At compile time, it checks if instance members have an object to reference, therefore, in static method, if I access a non-static methods or properties without object to reference, error will show up (it does not check whether that non-static method access instance memeber, it is my job)

Understanding:

1. in class, when create getter and setter, it should be Public because value is stored privately and can only be access via that.
    
    Example: public string Name {get; set;}
        a. From here, at compile time, it will create a private properties named (possibly) _name.
        b. When someone call Name or (object.Name), it will use get which return _name.
        c. While if someone set Name = value, it will use set(value) which eventually _name=value

2. There is no ArrayList<> in C# though it still use the same interface and implementation class as Java
    a. In C#, List<> is the same as ArrayList<> which implements IList<T> interfaces.


