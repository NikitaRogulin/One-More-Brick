using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T> where T : IPoolable
{
    T GetPoolable();
}
