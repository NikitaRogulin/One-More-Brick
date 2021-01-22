using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : IPool<T> where T : IPoolable
{
    private Func<T> create;
    private List<T> poolables = new List<T>();

    public Pool(Func<T> createPoolable)
    {
        create = createPoolable;
    }

    public T GetPoolable() 
    {
        foreach(T element in poolables)
        {
            if (element.IsFree)
            {
                element.ResetPoolable(); // Обчихать с Владом!
                return element;
            }
        }

        T newPoolable = create();
        poolables.Add(newPoolable);
       
        return newPoolable;
    }
}

