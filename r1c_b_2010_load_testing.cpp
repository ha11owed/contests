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

namespace helper
{
	int cmpIntsAsc(const void * a, const void * b) { return ( *(int*)a - *(int*)b ); }
	int cmpIntsDesc(const void * a, const void * b) { return ( *(int*)b - *(int*)a ); }

	inline void qsortIntsAsc(int* values, int n) { qsort(values, n, sizeof(int), cmpIntsAsc); }
	inline void qsortIntsDesc(int* values, int n) { qsort(values, n, sizeof(int), cmpIntsDesc); }
}

FILE *fin, *fout;

// Add extra defines
//#define N 51

class ProblemSolver {
public:
	// DATA
	int l, p, c;
	int nrTests;

public:
	int minTests(int n, int m) {
		if(n * c >= m) {
			return 0;
		}

		int n1 = n * c;
		int m1 = (int) floor((double) m / c);
		
		return 1 + minTests(n1, m1);
	}

	void solve(int problemIdx) {
		nrTests = minTests(l, p);
	}

	void read(int problemIdx) {
		fscanf(fin, "%d", &l);
		fscanf(fin, "%d", &p);
		fscanf(fin, "%d", &c);
	}

	void write(int problemIdx) {
		fprintf(fout, "%d\n", nrTests);
	}
};

int main(int argc, char** argv) {

	fin = fopen("input.txt", "rt");
	fout = fopen("output.txt", "wt");
	//FILE *fout = stdout;

	int problemCount;
	ProblemSolver problemSolver;

	fscanf(fin,"%d", &problemCount);
	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		problemSolver.read(problemIdx);
		problemSolver.solve(problemIdx);
		fprintf(fout, "Case #%d:", problemIdx);
		problemSolver.write(problemIdx);
	}

	fclose(fin);
	fclose(fout);

	return 0;
}
