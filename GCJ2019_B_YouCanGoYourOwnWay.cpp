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

struct Position
{
    int x;
    int y;
    int n;

    Position() {}
    Position(int n_) : x(1), y(1), n(n_) {}

    bool go(char step)
    {
        if (step == 'E')
        {
            if (x < n)
            {
                x++;
                return true;
            }
        }
        else if (step == 'S')
        {
            if (y < n)
            {
                y++;
                return true;
            }
        }
        return false;
    }

    void revert(char step)
    {
        if (step == 'E')
        {
            x--;
        }
        else if (step == 'S')
        {
            y--;
        }
    }
};

bool operator==(const Position& lhs, const Position& rhs)
{
    return lhs.x == rhs.x && lhs.y == rhs.y;
}

bool operator!=(const Position& lhs, const Position& rhs)
{
    return !(lhs == rhs);
}

char otherStep(char step)
{
    if (step == 'E')
        return 'S';
    if (step == 'S')
        return 'E';
    return 'X';
}

class ProblemSolver {
public:
    // DATA
    int n;
    size_t m;

    string pathLydia;
    string pathMe;

    Position me;
    Position lydia;

public:
    bool tryStep(size_t stepIndex)
    {
        char stepLydia = pathLydia[stepIndex];
        char stepMe = pathMe[stepIndex];

        if (lydia == me)
        {
            if (stepLydia == stepMe)
            {
                return false;
            }
        }

        if (me.go(stepMe))
        {
            lydia.go(stepLydia);
            return true;
        }
        return false;
    }

    void solve(int problemIdx)
    {
        me = Position(n);
        lydia = Position(n);

        size_t index = 0;
        while (index < m)
        {
            // Try closest to midle
            if (index >= pathMe.size())
            {
                char stepMe = me.x < me.y ? 'E' : 'S';
                pathMe.push_back(stepMe);
            }

            if (tryStep(index))
            {
                index++;
                continue;
            }

            // Try the other way
            pathMe[index] = otherStep(pathMe[index]);
            if (tryStep(index))
            {
                index++;
                continue;
            }

            // If nothing works, we need to go one step back until we reach a position with an alternative
            while (true)
            {
                bool isEq = (me == lydia);
                Position lydiaCopy = lydia;

                pathMe.pop_back();
                index--;
                me.revert(pathMe[index]);
                lydia.revert(pathLydia[index]);
                pathMe[index] = otherStep(pathMe[index]);

                if (isEq)
                {
                    Position meCopy = me;
                    if (meCopy.go(pathMe[index]))
                    {
                        if (meCopy != lydiaCopy)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    void read(int problemIdx)
    {
        pathLydia.clear();
        pathMe.clear();

        pathLydia.reserve(50000);
        pathMe.reserve(50000);

        fscanf(fin, "%d\n", &n);
        m = static_cast<size_t>(2 * n - 2);

        static char buffer[50004];
        fgets(buffer, 50004, fin);
        pathLydia.assign(buffer);
        if (pathLydia[pathLydia.size() - 1] == '\n')
        {
            pathLydia.pop_back();
        }
    }

    void generate()
    {
        int ri = rand() % pathLydia.size();
        char temp = pathLydia[0];
        pathLydia[0] = pathLydia[ri];
        pathLydia[ri] = temp;

        pathMe.clear();
    }

    void write(int problemIdx)
    {
        int ns = 0, ne = 0;
        for (char c : pathMe)
        {
            if (c == 'S')      { ns++; }
            else if (c == 'E') { ne++; }
        }
        if (ns != ne || ns != n - 1)
        {
            fprintf(fout, "ups... ");
        }
        fprintf(fout, "%s\n", pathMe.c_str());
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
        //while (true) {
        //problemSolver.solve(problemIdx);
        fprintf(fout, "Case #%d: ", problemIdx);
        problemSolver.write(problemIdx);
        //problemSolver.generate();
        //}
    }

    fclose(fin);
    fclose(fout);

    return 0;
}
