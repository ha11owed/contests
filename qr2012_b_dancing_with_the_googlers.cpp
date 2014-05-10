#include <stdio.h>
#include <stdlib.h>

typedef long long ll;

template<typename T> void swap( T& a, T& b ) { T temp = a; a = b; b = temp; }

int cmpIntsAsc(const void * a, const void * b) { return ( *(int*)a - *(int*)b ); }
int cmpIntsDesc(const void * a, const void * b) { return ( *(int*)b - *(int*)a ); }

inline void qsortIntsAsc(int* values, size_t n) { qsort(values, n, sizeof(int), cmpIntsAsc); }
inline void qsortIntsDesc(int* values, size_t n) { qsort(values, n, sizeof(int), cmpIntsDesc); }

void solve();

int main(int argc, char** argv) {
	freopen("input.txt", "rt", stdin);
	freopen("output.txt", "wt", stdout);

	solve();

	return 0;
}

void solve() {
	// https://code.google.com/codejam/contest/1460488/dashboard#s=p1
		
	int scores[102];
	int problemCount, n, s, p;
	scanf("%d", &problemCount);
	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		scanf("%d", &n);
		scanf("%d", &s);
		scanf("%d", &p);

		// General case: p>=2 && p <=10
		// S: 1 1 3 => 5
		// S: 1 2 3 => 6
		// S: 1 3 3 => 7
		// N: 2 2 3 => 7
		// N: 2 3 3 => 8
		// N: 3 3 3 => 9
		int cnt = 0, usedS = 0;
		int minFit = p * 3 - 2;
		int minS = minFit - 2;

		if(p == 1) {
			// S: 0 0 1
			// S: 0 
			// s: 1 1 2
			minFit = 1;
			minS = 1;
		}
		if(p == 0) {
			// S: 0
			// N: 0
			minFit = 0;
		}

		for(int i=0; i<n; i++) {
			scanf("%d", scores + i);
			if(scores[i] >= minFit) {
				cnt++;
			}
			else if(scores[i] >= minS && usedS < s) {
				cnt++;
				usedS++;
			}
		}

		printf("Case #%d: %d\n", problemIdx, cnt);
	}
}