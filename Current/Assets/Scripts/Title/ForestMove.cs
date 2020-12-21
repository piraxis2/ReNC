using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMove : MonoBehaviour
{

    private BackGroundMove[] backGroundMoves;
    private Material[] materials = new Material[2];
    public float m_speed = 1;
    public float m_elapsedtime = 0;
    public bool m_start = false;

    public bool m_swap = false;
    private float[] m_scrollSpeed = new float[9];
    private Renderer[] m_materials = new Renderer[9];
    private Vector2[] m_newOffset = new Vector2[9];

    private void Awake()
    {
        backGroundMoves = GetComponentsInChildren<BackGroundMove>();
        float speed = 0;
        foreach(var x in backGroundMoves)
        {
            speed += 0.01f;
            x.m_scrollSpeed = speed; 
        }
        materials[0] = transform.Find("Light").GetComponent<Renderer>().material;
        materials[1] = transform.Find("Light2").GetComponent<Renderer>().material;

        StartCoroutine(IELightblink());
        init();
    }

    IEnumerator IELightblink()
    {
        Color[] targetcolor = new Color[2];
        Color[] oricolor = new Color[2];
        for(int i =0; i<2;i++)
        {
            targetcolor[i] = new Color(materials[i].GetColor("_TintColor").r, materials[i].GetColor("_TintColor").g, materials[i].GetColor("_TintColor").b, 0);
            oricolor[i] = materials[i].GetColor("_TintColor");
        }


        while (m_start)
        {
            m_elapsedtime += Time.deltaTime * m_speed;
            for (int i = 0; i < 2; i++)
            {
                materials[i].SetColor("_TintColor", Color.Lerp(oricolor[i], targetcolor[i], m_elapsedtime));
            }

            if (m_elapsedtime>1)
            {
                Color[] temp = targetcolor;
                targetcolor = oricolor;
                oricolor = temp;
                m_elapsedtime = 0;
            }
            yield return null;
        }


    }


 
    public void init()
    {
        m_materials = transform.Find("bg").GetComponentsInChildren<Renderer>(true);

        float time = 0.0022f;
        for (int i= 0;i<9;i++)
        {
            
         

            m_newOffset[i] = m_materials[i].material.mainTextureOffset;
        
            m_scrollSpeed[i] = time;
            time += 0.0022f;
        }

    }

    private void Update()
    {

        for (int i = 0; i < 9; i++)
        {
            m_newOffset[i].Set(m_newOffset[i].x+(m_scrollSpeed[i] * Time.deltaTime), 0);
            m_materials[i].material.mainTextureOffset = m_newOffset[i];
        }
    }


}
