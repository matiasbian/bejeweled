using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions 
{
    public static void AddIfNotNull<T>(this List<T> list, T item)
    {
        if (item != null) list.Add(item); 
    }
}
