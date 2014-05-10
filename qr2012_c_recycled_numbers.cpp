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
	//freopen("output.txt", "wt", stdout);

	solve();

	return 0;
}

void solve() {
	// https://code.google.com/codejam/contest/1460488/dashboard#s=p2

	int problemCount, m, n;
	scanf("%d", &problemCount);
	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		scanf("%d", &n);
		scanf("%d", &m);

		printf("Case #%d: %d\n", problemIdx, cnt);
	}
}