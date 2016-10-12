#include <stdio.h>

#define MAXLINE 1000

void detab(char s[], int lim, int n);
void entab(char s[], int lim, int n);
void addchar(char s[], int n, int ind, int c);
void copy(char s1[], char s2[], int lim);
int countspaces(void);

int main()
{
    char line[MAXLINE];
    char result[MAXLINE];
    
    entab(line, MAXLINE, 4);
    copy(line, result, MAXLINE);
    printf("%s", result);
}

void detab(char s[], int lim, int n)
{
    int i, c;
    
    for(i = 0; i < lim && ((c = getchar()) != EOF); i++)
    {
        if(c == '\t')
        {
            addchar(s, n, i, ' ');
            i += n-1;
        }
            
        else s[i] = c;
    }
    s[i] = '\0';
        
}

void entab(char s[], int lim, int n)
{
    int i, j, c, spaces, tabcount, spacecount;
    spaces = 0;
    
    for(i = 0; i < lim && (c = getchar()) != EOF; i++)
    {
        if(c == ' ')
        {
            printf("%d", i);
            spaces++;
            for(j = 0; j < MAXLINE && (c = getchar()) == ' '; j++)
                spaces++;
            tabcount = spaces / n;
            addchar(s, tabcount, i, '\t');
            i += tabcount;
            spacecount = spaces - tabcount * n;
            addchar(s, spacecount, i, ' ');
            i += spacecount;
            printf("%d", i);
            s[i] = c;
            spaces = 0;
        }
        else s[i] = c;
    }
    s[i] = '\0';
}

int countspaces(void)
{
    int i, c, count;
    count = 1;
    for(i = 0; i < MAXLINE && (c = getchar()) == ' '; i++)
        count++;
    
    return count;
}

void addchar(char s[], int n, int ind, int c)
{
    int i;
    for(i = ind; i < ind+n; i++)
        s[i] = c;
}

void copy(char s1[], char s2[], int lim)
{
    int i;
    for(i = 0; i < lim && s1[i] != '\0'; i++)
        s2[i] = s1[i];
    s2[i] = '\0';
}



