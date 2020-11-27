using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDamageBaf : AbstractBaf
{
    [SerializeField] private GameObject dmgLinePrefab;


    protected override void LastBulletHandler()
    {
        Destroy(gameObject);
    }

    protected override void Handle(AbstractBullet bullet)
    {
        Instantiate(dmgLinePrefab, transform.position, Quaternion.identity);
    }
}