using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwap : MonoBehaviour
{

    private Image oributton;
    public Sprite swapsprite;


    private void Start()
    {
        oributton = transform.GetComponent<Image>();
    }

    public void swap()
    {
        Sprite temp;

        temp = oributton.sprite;
        oributton.sprite = swapsprite;
        swapsprite = temp;
    }

}
