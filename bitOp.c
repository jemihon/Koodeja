#include <stdio.h>
#include <math.h>

unsigned setbits(unsigned x, int p, int n, unsigned y);
unsigned setbits2(unsigned x, int p, int n, unsigned y);
int binCount(unsigned x);

int main(void)
{
    unsigned result;
    unsigned lol = 16;
    unsigned wew = 6;
    //result = setbits(lol, 4, 3, wew);
    int num = 185;
    printf("%d", 0x1);
    //printf("%d", setbits2(lol, 2, 3, wew));
}

unsigned setbits(unsigned x, int p, int n, unsigned y)
{
    unsigned startbits, midbits, endbits;

    startbits = x >> p+1;
    midbits = y & ~(~0 << n);    
    endbits = x & ~(~0 << (p+1-n));
    
    int count = binCount(x);
    int startcount = binCount(startbits);
    int midcount = p;
    int endcount = binCount(endbits);
    
    unsigned result = startbits << (count - startcount) ;
    result += midbits << (count - startcount - p);
    result += endbits << (count - startcount - p - endcount);
    printf("%d %d %d ", startbits, midbits, endbits);
    printf("%d", result);
    return 0;
}

unsigned setbits2(unsigned x, int p, int n, unsigned y)
{    
    return ((y << (p+1-n)) & (((~(~0 << n)) << (p+1-n)))) | ((~((1 << n) - 1) ) & x);   
}


int binCount(unsigned x)
{
    double i;
    i = 0;
    while(i < 100)
    {
        if(pow(2, i) > x)
            return (int)i;
        i++;
    }
    return -1;
}

