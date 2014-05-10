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
	// https://code.google.com/codejam/contest/1460488/dashboard#s=p0

	char mapping[257];
	char inversed[257];

	const char *knownEncodedText[3] = {
		"ejp mysljylc kd kxveddknmc re jsicpdrysi",
		"rbcpc ypc rtcsra dkh wyfrepkym veddknkmkrkcd",
		"de kr kd eoya kw aej tysr re ujdr lkgc jv"
	};

	const char *knownPlainText[3] = {
		"our language is impossible to understand",
		"there are twenty six factorial possibilities",
		"so it is okay if you want to just give up"
	};

	for(int i=0; i<257; i++) {
		mapping[i] = 0;
		inversed[i] = 0;
	}

	mapping[' '] = ' ';
	mapping['a'] = 'y';
	mapping['o'] = 'e';
	mapping['z'] = 'q';
	mapping['q'] = 'z';

	inversed[' '] = ' ';
	inversed['y'] = 'a';
	inversed['e'] = 'o';
	inversed['q'] = 'z';
	inversed['z'] = 'q';

	for(int i=0; i<3; i++) {
		for(int j=0; knownEncodedText[i][j] != '\0'; j++) {
			char plain = knownPlainText[i][j];
			char enc = knownEncodedText[i][j];
			
			mapping[plain] = enc;
			inversed[enc] = plain;
		}
	}
	
	char msg[105];
	int problemCount, n;
	scanf("%d", &problemCount);
	gets(msg);

	for(int problemIdx = 1; problemIdx <= problemCount; problemIdx++) {
		gets(msg);
		printf("Case #%d: ", problemIdx);

		for(int i=0; i < 1001 && msg[i] != '\0'; i++) {
			char plain = inversed[msg[i]];
			if(plain == 0) {
				printf("%c", '-');
			}
			else {
				printf("%c", plain);
			}
		}
		printf("\n");
	}
}