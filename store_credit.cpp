#include <stdio.h>

template<typename T> void swap( T& a, T& b ) {
	T temp = a;
	a = b;
	b = temp;
}

void solve();

int main(int argc, char** argv) {
	freopen("input.txt", "rt", stdin);
	freopen("output.txt", "wt", stdout);

	solve();

	return 0;
}

void solve() {
	int items[2001], prices[1001], count[1001];;
	int problemCount, n, credit;
	scanf("%d", &problemCount);

	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		scanf("%d", &credit);
		scanf("%d", &n);

		for(int i=0; i<=credit; i++) {
			prices[i]  = -1;
			count[i] = 1;
		}

		int pi;
		for(int i=0; i<n; i++) {
			scanf("%d", &pi);
			items[i] = pi;
			if(prices[pi] == -1) {
				prices[pi] = i;
			}
			else {
				count[pi]++;
			}
		}

		for(int i=0; i<credit; i++) {
			if(prices[i] != -1) {
				// TODO: there is also the case of a price being more then once
				if(items[prices[i]] + items[prices[credit - i]] == credit) {
					// found
					int idx1 = prices[i];
					int idx2 = prices[credit - i];
					if(idx1 > idx2) {
						swap(idx1, idx2);
					}
					printf("Case #%d: %d %d\n", problemIdx, idx1 + 1, idx2 + 1);
					break;
				}
			}
		}

	}
}