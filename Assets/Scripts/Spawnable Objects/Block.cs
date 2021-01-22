using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Block : SpawnableObject
{
    private Text blockHP;
    [SerializeField] private int health;

    private int Health
    {
        get => health;
        set
        {
            health = value;
            CurrentHp();
        }
    }

    private void Start()
    {
        //health = HPBlockController.StartHP;
        Health = HPBlockController.StartHP;
        blockHP = GetComponentInChildren<Text>();
        //CurrentHp();
    }

    private void Update()
    {
        //баффы которые спаунятся рандомно в течение хода и исчезают после него
        //баффы которые спаунятся в начале хода на линии и исчезают после него
        //баффы которые спаунятся в начале хода на линии и исчезают если пуля попадает в него
        //блоки которые спаунятся в начале хода на линии и исчезают если пуля попадает в него
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health--;
        //health--;
        //CurrentHp();
        if (Health == 0)
        {
            isFree = true;
            gameObject.SetActive(false);
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(0);
    }

    private void CurrentHp()
    {
        blockHP = GetComponentInChildren<Text>();
        blockHP.text = Health.ToString();
    }

    public override void ResetPoolable()
    {
        Health = HPBlockController.StartHP;
        //health = HPBlockController.StartHP;
        //CurrentHp();
    }
}
