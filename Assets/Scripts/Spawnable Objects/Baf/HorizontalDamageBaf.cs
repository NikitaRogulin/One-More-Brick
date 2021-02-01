using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDamageBaf : AbstractBaf
{
    [SerializeField] private GameObject dmgLinePrefab;


    public override void OnTurnEnd()
    {
        gameObject.SetActive(false);
        isFree = true;
    }

    protected override void Handle(AbstractBullet bullet)
    {
        Instantiate(dmgLinePrefab, transform.position, Quaternion.identity);
    }

    public override void ResetPoolable()
    {
        
    }
}