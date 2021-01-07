using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{

    private static MouseOver m_mouseover;
    public static MouseOver Instance
    {
        get
        {
            if (m_mouseover == null)
                m_mouseover = FindObjectOfType<MouseOver>();

            return m_mouseover;
        }
    }





    private GameObject m_obj;
    private PopupBox m_box;

    public void MouseOverON(GameObject obj,PopupBox box)
    {
        m_box = box;
        m_obj = obj;
        box.Init();
        StartCoroutine(IEMouseOver(m_obj, m_box));
    }
    public void MouseOverOff()
    {
        StopCoroutine(IEMouseOver(m_obj, m_box));
    }


    private IEnumerator IEMouseOver(GameObject obj, PopupBox box)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();

        while (true)
        {
            bool boxoff = false;




            if (CalculateBounds(rt, rt.lossyScale.x).Contains(Input.mousePosition))
            {

                box.Setbox(Input.mousePosition);
                boxoff = true;
            }


            if (!boxoff)
            {
                box.gameObject.SetActive(false);
            }


            yield return null;
        }
    }

    private Bounds CalculateBounds(RectTransform transform, float uiScaleFactor)
    {
        Bounds bounds = new Bounds(transform.position, new Vector3(transform.rect.width, transform.rect.height, 0.0f) * uiScaleFactor);

        if (transform.childCount > 0)
        {
            foreach (RectTransform child in transform)
            {
                Bounds childBounds = new Bounds(child.position, new Vector3(child.rect.width, child.rect.height, 0.0f) * uiScaleFactor);
                bounds.Encapsulate(childBounds);
            }
        }

        return bounds;
    }

}
