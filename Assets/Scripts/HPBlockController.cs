using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBlockController : MonoBehaviour
{
    [SerializeField] private int startHpSerialize;
    private static int startHp = 0;

    public static int StartHP => startHp;

    private void Start()
    {
        startHp = startHpSerialize;
    }

    public void HpUp()
    {
        startHp++;
    }
}
