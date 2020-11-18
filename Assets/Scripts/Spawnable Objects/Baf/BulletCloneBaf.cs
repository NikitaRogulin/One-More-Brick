using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletCloneBaf : AbstractBaf
{
    private static List<AbstractBullet> buffedBullets = new List<AbstractBullet>();

    [SerializeField] private CloneBullet cloneBulletPrefab;
    protected override void Handle(AbstractBullet bullet)
    {
        if (!(bullet is CloneBullet) && !buffedBullets.Contains(bullet))
        {
            buffedBullets.Add(bullet);
            AbstractBullet newBullet = Instantiate(cloneBulletPrefab, transform.position, Quaternion.identity);
            newBullet.Push(new Vector3(Random.Range(0f, 1f) > 0.5f ? 1 : -1, Random.Range(0f, 1f) > 0.5f ? 1 : -1));
        }
    }

    protected override void DestroyObj()
    {
        buffedBullets = new List<AbstractBullet>();
        Destroy(gameObject);
    }
    
    //private int RandomFromRangeWithExceptions(int rangeMin, int rangeMax, params int[] exclude)
    //{
    //    var range = Enumerable.Range(rangeMin, rangeMax).Where(i => !exclude.Contains(i)); // плохой вариант!
    //    int index = Random.Range(0, rangeMax - exclude.Count());
    //    return range.ElementAt(index);
    //}
}
