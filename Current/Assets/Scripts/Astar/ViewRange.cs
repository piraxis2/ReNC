using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRange : MonoBehaviour
{
    PathFind m_pathfind;
    NodeMng m_nodemng;
    BaseChar m_ray;
    BaseChar m_prevray;

    Color m_originColor = new Color();
    // Start is called before the first frame update
    void Start()
    {
        m_nodemng = GameObject.FindObjectOfType<NodeMng>();
        m_pathfind = GameObject.FindObjectOfType<PathFind>();
        m_originColor = NodeMng.instance.NodeArr[0,0].m_sprite.color;

    }
    // Update is called once per frame
    void Update()
    {
        m_prevray = m_ray;
        m_ray = Ray();


        if (m_ray != null)
        {
            if (m_ray.name == "Dummy")
                return;

            foreach (var x in m_ray.RangeList)
            {
                x.m_sprite.color =  new Color(0.7f,1, 0.8f);
            }
            CharacterInfoBox.Instance.SetBox(m_ray.MyStatus, Input.mousePosition);

        }

        if (m_ray == m_prevray)
            return;

        if (m_prevray != null)
        {
            foreach (var x in m_prevray.RangeList)
            {
                x.m_sprite.color = x.OriColor;
            }
            CharacterInfoBox.Instance.OffBox();
        }
    }

    BaseChar Ray()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;  
        if (Physics.Raycast(ray,out hit))
        {
            BaseChar se= hit.transform.GetComponent<BaseChar>();

            if (se != null)
            {
                return se;
            }
        }

        return null;
    }
}
