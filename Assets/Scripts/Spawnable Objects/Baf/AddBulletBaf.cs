using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddBulletBaf : AbstractBaf
{
    public static UnityEvent AddBullet = new UnityEvent();

    protected override void Handle(AbstractBullet bullet)
    {
        AddBullet.Invoke();
        Destroy(gameObject);
    }

    protected override void LastBulletHandler()
    {
        
    }
}
