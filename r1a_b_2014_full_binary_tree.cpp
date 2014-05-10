// -------------------------------------
// STATUS: OK SMALL, OK LARGE, DONE
// Full Binary Tree
// https://code.google.com/codejam/contest/2984486/dashboard#s=p1
//
// IDE: VC++ 2010
// OS Windows 7
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
//#define N 51
const int inf = 1 << 30;

int n;
int nDeleted;

vector<vector<int>>     graph;
vector<int>             vis;
vector<int>             tree;


void buildTreeRec(int root) {
	assert(vis[root] == 0);

	vis[root] = 1;
	tree[root] = 1;

	size_t m = graph[root].size();
	if(m <= 1) {
		// nothing to do... can't be a tree
	}
	else {
		int maxIdx1 = -1;
		int maxIdx2 = -1;
		for(size_t i = 0; i<m; i++) {
			int node = graph[root][i];

			if(vis[node] == 0) {
				buildTreeRec(node);
				if(maxIdx2 == -1 || maxIdx2 == -1) {
					// can still visit
					if(maxIdx1 == -1)
						maxIdx1 = node;
					else
						maxIdx2 = node;
				}
				else {
					// pick the max
					if(tree[maxIdx1] > tree[maxIdx2]) {
						if(tree[maxIdx2] < tree[node]) {
							maxIdx2 = node;
						}
					}
					else {
						if(tree[maxIdx1] < tree[node]) {
							maxIdx1 = node;
						}
					}
				}
			}
		}

		if(maxIdx1 != -1 && maxIdx2 != -1) {
			tree[root] += (tree[maxIdx1] + tree[maxIdx2]);
		}

	}

	vis[root] = 0;
}

void solve(int problemIdx) {
	nDeleted = n;
	
	for(int i=1; i<=n; i++) {
		size_t m = graph[i].size();
		if(m == 0) {
			continue;
		}
		else if(m == 1) {
			nDeleted = min(nDeleted, n - 1);
		}
		else {
			tree.resize(n+1, 1);
			vis.resize(n+1, 0);

			buildTreeRec(i);

			int deleted = n - tree[i];
			nDeleted = min(deleted, nDeleted);
		}
	}
}
	
void read(int problemIdx) {
	fscanf(fin, "%d", &n);

	graph.clear();
	graph.resize(n+1);
	
	for(int i=0; i<n-1; i++) {
		int a;
		int b;
		fscanf(fin,"%d", &a);
		fscanf(fin,"%d", &b);

		graph[a].push_back(b);
		graph[b].push_back(a);
	}
}

void write(int problemIdx) {
	fprintf(fout, " %d\n", nDeleted);
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
