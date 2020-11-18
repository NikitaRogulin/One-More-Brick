using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletJumpFloorBaf : AbstractBaf
{
    private static List<AbstractBullet> buffedBullets = new List<AbstractBullet>();
    //void Start()
    //{
    //    // Player.LastBullet.AddListener(() => Destroy(gameObject)); НА ЗАМЕТКУ!!!
    //}

    protected override void Handle(AbstractBullet bullet)
    {
        if (!buffedBullets.Contains(bullet))
            bullet.BounceCount = 3;
    }

    protected override void LastBulletHandler()
    {
        buffedBullets = new List<AbstractBullet>();
        Destroy(gameObject);
    }
}
