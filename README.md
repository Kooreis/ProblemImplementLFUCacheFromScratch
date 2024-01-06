# Question: How do you implement a Least Frequently Used (LFU) cache from scratch? JavaScript Summary

The provided JavaScript code implements a Least Frequently Used (LFU) cache. The LFU cache is a type of cache that removes the least frequently used items first when the cache is full. The code defines three classes: Node, DLinkedList, and LFUCache. The Node class represents an item in the cache, containing a key, a value, a frequency count, and pointers to the previous and next nodes. The DLinkedList class is a doubly-linked list that supports insertion, removal, and checking if it's empty. The LFUCache class is the main class that implements the LFU cache. It maintains a capacity limit, the current size, the least frequently used frequency, a hash map of nodes, and a hash map of frequencies. The LFU cache supports two main operations: get and put. The get operation retrieves the value of a key if it exists in the cache, and updates the frequency of the key. The put operation inserts or updates a key-value pair in the cache. If the cache is full, it removes the least frequently used key before inserting the new key. The LFU cache uses the frequency hash map to quickly find the least frequently used key, making the removal operation efficient.

---

# TypeScript Differences

The TypeScript version of the LFU Cache implementation is very similar to the JavaScript version. Both versions use the same logic and data structures (doubly linked list and two hash maps) to solve the problem. However, there are a few differences due to the nature of TypeScript:

1. Type Annotations: TypeScript requires type annotations. In the TypeScript version, we can see type annotations for class properties and method parameters. For example, in the LFUNode class, `key: number`, `value: number`, `freq: number`, `prev: LFUNode | null`, and `next: LFUNode | null` are type annotations.

2. Non-null Assertion Operator: TypeScript includes a non-null assertion operator (!) which is used to assert that its operand is non-null and non-undefined. This is used in the TypeScript version in methods like `removeNode(node: LFUNode)`, `addToHead(node: LFUNode)`, and `removeTail()` of the LFUDoublyLinkedList class, and in the `get(key: number)` and `put(key: number, value: number)` methods of the LFUCache class.

3. Class and Method Renaming: The TypeScript version has renamed some classes and methods for clarity. For example, the JavaScript version's `Node` class is renamed to `LFUNode` in TypeScript, and the `DLinkedList` class is renamed to `LFUDoublyLinkedList`. Similarly, the `insert` method is renamed to `addToHead`, and the `remove` method is renamed to `removeNode`.

4. Optional Return Type: TypeScript allows specifying the return type of a function. In the TypeScript version, the `get` method in the LFUCache class has a return type of `number`.

Overall, the TypeScript version provides better type safety and can prevent potential runtime errors that might occur in the JavaScript version.

---

# C++ Differences

The C++ and JavaScript versions of the LFU cache implementation are quite similar in terms of logic and structure. Both versions use a similar approach to solve the problem, which involves using a hash map to store the keys and their corresponding nodes, and another hash map to store the frequencies and their corresponding linked lists of nodes. 

However, there are some differences in the language features and methods used in the two versions:

1. Data Structures: In JavaScript, the Map object is used for the hash maps, while in C++, the unordered_map is used. For the linked lists, JavaScript uses a custom doubly linked list class, while C++ uses the built-in list class.

2. Class and Object Definitions: In JavaScript, classes are defined using the class keyword and constructor functions, while in C++, classes are defined using the class keyword and member functions. In JavaScript, objects are created using the new keyword, while in C++, objects are created without the new keyword.

3. Function Definitions: In JavaScript, methods are defined inside the class using the method name followed by parentheses and curly braces, while in C++, methods are defined inside the class using the return type, method name, parentheses, and curly braces.

4. Access Modifiers: In JavaScript, all members of a class are public by default, while in C++, members are private by default. The C++ version uses the private keyword to explicitly declare private members.

5. Null and Undefined: In JavaScript, null is used to represent a non-existent object, while in C++, nullptr is used.

6. Output: In JavaScript, console.log is used to print output, while in C++, cout is used.

7. Memory Management: In JavaScript, memory management is handled automatically by the garbage collector, while in C++, developers have more control over memory management, but also more responsibility.

8. Error Handling: In JavaScript, if a key is not found in the cache, the get method returns -1. In C++, the same method returns -1 as well, but it also checks if the capacity of the cache is 0 before doing anything else.

---
