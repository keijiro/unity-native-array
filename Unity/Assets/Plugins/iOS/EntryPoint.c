void WriteIntArray(int *array, int length)
{
    for (int i = 0; i < length; i++)
    {
        array[i] = i;
    }
}

void WriteVector3Array(float *array, int length)
{
    for (int i = 0; i < length * 3; i++)
    {
        array[i] = i;
    }
}
