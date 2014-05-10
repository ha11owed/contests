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
	// https://code.google.com/codejam/contest/351101/dashboard#s=p2

	// The map is larger then necessary, but this makes the search code simpler
	char* map[256];
	map['a'] = "2";
	map['b'] = "22";
	map['c'] = "222";

	map['d'] = "3";
	map['e'] = "33";
	map['f'] = "333";

	map['g'] = "4";
	map['h'] = "44";
	map['i'] = "444";

	map['j'] = "5";
	map['k'] = "55";
	map['l'] = "555";

	map['m'] = "6";
	map['n'] = "66";
	map['o'] = "666";

	map['p'] = "7";
	map['q'] = "77";
	map['r'] = "777";
	map['s'] = "7777";

	map['t'] = "8";
	map['u'] = "88";
	map['v'] = "888";

	map['w'] = "9";
	map['x'] = "99";
	map['y'] = "999";
	map['z'] = "9999";

	map[' '] = "0";

	char msg[1005];
	int problemCount, n;
	scanf("%d", &problemCount);
	gets(msg);

	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		gets(msg);
		printf("Case #%d: ", problemIdx);

		char last = 0;
		for(int i=0; i < 1001 && msg[i] != '\0'; i++) {
			char* enc = map[msg[i]];
			if(last == enc[0]) {
				printf(" ");
			}

			printf("%s", enc);
			last = enc[0];
		}

		printf("\n");
	}
}