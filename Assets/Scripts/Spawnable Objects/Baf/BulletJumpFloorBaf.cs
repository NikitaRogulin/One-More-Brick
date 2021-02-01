using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletJumpFloorBaf : AbstractBaf
{
    private static List<AbstractBullet> buffedBullets = new List<AbstractBullet>();
    private bool isFired;

    public override void ResetPoolable()
    {
        isFired = false;
    }

    protected override void Handle(AbstractBullet bullet)
    {
        if (!buffedBullets.Contains(bullet))
        {
            isFired = true;
            bullet.BounceCount = 3;
        }   
    }

    public override void OnTurnEnd()
    {
        if (isFired)
        {
            buffedBullets = new List<AbstractBullet>();
            gameObject.SetActive(false);
            isFree = true;
        }
    }
}
