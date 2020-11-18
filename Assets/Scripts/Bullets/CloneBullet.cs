using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBullet : AbstractBullet
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected override void BounceCountZero()
    {
        Destroy(gameObject);
    }
}// Push клонированных пуль ???
// Булет манагер
// 
