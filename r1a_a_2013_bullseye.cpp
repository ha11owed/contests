// -------------------------------------
// STATUS: 
// Load Testing
// https://code.google.com/codejam/contest/619102/dashboard#s=p1
//
// IDE: VC++ 2010
// OS Windows 7
// -------------------------------------
#ifdef WIN32
#define _CRT_SECURE_NO_WARNINGS
#endif

#define _USE_MATH_DEFINES
#include <cstdio>
#include <cstdlib>
#include <cmath>
#include <cassert>

#include <vector>
#include <list>
#include <map>
#include <set>
#include <queue>
#include <deque>
#include <stack>
//#include <bitset>
#include <algorithm>
#include <functional>

using namespace std;

#ifdef NDEBUG
#define ASSERT(x) 
#define FAIL()
#else
#define ASSERT(x)    assert(x)
#define FAIL(msg)    assert(msg)
#endif

typedef long long ll;

FILE *fin, *fout;

namespace helper
{
	int cmpIntsAsc(const void * a, const void * b) { return ( *(int*)a - *(int*)b ); }
	int cmpIntsDesc(const void * a, const void * b) { return ( *(int*)b - *(int*)a ); }

	inline void qsortIntsAsc(int* values, int n) { qsort(values, n, sizeof(int), cmpIntsAsc); }
	inline void qsortIntsDesc(int* values, int n) { qsort(values, n, sizeof(int), cmpIntsDesc); }

	inline char readChar() {
		char c;
		do {
			fscanf(fin, "%c", &c);
		}
		while(c == '\r' || c == '\n');
		return c;
	}
}


// Add extra defines
//#define N 51
ll r;
ll t;
ll n;

void solve(int problemIdx) {
	n = 0;
	long double T = (long double)t;
	// (2r+1)pi
	// 2r*n + n(n-1) + n = n^2 - n + n + 2r*n = n^2 + 2r*n = n (n + 2r) = tPerPi
	// ax^2 + bx + c = 0 => x = (-b +- sqrt(b^2 - 4ac)) / 2a
	//
	// n^n + 2rn - tPerPi = 0
	// => n = -2r + sqrt(4r^2 - 4tPerPi) / 2 = -r + sqrt(r^2 - tPerPi)

	long double nD = (sqrt(4.0L*r*r-4.0L*r+1L+8L*T) + 1L - 2*r) / 4L;
	n = (ll)floor(nD);
}
	
void read(int problemIdx) {
	fscanf(fin, "%lld", &r);
	fscanf(fin, "%lld", &t);
}

void write(int problemIdx) {
	fprintf(fout, " %lld\n", n);
}

int main(int argc, char** argv) {

	fin = fopen("input.txt", "rt");
	fout = fopen("output.txt", "wt");
	//FILE *fout = stdout;

	int problemCount;
	fscanf(fin,"%d", &problemCount);
	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		read(problemIdx);
		solve(problemIdx);
		fprintf(fout, "Case #%d:", problemIdx);
		write(problemIdx);
	}

	fclose(fin);
	fclose(fout);

	return 0;
}
