using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CoroutineExtensions
{
    public static IEnumerator WaitAll(this MonoBehaviour mono, List<IEnumerator> ienumerators)
    {
        return ienumerators.Select(mono.StartCoroutine).ToArray().GetEnumerator();
    }
}
