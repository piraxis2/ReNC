using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFx : MonoBehaviour
{


    public void AimStart(Vector3 pos)
    {
        List<Vector3> curve = new List<Vector3>();
        curve.Add(transform.parent.position);
        curve.Add(curve[0] + new Vector3(0, 30, 0));
        curve.Add(curve[1] + new Vector3(0, 30, 0));
        curve.Add(pos + new Vector3(0, 60, 0));
        curve.Add(pos + new Vector3(0, 0.0f, 0));
        StartCoroutine(IEAim(curve));
    }

    private IEnumerator IEAim(List<Vector3> pos)
    {
        float time = 0;
        bool stop = false;

        while (!stop)
        {
            time += Time.deltaTime;
            transform.position = MathHelper.BezierCurve(pos[0], pos[1], pos[2], pos[3], time);
            if(time>=1)
            {
                transform.position = transform.parent.position;
                stop = true;
            }
            yield return null;
        }
    }

    private void OnGUI()
    {

        if(GUI.Button(new Rect(0,0,100,100),"s"))
        {
            AimStart(new Vector3(0, 0, 0));
        }
    }
}
