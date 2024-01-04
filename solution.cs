Here is a JavaScript implementation of a Least Frequently Used (LFU) cache:

```javascript
class Node {
    constructor(key, value) {
        this.key = key;
        this.value = value;
        this.freq = 0;
        this.prev = this.next = null;
    }
}

class DLinkedList {
    constructor() {
        this.head = new Node(null, null);
        this.tail = new Node(null, null);
        this.head.next = this.tail;
        this.tail.prev = this.head;
    }

    insert(node) {
        node.next = this.head.next;
        node.prev = this.head;
        this.head.next.prev = node;
        this.head.next = node;
    }

    remove(node) {
        let prev = node.prev;
        let next = node.next;
        prev.next = next;
        next.prev = prev;
    }

    removeLast() {
        if (this.head.next !== this.tail) {
            let last = this.tail.prev;
            this.remove(last);
            return last.key;
        }
        return -1;
    }

    isEmpty() {
        return this.head.next === this.tail;
    }
}

class LFUCache {
    constructor(capacity) {
        this.capacity = capacity;
        this.currentSize = 0;
        this.leastFreq = 0;
        this.nodeHash = new Map();
        this.freqHash = new Map();
    }

    update(node) {
        let freq = node.freq;

        this.freqHash.get(freq).remove(node);
        if (this.freqHash.get(freq).isEmpty()) {
            this.freqHash.delete(freq);
            if (freq === this.leastFreq) {
                this.leastFreq++;
            }
        }

        node.freq++;
        if (!this.freqHash.has(node.freq)) {
            this.freqHash.set(node.freq, new DLinkedList());
        }
        this.freqHash.get(node.freq).insert(node);
    }

    get(key) {
        if (!this.nodeHash.has(key)) return -1;
        let node = this.nodeHash.get(key);
        this.update(node);
        return node.value;
    }

    put(key, value) {
        if (this.capacity === 0) return;
        if (this.nodeHash.has(key)) {
            let node = this.nodeHash.get(key);
            node.value = value;
            this.update(node);
        } else {
            if (this.currentSize === this.capacity) {
                let leastFreqList = this.freqHash.get(this.leastFreq);
                let removedKey = leastFreqList.removeLast();
                this.nodeHash.delete(removedKey);
                this.currentSize--;
            }
            let newNode = new Node(key, value);
            newNode.freq = 1;
            this.nodeHash.set(key, newNode);
            if (!this.freqHash.has(1)) {
                this.freqHash.set(1, new DLinkedList());
            }
            this.freqHash.get(1).insert(newNode);
            this.leastFreq = 1;
            this.currentSize++;
        }
    }
}
```

You can use the LFUCache class like this:

```javascript
let cache = new LFUCache(2);
cache.put(1, 1);
cache.put(2, 2);
console.log(cache.get(1)); // returns 1
cache.put(3, 3); // evicts key 2
console.log(cache.get(2)); // returns -1 (not found)
console.log(cache.get(3)); // returns 3
cache.put(4, 4); // evicts key 1
console.log(cache.get(1)); // returns -1 (not found)
console.log(cache.get(3)); // returns 3
console.log(cache.get(4)); // returns 4
```