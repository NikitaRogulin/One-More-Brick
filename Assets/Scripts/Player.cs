using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int currentBulletsCount;

    private int shootedBullets; // выстреленные пули
    private Pool<Bullet> bullets;
    private Vector3 destination; // назначение 
    private bool canShoot = true;
    private bool isFirstBulletHitFloor;

    public UnityEvent LastBullet = new UnityEvent();
    private UnityEvent BulletsCollected = new UnityEvent();

    void Start()
    {
        AddBulletBaf.AddBullet.AddListener(() => currentBulletsCount++);

        destination = transform.position; // назначение = место расположению 
        bullets = new Pool<Bullet>(()=>Instantiate(bulletPrefab,transform.position,Quaternion.identity));
        //SpawnBullets(startBulletsCount);
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

        int buffer = currentBulletsCount;
        shootedBullets = currentBulletsCount;

        for (int i = 0; i < buffer; i++)
        {
            Bullet bullet = bullets.GetPoolable();
            bullet.gameObject.SetActive(true);
            bullet.Push(direction);
            BulletsCollected.AddListener(() => bullet.CollectTo(destination));
            bullet.HitFloorEvent.AddListener(OnBulletHitFloor);
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
        //bullet.HitFloorEvent.RemoveListener(OnBulletHitFloor);
        shootedBullets--;

        if (currentBulletsCount - shootedBullets == 1)
        {
            destination = new Vector3(bullet.transform.position.x, transform.position.y);// назначение = расположению пули по Х
            isFirstBulletHitFloor = true;
        }

        bullet.CollectTo(destination);
        bullet.HitFloorEvent.RemoveListener(OnBulletHitFloor);

        if (shootedBullets == 0)
        {
            EndTurn();
        }
    }

    public void CollectBullets()
    {
        if (canShoot || !isFirstBulletHitFloor)
            return;

        BulletsCollected.Invoke();
        BulletsCollected.RemoveAllListeners();

        EndTurn();
    }

    private void EndTurn()
    {
        StartCoroutine(Move(destination));
        LastBullet.Invoke();
        currentBulletsCount++;
    }
}
