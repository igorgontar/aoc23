class Algo
{
    public static long Extrapolate(long[] a)
    {
        int n = a.Length;
        for (;;)
        {
            bool allZero = true;
            for (int i = 0; i < n - 1; i++)
            {
                long diff = a[i + 1] - a[i];
                a[i] = diff;
                if (diff != 0)
                    allZero = false;
            }
            
            if(allZero)
            {
                long sum = a.Sum(); 
                return sum;       
            }
            n = n-1;
        }

        throw new Exception("invalid input");
    }
}