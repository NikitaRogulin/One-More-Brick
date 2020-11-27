using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int startBulletsCount;

    private List<Bullet> shootedBullets;// выстреленные пули
    private List<Bullet> bulletsPool; //все пули
    private Vector3 destination; // назначение 
    private bool canShoot = true;
    private bool isFirstBulletHitFloor;

    public static UnityEvent LastBullet = new UnityEvent(); 

    void Start()
    {
        AddBulletBaf.AddBullet.AddListener(SpawnBullet);
        destination = transform.position; // назначение = место расположению 

        bulletsPool = new List<Bullet>();
        shootedBullets = new List<Bullet>();
        SpawnBullets(startBulletsCount);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = new Vector3(mousePosInWorld.x - transform.position.x, mousePosInWorld.y - transform.position.y,0).normalized;

            StartCoroutine(Shoot(direction));
        }
    }

    IEnumerator Shoot(Vector3 direction)
    {
        isFirstBulletHitFloor = false;
        canShoot = false;
        var bulletCount = bulletsPool.Count;
        for (; bulletCount > 0; bulletCount--)
        {
            Bullet bullet = bulletsPool[bulletCount-1];
            bullet.gameObject.SetActive(true);// делаем пулю активной при выстреле 
            bullet.Push(direction);
            shootedBullets.Add(bullet);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Move(Vector3 destination)
    {
        while(transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 5);
            yield return null;
        }
        canShoot = true;
    }

    private void OnBulletHitFloor(Bullet bullet)
    {
        if (!shootedBullets.Contains(bullet))
            return;
        shootedBullets.Remove(bullet);

        //var magazineCount = 0;

        //foreach(Bullet b in bulletsPool)
        //{
        //    if (!shootedBullets.Contains(b)) 
        //        magazineCount++;
        //}

        if (bulletsPool.Count - shootedBullets.Count == 1)
        {
            destination = new Vector3(bullet.transform.position.x, transform.position.y);// назначение = расположению пули по Х
            isFirstBulletHitFloor = true;
        }
            bullet.MoveTo(destination);

        if (shootedBullets.Count == 0)
        {
            StartCoroutine(Move(destination));
            LastBullet.Invoke();
            SpawnBullet();
        }
    }

    private void SpawnBullets(int count)
    {
        for (int i = 0; i < count; i++)
            SpawnBullet();
    }

    public void SpawnBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, destination, Quaternion.identity);
        bullet.HitFloorEvent.AddListener(OnBulletHitFloor);
        bulletsPool.Add(bullet);//добавляет пулю во все
    }

    public void CollectBullets()
    {
        if (canShoot || !isFirstBulletHitFloor)
            return;
        int count = bulletsPool.Count;
        for (int i = 0; i < count; i++)
        {
            Bullet bullet = bulletsPool[i];
            OnBulletHitFloor(bullet);
        }
    }
}
