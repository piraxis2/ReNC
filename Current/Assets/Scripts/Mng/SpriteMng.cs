using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMng : Mng
{
    public static SpriteRenderer[] s_classimage = new SpriteRenderer[5];
    public static Sprite s_blanksprite;
    public static SpriteRenderer[] s_someicons;
    public static Sprite[,] s_perkicons = new Sprite[2, 9];
    public override void Init()
    {
        s_classimage = transform.Find("Class").GetComponentsInChildren<SpriteRenderer>();
        s_someicons = transform.Find("Icons").GetComponentsInChildren<SpriteRenderer>();
        s_blanksprite = s_someicons[1].sprite;
        SpriteRenderer[] sprites = transform.Find("Perks").GetComponentsInChildren<SpriteRenderer>();
        int idx = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s_perkicons[i, j] = sprites[idx].sprite;
                idx++;
            }
        }
    }
}
