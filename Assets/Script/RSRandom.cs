using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSRandom {

    static uint seed = 1;

    public static int Random (int n)
    {
        int val = Mathf.FloorToInt(NextDouble() * (n+1));
        return val;
    }

    public static float Value (float n=1f)
    {
        float val = NextDouble()*n;
        return val;
    }

    public static int Range (int min, int max, bool include=false)
    {
        int val = Mathf.FloorToInt(NextDouble() * (max-min+(include?1:0))) + min;
        return val;
    }

    public static float Range (float min, float max, bool include=false)
    {
        float val = NextDouble() * (max-min+(include?1:0)) + min;
        return val;
    }

    public static void SetSeed (int newSeed)
    {
        SetSeed((uint)newSeed);
    }

    public static void SetSeed (uint newSeed)
    {
        if (newSeed == 0) newSeed = 1;
        seed = newSeed;
        // generate a dummy number. Otherwise the first random number will be the same for lots of seeds.
        Generate (); 
    }

    public static uint GetSeed ()
    {
        return seed;
    }


    static uint NextInt ()
    {
        //returns a random float from 0.0f to 1.1f
        return Generate();
    }

    static float  NextDouble ()
    {
        return Generate() / 2147483647.0f;
    }

    static uint Generate ()
    {
        //integer version 1, for max int 2^46 - 1 or larger
        return seed = (seed * 16807) % 2147483647;
    }

    public static int WeightedRandom(int min, int max, float gamma) {
        int offset= max-min+1;
        return Mathf.FloorToInt(min+Mathf.Pow(Value(), gamma)*offset);
    }
}
