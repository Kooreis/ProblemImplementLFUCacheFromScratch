class Node {
    constructor(key, value) {
        this.key = key;
        this.value = value;
        this.freq = 0;
        this.prev = this.next = null;
    }
}