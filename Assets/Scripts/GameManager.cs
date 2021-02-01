using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    [SerializeField] private HPBlockController hPBlockController;
    [SerializeField] private Text text;
    [SerializeField] private Player player;
    [SerializeField] private Grid grid;

    public UnityEvent TurnEnded = new UnityEvent();

    void Start()
    {
        TurnEnded.AddListener(grid.OnTurnEnd);
        TurnEnded.AddListener(hPBlockController.HpUp);
        TurnEnded.AddListener(grid.ShiftDown);
        TurnEnded.AddListener(grid.SpawnLine);
        player.LastBullet.AddListener(OnTurnEnd);
        //TurnEnded.AddListener(AddScore);
    }

    void OnTurnEnd()
    {
        TurnEnded.Invoke();
        AddScore();
    }

    private void AddScore()
    {
        score++;
        text.text = "Level : " + score.ToString();
    }
}
