// -------------------------------------
// STATUS: done for small and large.
// Rope Intranet
// https://code.google.com/codejam/contest/619102/dashboard#s=p0
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
	vector<pair<int, int>> graph;
	size_t n;
	size_t m;

public:
	bool isIntersection(int i, int j) {
		int a1 = graph[i].first;
		int b1 = graph[i].second;
		int a2 = graph[j].first;
		int b2 = graph[j].second;
		return (a1 > a2 && b2 > b1) || (a2 > a1 && b1 > b2);
	}

	void solve(int problemIdx) {
		m = 0;
		for(size_t i=0; i<n; i++) {
			for(size_t j=i+1; j<n; j++) {
				if(isIntersection(i, j)) {
					m++;
				}
			}
		}
	}

	void read(int problemIdx) {
		size_t a, b;
		fscanf(fin, "%d", &n);
		graph.resize(n);
		for(size_t i=0; i<n; i++) {
			fscanf(fin, "%d", &a);
			fscanf(fin, "%d", &b);
			graph[i] = std::pair<int, int>(a, b);
		}
	}

	void write(int problemIdx) {
		fprintf(fout, " %d\n", m);
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
