#include <stdio.h>
#include <math.h>

#define MAXLINE 1000

int htoi(char s[]);
void getline(char s[]);

int main(void)
{
    char numbers[MAXLINE];
    int result;
    //getline(numbers);
    result = htoi("dfd0X6f");
    printf("%d", result);
    return 0;
}

/* converts a string of hexadecimal digits into its equivalent integer value */
int htoi(char s[])
{
    int end, i, inc0x;
    double result, power;
    power = 0;
    
    
    for(i = 0; i < MAXLINE && s[i] != '\0'; i++)
        ;
    end = i;
    
    if(end > 1 && s[0] == '0' && (s[1] == 'x' || s[1] == 'X'))
        inc0x = 2;
    else inc0x = 0;
    
    
    for(i = end-1; i >= inc0x; i--)
    {
        if(s[i] >= '0' && s[i] <= '9')
            result += (s[i] - '0') * pow(16, power++);
        else if(s[i] >= 'a' && s[i] <= 'f')
            result += (10 + s[i] - 'a') * pow(16, power++);
        else if(s[i] >= 'A' && s[i] <= 'F')
            result += (10 + s[i] - 'A') * pow(16, power++);
        else return -1;
    }
    return (int)result;
    
}

void getline(char s[])
{
    int i, c;
    
    for(i = 0; i < MAXLINE && (c = getchar()) != EOF; i++)
        s[i] = c;
}