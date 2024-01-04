```cpp
#include <unordered_map>
#include <set>
#include <iostream>

using namespace std;

class LFUCache {
private:
    struct Node {
        int key, value, freq;
        Node(int k, int v, int f): key(k), value(v), freq(f) {}
    };
    int capacity, minFreq;
    unordered_map<int, list<Node>::iterator> keyMap;
    unordered_map<int, list<Node>> freqMap;

public:
    LFUCache(int capacity): capacity(capacity), minFreq(0) {}

    int get(int key) {
        if (capacity == 0) return -1;
        auto it = keyMap.find(key);
        if (it == keyMap.end()) return -1;
        list<Node>::iterator node = it->second;
        int val = node->value, freq = node->freq;
        freqMap[freq].erase(node);
        if (freqMap[freq].size() == 0) {
            freqMap.erase(freq);
            if (minFreq == freq) minFreq++;
        }
        freqMap[freq+1].push_front(Node(key, val, freq+1));
        keyMap[key] = freqMap[freq+1].begin();
        return val;
    }

    void put(int key, int value) {
        if (capacity == 0) return;
        auto it = keyMap.find(key);
        if (it == keyMap.end()) {
            if (keyMap.size() == capacity) {
                int delKey = freqMap[minFreq].back().key;
                freqMap[minFreq].pop_back();
                if (freqMap[minFreq].size() == 0) freqMap.erase(minFreq);
                keyMap.erase(delKey);
            }
            freqMap[1].push_front(Node(key, value, 1));
            keyMap[key] = freqMap[1].begin();
            minFreq = 1;
        } else {
            list<Node>::iterator node = it->second;
            int freq = node->freq;
            freqMap[freq].erase(node);
            if (freqMap[freq].size() == 0) {
                freqMap.erase(freq);
                if (minFreq == freq) minFreq++;
            }
            freqMap[freq+1].push_front(Node(key, value, freq+1));
            keyMap[key] = freqMap[freq+1].begin();
        }
    }
};

int main() {
    LFUCache cache(2);
    cache.put(1, 1);
    cache.put(2, 2);
    cout << cache.get(1) << endl;
    cache.put(3, 3);
    cout << cache.get(2) << endl;
    cout << cache.get(3) << endl;
    cache.put(4, 4);
    cout << cache.get(1) << endl;
    cout << cache.get(3) << endl;
    cout << cache.get(4) << endl;
    return 0;
}
```