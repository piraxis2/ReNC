using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    GameObject m_target;
    Vector3 m_original;
    // Start is called before the first frame update
    public void Init()
    {
        m_original = transform.position;
        m_target = transform.GetChild(3).gameObject;
    }



    public void Move(bool open)
    {
        StartCoroutine(IEmove(open));
    }

    IEnumerator IEmove(bool open)
    {
        float elapsedtime = 0;
        if (!open)
        {
            while (elapsedtime <= 1)
            {
                elapsedtime += Time.deltaTime * 5;
                transform.position = Vector3.Lerp(m_original, m_target.transform.localPosition, elapsedtime);
                yield return null;
            }
        }
        else
        {
            while (elapsedtime <= 1)
            {
                elapsedtime += Time.deltaTime * 5;
                transform.position = Vector3.Lerp(m_target.transform.localPosition,m_original, elapsedtime);
                yield return null;
            }
        }
        yield return null;

    }
}
