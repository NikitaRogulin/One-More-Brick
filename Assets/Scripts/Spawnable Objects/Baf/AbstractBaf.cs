using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaf : SpawnableObject
{
    private void Start()
    {
        // Player.LastBullet.AddListener(() => Destroy(gameObject)); НА ЗАМЕТКУ!!!
    }

    protected abstract void Handle(AbstractBullet bullet);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out AbstractBullet bullet))
            Handle(bullet);
        if (collision.gameObject.tag == "LoseBorder")
            Destroy(gameObject);
    }
}
