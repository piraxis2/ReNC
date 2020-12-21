using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBox : MonoBehaviour
{

    public virtual void Init()
    {

    }

    public virtual void Setbox(Vector3 pos)
    {
        gameObject.SetActive(true);
        transform.position = pos + new Vector3(0, 0, 0);
    }

}
