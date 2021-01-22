using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : AbstractBullet
{
    public class HitEvent : UnityEvent<Bullet> { }
    public HitEvent HitFloorEvent = new HitEvent();

    public void CollectTo(Vector3 destination)
    {
        StartCoroutine(Move(destination));
        isFree = true;
    }

    IEnumerator Move(Vector3 destination)
    {
        rb.velocity = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;
        while (transform.position != destination)
        {
            //transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 10);
            transform.position = destination;
            yield return null;
        }
    }

    protected override void BounceCountZero()
    {
        rb.velocity = Vector3.zero;
        HitFloorEvent.Invoke(this);
    }

    public override void ResetPoolable()
    {
        base.ResetPoolable();
        HitFloorEvent.RemoveAllListeners();
    }
}
