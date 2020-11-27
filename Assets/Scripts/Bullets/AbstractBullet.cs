using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float power;
    protected Rigidbody2D rb;
    protected bool isFree = true;

    public bool IsFree { get => isFree; }
    public int BounceCount { get; set; } = 1;

    public void Push(Vector3 direction)
    {
        isFree = false;
        BounceCount = 1;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * power, ForceMode2D.Impulse);
        GetComponent<Collider2D>().enabled = true;
    }

    protected abstract void BounceCountZero();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            BounceCount--;

            if (BounceCount == 0)
                BounceCountZero();
        }
    }

    public void ResetPoolable()
    {
        BounceCount = 1;
    }
}
