#include <stdio.h>

float ftoc(float f);

/* Ohjelmalla voi muuntaa Fahrenheit-asteita Celsiusasteiksi. */

float main(void)
{
    float fahrenheit;
    float muunnos;

    printf("Syötä lämpötila Fahrenheittina: ");
    scanf("%f", &fahrenheit);
    
    muunnos = ftoc(fahrenheit);
    printf("%3.0f", fahrenheit);
    printf(" F on");
    printf("%3.0f", muunnos);
    printf(" C.\n");

    return 0;
}

float ftoc(float f)
{
    return (f - 32) / 1.8;
}