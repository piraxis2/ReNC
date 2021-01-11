using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltFx : MonoBehaviour
{
    public Transform g1;
    public Transform g2;

     Vector3 v1 = new Vector3();
    Vector3 v2 = new Vector3();
    public TrailRenderer trail = new TrailRenderer();
    public TrailRenderer trail2 = new TrailRenderer();
    public TrailRenderer trail3 = new TrailRenderer();
    public TrailRenderer trail4 = new TrailRenderer();
    public TrailRenderer trail5 = new TrailRenderer();
    public TrailRenderer trail6 = new TrailRenderer();
    Vector3 temp;

    List<TrailRenderer> trailRenderers = new List<TrailRenderer>();

    void Start()
    {
        trailRenderers.Add(trail);
        trailRenderers.Add(trail2);
        trailRenderers.Add(trail3);
        trailRenderers.Add(trail4);
        trailRenderers.Add(trail5);
        trailRenderers.Add(trail6);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            StartCoroutine(IETrail());
        }

       

    }

    IEnumerator IETrail()
    {
        float elapsedtime = 0;
        bool stop = false;
  
        for (int i = 0; i < trailRenderers.Count; i++)
        {
            trailRenderers[i].gameObject.SetActive(true);
            stop = false;
            Vector3 vec = new Vector3();
            elapsedtime = 0;
            while (!stop)
            {

                float x = Random.Range(-2, 2);
                vec = Vector3.Lerp(g1.position, g2.position, elapsedtime);

                if (elapsedtime<=0.1f)
                {
                    trailRenderers[i].transform.position = vec; 
                }
                else if (elapsedtime <= 0.9f)
                    trailRenderers[i].transform.position = new Vector3(vec.x, vec.y, vec.z + x);


                elapsedtime += Time.deltaTime * 8;

                if (elapsedtime >= 1)
                {
                    trailRenderers[i].transform.position = vec;
                    elapsedtime = 0;
                    stop = true;
                }
                yield return null;

            }
            
        }

        yield return null;
    }
}
