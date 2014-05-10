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
	// https://code.google.com/codejam/contest/32016/dashboard#s=p0

	int vector1[801], vector2[801];
	int problemCount, n;
	scanf("%d", &problemCount);

	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		scanf("%d", &n);

		for(int i=0; i<n; i++) {
			scanf("%d", vector1 + i);
		}
		for(int i=0; i<n; i++) {
			scanf("%d", vector2 + i);
		}

		qsortIntsAsc(vector1, n);
		// NOTE: the vector contains an extra element that
		// will prevent the while condition to go any further.
		vector1[n] = vector1[n-1] + 1;
		qsortIntsDesc(vector2, n);
		vector2[n] = vector2[n-1] + 1;

		ll result = 0, acc;

		for(int i=0; i<n; i++) {
			acc = vector2[i];
			while(vector1[i] == vector1[i+1]) {
				i++;
				acc += vector2[i];
			}
			result += vector1[i] * acc;
		}

		printf("Case #%d: %lld\n", problemIdx, result);
	}
}