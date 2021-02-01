using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddBulletBaf : AbstractBaf
{
    public static UnityEvent AddBullet = new UnityEvent();

    public override void ResetPoolable()
    {
        
    }

    protected override void Handle(AbstractBullet bullet)
    {
        AddBullet.Invoke();
        gameObject.SetActive(false);
        isFree = true;
    }

    public override void OnTurnEnd()
    {
        
    }
}
