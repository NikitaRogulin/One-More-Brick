using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestA t = new TestA();
        TestA.i.Add(1);
        t = new TestA();
        Debug.Log(TestA.i.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class TestA
{
    public static List<int> i = new List<int>();
}