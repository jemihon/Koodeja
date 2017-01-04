#include <stdio.h>

int main()
{
    int arr[] = { 1, 3, 3, 5, 7, 8, 12, 23, 23, 25 };
    
    printf("%d\n", binsearch(23, arr, 10));
    printf("%d", newbinsearch(23, arr, 10));
}

int newbinsearch(int x, int v[], int n)
{
    int low, high, mid;
    
    low = 0;
    high = n - 1;
    while (low <= high && v[mid] != x) {
        mid = (low+high) / 2;
        if(x < v[mid])
            high = mid - 1;
        else 
            low = mid + 1;
    }
    if(v[mid] == x)
        return mid;
    
    return -1;
}

int binsearch(int x, int v[], int n)
{
    int low, high, mid;
    
    low = 0;
    high = n - 1;
    while (low <= high) {
        mid = (low+high) / 2;
        if(x < v[mid])
            high = mid - 1;
        else if (x > v[mid])
            low = mid + 1;
        else
            return mid;
    }
    return -1;
}