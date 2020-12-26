using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    ItemInven m_inven;
    Item m_targetitem;
    private GameObject m_target;
    public int m_idx = 0;
    private Vector3 m_oripos;

    public void Init(int idx, ItemInven inven)
    {
        m_idx = idx;
        m_inven = inven;
        m_target = transform.GetChild(0).gameObject;
        m_oripos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_targetitem = m_inven.Inven[m_idx];
        m_oripos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_targetitem != null)
            m_target.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        BaseChar targetchar = HRay();

        if (targetchar == null)
        {
            Debug.Log(1);
            m_target.transform.position = m_oripos;
            return;
        }
        else
        {
            int num = -1;
            for(int i= 0; i<3; i++)
            {
                if (targetchar.Status.EquipMent[i] == null)
                { num = i; break; }
            }
            if (num >= 0)
            {
                targetchar.Status.ItemEquip(num, m_targetitem);
                m_inven.RemoveItem(m_idx);
                m_target.transform.position = m_oripos;
                m_targetitem = null;
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
