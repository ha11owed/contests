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
#define N 52
#define BLUE 'B'
#define RED  'R'

char board[N][N];

class ProblemSolver {
public:
	// DATA
	int n, k, winner;

public:
	void rotate() {
		for(int i=1; i<=n; i++) {
			for(int j=n; j>0; j--) {
				int jj = j;
				while(board[i][j] == '.') {
					j--;
				}
				if(j > 0 && j != jj) {
					std::swap(board[i][jj], board[i][j]);
					j = jj;
				}
			}
		}
	}

	int chech(int si, int sj, int di, int dj) {
		int redWin = 0;
		int blueWin = 0;
		int nb = 0;
		int nr = 0;

		int i=si, j=sj;
		while(i >= 1 && j >= 1 && i <= n && j <= n) {
			switch(board[i][j]) {
			case RED:
				nr ++;
				nb = 0;
				break;
			case BLUE:
				nb ++;
				nr = 0;
				break;
			default:
				nr = nb = 0;
				break;
			}
			i += di;
			j += dj;
			if(nr == k)
				redWin = 1;
			if(nb == k)
				blueWin = 2;
		}

		return redWin | blueWin;
	}

	void solve(int problemIdx) {
		rotate();
		print(problemIdx);

		winner = 0;
		
		// line
		for(int i=1; i<=n && winner != 4; i++) {
			winner |= chech(i, 1, 0, 1);
		}
		if(winner == 4) { return; }
		// column
		for(int j=1; j<=n && winner != 4; j++) {
			winner |= chech(1, j, 1, 0);
		}
		if(winner == 4) { return; }

		// diag1
		for(int i=1; i<=n && winner != 4; i++) {
			winner |= chech(i, 1, 1, 1);
		}
		if(winner == 4) { return; }

		for(int j=1; j<=n && winner != 4; j++) {
			winner |= chech(1, j, 1, 1);
		}
		if(winner == 4) { return; }
		// diag2
		for(int i=1; i<=n && winner != 4; i++) {
			winner |= chech(i, 1, -1, 1);
		}
		if(winner == 4) { return; }

		for(int j=1; j<=n && winner != 4; j++) {
			winner |= chech(n, j, -1, 1);
		}
		if(winner == 4) { return; }
	}

	void print(int problemIdx) {
		printf("problem: %d\n", problemIdx);
		for(int i=1; i<=n; i++) {
			printf("%s\n", board[i] + 1);
		}
	}
	
	void read(int problemIdx) {
		fscanf(fin, "%d", &n);
		fscanf(fin, "%d", &k);

		for(int i=1; i<=n; i++) {
			for(int j=1; j<=n; j++) {
				board[i][j] = readChar();
			}
			board[i][n+1] = 0;
		}
		//print(problemIdx);
	}

	char readChar() {
		char c;
		do {
			fscanf(fin, "%c", &c);
		}
		while(c == '\r' || c == '\n');
		return c;
	}

	void write(int problemIdx) {
		if(winner == 0) {
			fprintf(fout, " Neither\n");
		}
		else if(winner == 1) {
			fprintf(fout, " Red\n");
		}
		else if(winner == 2) {
			fprintf(fout, " Blue\n");
		}
		else {
			fprintf(fout, " Both\n");
		}
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
