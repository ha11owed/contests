// -------------------------------------
// STATUS: 
// <<Problem Name>>
// -------------------------------------
#ifdef WIN32
#define _CRT_SECURE_NO_WARNINGS
#endif

#define _USE_MATH_DEFINES
#include <fstream>
#include <vector>
#include <set>
#include <map>
#include <cstring>
#include <string>
#include <cmath>
#include <cassert>
#include <ctime>
#include <algorithm>
#include <sstream>
#include <list>
#include <queue>
#include <deque>
#include <stack>
#include <cstdlib>
#include <cstdio>
#include <iterator>
#include <functional>
#include <bitset>

using namespace std;

typedef long long ll;
typedef long double ld;

const int inf = 1 << 30;

FILE *fin, *fout;

inline char my_readChar() { char c;	do { fscanf(fin, "%c", &c); } while(c == '\r' || c == '\n'); return c; }

// Add extra defines
#define T 10002

int v[T];
int n, k;
int nFlips;
	
void read(int problemIdx) {
	int r;
	char c;

	n = 0;
	k = 0;
	while (true) {
		r = fscanf(fin, "%c", &c);
		if (c == '+') {
			v[n] = 1;
		}
		else if (c == '-') {
			v[n] = 0;
		}
		else {
			break;
		}
		n++;
	}
	v[n] = 9999;
	r = fscanf(fin, "%d\n", &k);

	printf("%d %d\n", n, k);
}

void flipAsc(int index) {
	int end = index + k;
	assert(end <= n);

	for (int i = index; i < end; i++) {
		v[i] = 1 - v[i];
	}
}

void solve(int problemIdx) {
	nFlips = 0;
	for (int i = 0; i < n - k + 1; i++) {
		if (v[i] == 0) {
			flipAsc(i);
			nFlips++;
		}
	}
}

void write(int problemIdx) {
	int cnt = 0;
	for (int i = 0; i < n; i++) {
		cnt += v[i];
	}
	if (cnt == n) {
		fprintf(fout, "%d", nFlips);
	}
	else {
		fprintf(fout, "IMPOSSIBLE");
	}
}

int main(int argc, char** argv) {

	fin = fopen("input.txt", "rt");
	fout = fopen("output.txt", "wt");
	//FILE *fout = stdout;

	int problemCount;
	fscanf(fin, "%d\n", &problemCount);
	for (int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		read(problemIdx);
		solve(problemIdx);
		fprintf(fout, "Case #%d: ", problemIdx);
		write(problemIdx);
		if (problemIdx < problemCount) {
			fprintf(fout, "\n");
		}
	}

	fclose(fin);
	fclose(fout);

	return 0;
}
