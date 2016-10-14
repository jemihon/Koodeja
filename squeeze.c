#include <stdio.h>

#define MAXLINE 1000

void squeeze(char s1[], char s2[]);
int contains(char c, char s[]);
int any(char s1[], char s2[]);

int main(void)
{
    char s1[] = "wew lad";
    char s2[] = "xxx";
    printf("%d", any(s1,s2));
    
    return 0;
}

void squeeze(char s1[], char s2[])
{
    int i, j;
    
    for(i = j = 0; i < MAXLINE && s1[i] != '\0'; i++)
        if(!contains(s1[i],s2))
            s1[j++] = s1[i];
    s1[j] = '\0';
}

int contains(char c, char s[])
{
    int i;
    
    for(i = 0; i < MAXLINE && s[i] != '\0'; i++)
    {
        if(s[i] == c)
            return 1;
    }
    return 0;
}

int any(char s1[], char s2[])
{
    int i;
    
    for(i = 0; i < MAXLINE && s1[i] != '\0'; i++)
        if(contains(s1[i],s2))
            return i;
    return -1;
            
}