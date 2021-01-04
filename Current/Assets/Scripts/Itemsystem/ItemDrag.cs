using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    ItemInven m_inven;
    Item m_targetitem;
    private GameObject m_target;
    private int m_idx = 0;
    private Vector3 m_oripos;
    private Transform m_foreground;

    public void Init(int idx, ItemInven inven)
    {
        m_foreground = FindObjectOfType<CanvasMng>().transform;
        m_idx = idx;
        m_inven = inven;
        m_target = transform.GetChild(0).gameObject;
        m_oripos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_target.transform.SetParent(m_foreground);
        m_targetitem = m_inven.Inven[m_idx];
        m_oripos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
    
        if (m_targetitem != null)
        {
            m_target.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        m_target.transform.SetParent(transform);

        BaseChar targetchar = HRay();

        GameObject obj = eventData.pointerEnter;



        if (obj != null)
        {
            ItemDrag dragpoint = obj.GetComponent<ItemDrag>();
            if (m_inven.ADDItem(m_targetitem, dragpoint.m_idx))
            {
                m_inven.RemoveItem(m_idx);
                m_target.transform.position = m_oripos;
            }
            else
            {
                m_target.transform.position = m_oripos;
            }
            return;
        }


        if (targetchar == null)
        {
            m_targetitem = null;
            m_target.transform.position = m_oripos;
            return;
        }
        else
        {
            if (!targetchar.FOE)
            {
                m_targetitem = null;
                m_target.transform.position = m_oripos;
                return;
            }

            int num = -1;
            for (int i = 0; i < 3; i++)
            {
                if (targetchar.MyStatus.EquipMent[i] == null)
                { num = i; break; }

                if (i == 2)
                {
                    if (targetchar.MyStatus.EquipMent[i].m_quality == "Common")
                    {
                        if (m_targetitem.m_quality == "Common")
                        {
                            m_inven.RemoveItem(m_idx);
                            ItemMng.instance.EquipmentCheck(targetchar.MyStatus.m_Equipment, m_targetitem);
                            m_targetitem = null;
                            m_target.transform.position = m_oripos;
                            return;
                        }
                        else
                        {
                            m_targetitem = null;
                            m_target.transform.position = m_oripos;
                            return;
                        }
                    }
                }
            }

            if (num < 0)
            {
                m_targetitem = null;
                m_target.transform.position = m_oripos;
            }

            else if (num >= 0)
            {
                targetchar.MyStatus.ItemEquip(num, m_targetitem);
                m_inven.RemoveItem(m_idx);
                ItemMng.instance.EquipmentCheck(targetchar.MyStatus.m_Equipment);
                m_targetitem = null;
                m_target.transform.position = m_oripos;
            }

        }
    }

    private BaseChar HRay()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 10 | 1 << 9))
        {
            BaseChar se = hit.transform.GetComponent<BaseChar>();

            if (se != null)
            {
                return se;
            }
        }
        return null;
    }


}
