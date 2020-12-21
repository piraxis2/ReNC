using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{

    public bool m_swap = false;
    public float m_scrollSpeed = 1f;
    //스크롤할 속도를 상수로 지정해 줍니다.
    private Material m_thisMaterial;
    private Vector2 m_newOffset;
    //Quad의 Material 데이터를 받아올 객체를 선언합니다.
    public void Awake()
    {
        m_thisMaterial = GetComponent<Renderer>().material;
        m_newOffset = m_thisMaterial.mainTextureOffset;
        StartCoroutine(IEUpdate());
    }


    IEnumerator IEUpdate()
    {
        while (true)
        {
            // 새롭게 지정해줄 OffSet 객체를 선언합니다.
            m_newOffset.Set(m_newOffset.x + (m_scrollSpeed * Time.deltaTime), 0);
            // Y부분에 현재 y값에 속도에 프레임 보정을 해서 더해줍니다.
            m_thisMaterial.mainTextureOffset = m_newOffset;
            //그리고 최종적으로 Offset값을 지정해줍니다.
            yield return null;

        }
        
    }

     

}
