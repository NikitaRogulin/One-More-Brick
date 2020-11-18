using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text text;
    private int score;

    void Start()
    {
        Player.LastBullet.AddListener(AddScore);
    }

    private void AddScore()
    {
        score++;
        text.text = "Level : " + score.ToString();
    }
}
