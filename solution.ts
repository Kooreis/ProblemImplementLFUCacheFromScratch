Here is a TypeScript implementation of a Least Frequently Used (LFU) cache:

```typescript
class LFUNode {
    key: number;
    value: number;
    freq: number;
    prev: LFUNode | null;
    next: LFUNode | null;
    constructor(key: number, value: number) {
        this.key = key;
        this.value = value;
        this.freq = 1;
        this.prev = null;
        this.next = null;
    }
}

class LFUDoublyLinkedList {
    head: LFUNode;
    tail: LFUNode;
    constructor() {
        this.head = new LFUNode(0, 0);
        this.tail = new LFUNode(0, 0);
        this.head.next = this.tail;
        this.tail.prev = this.head;
    }
    removeNode(node: LFUNode) {
        node.prev!.next = node.next;
        node.next!.prev = node.prev;
    }
    addToHead(node: LFUNode) {
        node.next = this.head.next;
        this.head.next!.prev = node;
        this.head.next = node;
        node.prev = this.head;
    }
    removeTail() {
        let tailNode = this.tail.prev!;
        this.removeNode(tailNode);
        return tailNode.key;
    }
}

class LFUCache {
    capacity: number;
    size: number;
    minFreq: number;
    nodeMap: Map<number, LFUNode>;
    freqMap: Map<number, LFUDoublyLinkedList>;
    constructor(capacity: number) {
        this.capacity = capacity;
        this.size = 0;
        this.minFreq = 0;
        this.nodeMap = new Map();
        this.freqMap = new Map();
    }
    get(key: number) {
        let node = this.nodeMap.get(key);
        if (!node) return -1;
        this.freqMap.get(node.freq)!.removeNode(node);
        if (node.freq === this.minFreq && this.freqMap.get(node.freq)!.head.next === this.freqMap.get(node.freq)!.tail) {
            this.minFreq++;
        }
        node.freq++;
        if (!this.freqMap.has(node.freq)) {
            this.freqMap.set(node.freq, new LFUDoublyLinkedList());
        }
        this.freqMap.get(node.freq)!.addToHead(node);
        return node.value;
    }
    put(key: number, value: number) {
        if (this.capacity === 0) return;
        if (this.nodeMap.has(key)) {
            let node = this.nodeMap.get(key)!;
            node.value = value;
            this.freqMap.get(node.freq)!.removeNode(node);
            if (node.freq === this.minFreq && this.freqMap.get(node.freq)!.head.next === this.freqMap.get(node.freq)!.tail) {
                this.minFreq++;
            }
            node.freq++;
            if (!this.freqMap.has(node.freq)) {
                this.freqMap.set(node.freq, new LFUDoublyLinkedList());
            }
            this.freqMap.get(node.freq)!.addToHead(node);
        } else {
            if (this.size === this.capacity) {
                let tailKey = this.freqMap.get(this.minFreq)!.removeTail();
                this.nodeMap.delete(tailKey);
                this.size--;
            }
            let newNode = new LFUNode(key, value);
            this.nodeMap.set(key, newNode);
            if (!this.freqMap.has(1)) {
                this.freqMap.set(1, new LFUDoublyLinkedList());
            }
            this.freqMap.get(1)!.addToHead(newNode);
            this.minFreq = 1;
            this.size++;
        }
    }
}
```

This LFU Cache implementation uses a doubly linked list and two hash maps. The doubly linked list is used to keep track of the nodes with the same frequency. The two hash maps are used to keep track of the nodes and their frequencies. The `get` and `put` methods are used to retrieve and store values in the cache, respectively.