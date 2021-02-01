using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletCloneBaf : AbstractBaf
{
    private List<AbstractBullet> buffedBullets = new List<AbstractBullet>();

    [SerializeField] private CloneBullet cloneBulletPrefab;
    private static Pool<CloneBullet> cloneBulletsPool;

    protected override void Handle(AbstractBullet bullet)
    {
        cloneBulletsPool = new Pool<CloneBullet>(() => Instantiate(cloneBulletPrefab, transform.position, Quaternion.identity));
        if (!(bullet is CloneBullet) && !buffedBullets.Contains(bullet))
        {
            CloneBullet cloneBullet = cloneBulletsPool.GetPoolable();
            buffedBullets.Add(bullet);
            cloneBullet.Push(new Vector3(Random.Range(0f, 1f) > 0.5f ? 1 : -1, Random.Range(0f, 1f) > 0.5f ? 1 : -1));
        }
    }

    public override void OnTurnEnd()
    {
        buffedBullets = new List<AbstractBullet>();
        gameObject.SetActive(false);
        isFree = true;
    }

    public override void ResetPoolable()
    {
        
    }

    //private int RandomFromRangeWithExceptions(int rangeMin, int rangeMax, params int[] exclude)
    //{
    //    var range = Enumerable.Range(rangeMin, rangeMax).Where(i => !exclude.Contains(i)); // плохой вариант!
    //    int index = Random.Range(0, rangeMax - exclude.Count());
    //    return range.ElementAt(index);
    //}
}
