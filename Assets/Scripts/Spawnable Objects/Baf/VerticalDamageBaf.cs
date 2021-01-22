using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDamageBaf : AbstractBaf
{
    [SerializeField] private GameObject dmgLinePrefab;
    protected override void LastBulletHandler()
    {
        gameObject.SetActive(false);
        isFree = true;
    }

    protected override void Handle(AbstractBullet bullet)
    {
        Instantiate(dmgLinePrefab, transform.position, Quaternion.Euler(0,0,90));
    }

    public override void ResetPoolable()
    {

    }
}
