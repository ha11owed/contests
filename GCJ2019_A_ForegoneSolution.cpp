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
using ll = long long;

static FILE *fin, *fout;

#define LOCAL_TEST

class ProblemSolver {
public:
    // DATA
    string input, a, b;
    size_t st;

public:
    void solve(int problemIdx) {
        size_t n = input.size();
        st = n;
        for (size_t i=0; i<n; i++)
        {
            int d = input[i] - '0';
            if (d == 4)
            {
                a.push_back('3');
                b.push_back('1');
                if (st == n)
                {
                    st = i;
                }
            }
            else
            {
                a.push_back(input[i]);
                if (st != n)
                {
                    b.push_back('0');
                }
            }
        }
        if (b.size() == 0)
        {
            b.push_back('0');
        }
    }

    void read(int problemIdx) {
        input.clear();
        a.clear();
        b.clear();

        char buffer[128];
        fgets(buffer, 128, fin);
        for (size_t i=0; buffer[i] != '\0'; i++)
        {
            if (buffer[i] != '\n')
                input.push_back(buffer[i]);
        }
    }

    void write(int problemIdx) {
        fprintf(fout, "%s %s\n", a.c_str(), b.c_str());
    }
};

int main(int, char**) {

#ifdef LOCAL_TEST
    fin = fopen("/home/alin/projects/contests/input.txt", "rt");
    fout = stdout;
#else
    //fin = fopen("input.txt", "rt");
    //fout = fopen("output.txt", "wt");
    fin = stdin;
    fout = stdout;
#endif

    int problemCount;
    ProblemSolver problemSolver;

    fscanf(fin, "%d\n", &problemCount);
    for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
        problemSolver.read(problemIdx);
        problemSolver.solve(problemIdx);
        fprintf(fout, "Case #%d: ", problemIdx);
        problemSolver.write(problemIdx);
    }

    fclose(fin);
    fclose(fout);

    return 0;
}
