#include <stdio.h>

void lower(char s1[]);

int main()
{
    unsigned int x = 345;
    char s[] = "aYY lMao";
    printf("%d\n", bitcount(x));
    lower(s);
    printf("%s", s);
    x &= (x-1); // poistaa oikeanpuoleisimman 1-bitin
    
    //printf("%d", x);
}

void lower(char s1[])
{
    int i;
    for(i = 0; s1[i] != '\0'; i++)
        s1[i] = islower(s1[i]) ? s1[i] : tolower(s1[i]);
   
}

int bitcount(unsigned x)
{
    int b;
    
    for(b = 0; x != 0; x &= (x-1))
            b++;
    return b;
}
