using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltFx : PixelFx
{

    private List<TrailRenderer> m_trailRenderers = new List<TrailRenderer>();
    private TrailRenderer[][] m_trails = new TrailRenderer[3][];
    private WaitForSeconds m_wait = new WaitForSeconds(0.2f);
    public bool m_initing = false;
    public float m_speed = 8;

    public override void Init()
    {

        if (m_initing)
            return;

        m_initing = true;
        m_trailRenderers = new List<TrailRenderer>();
        m_trailRenderers.AddRange(GetComponentsInChildren<TrailRenderer>(true));
        int idx = 0; 
        for (int i = 0; i < 3; i++)
        {
            m_trails[i] = new TrailRenderer[8];
            for (int j = 0; j < 8; j++)
            {
                m_trails[i][j] = m_trailRenderers[idx];
                idx++;
            }
        }
    }

    public void Test()
    {
        Init();
        BoltStart(NodeMng.instance.NodeArr[0, 0].transform, NodeMng.instance.NodeArr[1, 1].transform);
        
    }


    public void BoltStart(Transform start, Transform target)
    {

        Init();

        StartCoroutine(IETrail1(start, target, m_trails[0]));
        StartCoroutine(IETrail1(start, target, m_trails[1]));
        StartCoroutine(IETrail1(start, target, m_trails[2]));
    }


    IEnumerator IETrail1(Transform g1, Transform g2, TrailRenderer[] trailRenderers)
    {
        float elapsedtime = 0;
        bool stop = false;
  
        for (int i = 0; i < trailRenderers.Length; i++)
        {
            stop = false;
            Vector3 vec = new Vector3();
            elapsedtime = 0;
            Vector3 v1 = new Vector3();
            Vector3 v2 = new Vector3();
            while (!stop)
            {
                if (!g2.gameObject.activeInHierarchy)
                {
                    v1 = g1.position;
                }
                else if (!g1.gameObject.activeInHierarchy)
                {
                    v2 = g2.position;
                }
                else if (!g1.gameObject.activeInHierarchy&&!g2.gameObject.activeInHierarchy)
                {

                }
                else
                {
                    v1 = g1.position;
                    v2 = g2.position;
                }


                float x = Random.Range(-0.25f, 0.25f);
                vec = Vector3.Lerp(v1, v2, elapsedtime);


              

                if (elapsedtime<=0.1f)
                {
                    trailRenderers[i].transform.position = vec; 
                }
                else if (elapsedtime <= 0.9f)
                    trailRenderers[i].transform.position = new Vector3(vec.x, vec.y, vec.z + x);

                trailRenderers[i].gameObject.SetActive(true);

                elapsedtime += Time.deltaTime * m_speed;

                if (elapsedtime >= 1)
                {
                    trailRenderers[i].transform.position = vec;
                    elapsedtime = 0;
                    stop = true;
                }
                yield return null;

            }
            yield return null;
        }
        yield return m_wait;

        for (int i = 0; i < trailRenderers.Length; i++)
        {
            trailRenderers[i].transform.position = transform.position;
            trailRenderers[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
        yield return null;
    }
}
