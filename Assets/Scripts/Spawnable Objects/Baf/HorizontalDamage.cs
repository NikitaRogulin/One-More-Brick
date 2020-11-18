using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDamage : AbstractBaf
{
    private static List<AbstractBullet> buffedBullets = new List<AbstractBullet>();
    [SerializeField] private GameObject dmgLinePrefab;

    private bool isFired;

    protected override void LastBulletHandler()
    {
        if (isFired)
        {
            buffedBullets = new List<AbstractBullet>();
            Destroy(gameObject);
        }
    }

    protected override void Handle(AbstractBullet bullet)
    {
        if (!buffedBullets.Contains(bullet))
        {
            isFired = true;
            Instantiate(dmgLinePrefab, transform.position, Quaternion.identity);
            buffedBullets.Add(bullet);
        }
    }
}