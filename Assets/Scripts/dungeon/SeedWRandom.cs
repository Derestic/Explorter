using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SeedWRandom
{
    private static uint seed = (uint)(Random.Range(0f, 10000000.0f));//95613;//765;                                    // Semilla inicial
    public static float random()
    {
        SeedWRandom.seed *= (uint)(1105245 * seed + 24691) * (uint)(11052125 * seed + 212391) * (uint)(1101325 * seed + 212353); // Formula para simular el random
        return ((float)SeedWRandom.seed) / 2147483647.0f / 2.0f;                           // Devuelve la nueva semilla
    }
}