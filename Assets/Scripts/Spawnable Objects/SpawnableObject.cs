using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float chanceSpawn;

    public float ChanceSpawn { get => chanceSpawn; }
}
