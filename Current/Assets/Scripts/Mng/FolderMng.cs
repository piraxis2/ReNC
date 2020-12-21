using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FolderMng : Mng
{

    public Vector3 m_target = new Vector3();
    private UIFolder[] m_folder = new UIFolder[3];

    public override void Init()
    {
        m_target = transform.GetChild(0).transform.localPosition;
        m_folder = GetComponentsInChildren<UIFolder>();
        foreach(var x in m_folder)
        {
            x.Init();
        }
    }

    public void FoldRest()
    {
        foreach(var x in m_folder)
        {
            if(x.IsOpen)
            {
                x.MoveFolder();
            }
        }
    }

//모바일용으로 변경
    void Update()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                FoldRest();
            }
        }



    }

  
}
