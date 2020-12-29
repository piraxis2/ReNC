using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testclass : MonoBehaviour
{
    public List<string> strings = new List<string>();
    public Item[] items = new Item[3];



    // Start is called before the first frame update
    void Start()
    {

        for(int i= 0; i<10; i++)
        {
            strings.Add((i + 1).ToString());
        }

        var x = new HashSet<string>();
        for (int i = 3; i < 7; i++)
        {
            x.Add(i.ToString());
        }

        strings.RemoveAll(x.Contains);

        foreach(var y in strings)
        {
            Debug.Log(y);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
