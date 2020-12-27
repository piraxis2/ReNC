using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class T
{

}



public class Tail : MonoBehaviour
{
    private float m_speed = 1.5f;
    private TrailRenderer m_trailRenderer;
   

    public void Init()
    {
        m_trailRenderer = GetComponent<TrailRenderer>();
    }
    public void SetTail(Vector3 target, bool inout)
    {

        List<Vector3> curve = new List<Vector3>(4);
        curve.Add(transform.parent.position);
        curve.Add(transform.parent.position + new Vector3(-3, 10, 0));
        curve.Add(target + new Vector3(-3, 10, 0));
        curve.Add(target + new Vector3(0, 1, 0));
        if (inout)
            StartCoroutine(IETrail(curve,inout));
        else
        {
            curve.Reverse();
            StartCoroutine(IETrail(curve, inout));
        }

    }

    public void SetManaTail(Vector3 start, Vector3 target)
    {
        List<Vector3> curve = new List<Vector3>();
        curve.Add(start);
        curve.Add(start + new Vector3(0,3, 0));
        curve.Add(curve[1] + new Vector3(0, 3, 0));
        curve.Add(target + new Vector3(0, 6, 0));
        curve.Add(target+new Vector3(0,0.0f,0));

        StartCoroutine(IEManaTail(curve, SummonerSkillMng.Instance.GetMana));
    }


    public void SetTail(Vector3 start, Vector3 target, System.Action action = null)
    {
        List<Vector3> curve = new List<Vector3>();
        curve.Add(start);
        curve.Add(start + new Vector3(0, 3, 0));
        curve.Add(curve[1] + new Vector3(0, 3, 0));
        curve.Add(target + new Vector3(0, 6, 0));
        curve.Add(target + new Vector3(0, 0.0f, 0));

        StartCoroutine(IEManaTail(curve, action));

    }




    private IEnumerator IETrail(List<Vector3> curve , bool inout)
    {

        PixelFx fx;


        bool stop = false;
        float elapsedtime = 0;
        if (!inout)
        {
            fx = FxMng.Instance.FxCall("Summon");
            CharMng.Instance.InvisibleCharters(0);
            fx.transform.position = curve[0] + new Vector3(0, -1, 0);
            fx.gameObject.SetActive(true);
        }

        while (!stop)
        {
            elapsedtime += Time.deltaTime*m_speed;

            transform.position = MathHelper.BezierCurve(curve[0], curve[1], curve[2], curve[3], elapsedtime);

            if(elapsedtime>=1)
            {
                stop = true;
            }
            yield return null;
        }



        if (inout)
        {
            fx = FxMng.Instance.FxCall("Summon");
            fx.transform.position = curve[3] + new Vector3(0, -1, 0);
            fx.gameObject.SetActive(true);
            CharMng.Instance.InvisibleCharters(1);
        }

        Invoke("ShutActive", 1);


        yield return null;

    }


    public IEnumerator IEManaTail(List<Vector3> curve, System.Action action = null)
    {
        bool stop = false;
        float elapsedtime = 0;

        gameObject.SetActive(true);
        transform.position = curve[0];




        while (!stop)
        {
            elapsedtime += Time.deltaTime * 3;
            transform.position = Vector3.Lerp(curve[0], curve[1], elapsedtime);
            if(elapsedtime>=1)
            {
                stop = true;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);

        elapsedtime = 0;
        stop = false;

        while (!stop)
        {
            elapsedtime += Time.deltaTime * 2;

            transform.position = MathHelper.BezierCurve(curve[1], curve[2], curve[3], curve[4], elapsedtime);

            if (elapsedtime >= 1)
            {
                stop = true;
            }
            yield return null;
        }

        if (action != null)
            action();

        Invoke("ShutActive", 1);



        yield return null;


    }

    private void ShutActive()
    {
        gameObject.SetActive(false);
        transform.position = transform.parent.position;
    }




}
