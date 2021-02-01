using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour, IPoolable
{
    [SerializeField] [Range(0, 1)] private float chanceSpawn;
    protected bool isFree = true;

    public float ChanceSpawn { get => chanceSpawn; }

    public bool IsFree { get => isFree; }

    public abstract void OnTurnEnd();

    public abstract void ResetPoolable();
}
