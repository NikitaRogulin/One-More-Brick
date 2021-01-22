using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int currentBulletsCount;

    private List<Bullet> shootedBullets; // выстреленные пули
    private Pool<Bullet> bullets;
    private Vector3 destination; // назначение 
    private bool canShoot = true;
    private bool isFirstBulletHitFloor;

    public static UnityEvent LastBullet = new UnityEvent();

    //состояния игры:
    //спаун игровых объектов
    //проигрыш
    //первая пуля приземлилась
    //конец хода
    //удаление игровых объектов
    //ожидание

    //ожидание <-> спаун игровых объектов
    //ожидание -> последняя пуля призмелилась
    //первая пуля приземлилась -> ожидание
    //первая пуля призмелилась -> конец хода
    //конец хода -> спаун игровых объектов
    //конец хода -> проигрыш


    //состояния:
    //в движении
    //ожидает
    //стреляющий

    //ожидает -> стреляющий (триггер - нажата лкм)    
    //ожидает -> в движении (триггер - первая пуля приземлилась)
    //стреляющий -> ожидает (триггер - пули закончились)
    //в движении -> ожидает (триггер - дошел до точки назначения)

    //действия:
    //стрелять
    //двигаться

    void Start()
    {
        AddBulletBaf.AddBullet.AddListener(() => currentBulletsCount++);

        destination = transform.position; // назначение = место расположению 
        bullets = new Pool<Bullet>(()=>Instantiate(bulletPrefab,transform.position,Quaternion.identity));
        shootedBullets = new List<Bullet>();
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
        for (int i = 0; i < buffer; i++)
        {
            Bullet bullet = bullets.GetPoolable();
            bullet.gameObject.SetActive(true);
            bullet.Push(direction);
            shootedBullets.Add(bullet);
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
        shootedBullets.Remove(bullet);

        if (currentBulletsCount - shootedBullets.Count == 1)
        {
            destination = new Vector3(bullet.transform.position.x, transform.position.y);// назначение = расположению пули по Х
            isFirstBulletHitFloor = true;
        }

        bullet.CollectTo(destination);
        bullet.HitFloorEvent.RemoveListener(OnBulletHitFloor);

        if (shootedBullets.Count == 0)
        {
            EndTurn();
        }
    }

    public void CollectBullets()
    {
        if (canShoot || !isFirstBulletHitFloor)
            return;

        for (int i = 0; i < shootedBullets.Count; i++)
        {
            Bullet bullet = shootedBullets[i];
            bullet.CollectTo(destination);
        }
        EndTurn();
    }

    private void EndTurn()
    {
        StartCoroutine(Move(destination));
        LastBullet.Invoke();
        currentBulletsCount++;
        shootedBullets.Clear();
    }
}
