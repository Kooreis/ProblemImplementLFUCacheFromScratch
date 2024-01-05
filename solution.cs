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