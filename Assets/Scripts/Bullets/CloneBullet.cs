using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBullet : AbstractBullet
{
    protected override void BounceCountZero()
    {
        gameObject.SetActive(false);
    }
}
