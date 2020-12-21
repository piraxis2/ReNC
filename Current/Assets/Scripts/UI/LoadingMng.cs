using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMng : MonoBehaviour
{
    private static LoadingMng s_fadeInOut;
    Image m_fade;
    GameObject m_loadingwindow;
    Circlemove m_circlemove;
    public int m_count = 0;

    public static LoadingMng Instance
    {

        get
        {
            if(s_fadeInOut==null)
            {
                GameObject obj = new GameObject(typeof(LoadingMng).Name, typeof(LoadingMng));
                s_fadeInOut = obj.GetComponent<LoadingMng>();
                s_fadeInOut.Init();
            }
            return s_fadeInOut;
        }
    }

    public void Init()
    {
        GameObject canvas = new GameObject(typeof(Canvas).Name, typeof(Canvas));
        CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        Canvas moduler = canvas.GetComponent<Canvas>();
        moduler.renderMode = RenderMode.ScreenSpaceCamera;
        moduler.sortingOrder = 100;

        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        s_fadeInOut.gameObject.transform.SetParent(canvas.transform);
        s_fadeInOut.gameObject.AddComponent<Image>().raycastTarget = false;
        s_fadeInOut.gameObject.GetComponent<RectTransform>().localScale = new Vector3(100, 100);
        m_fade = GetComponent<Image>();
        m_fade.color = Color.black;

        m_loadingwindow = Instantiate(Resources.Load("Prefab/UI/Loading") as GameObject, canvas.transform);
        m_loadingwindow.gameObject.SetActive(false);

        DontDestroyOnLoad(canvas);
    }

    public void Fade(bool inout, System.Action func = null, int count = 1)
    {
        StartCoroutine(IEFade(inout, func, count));
    }


    public void Loading(bool onoff)
    {
        m_loadingwindow.gameObject.SetActive(onoff);
    }

    public IEnumerator IEFade(bool inout, System.Action action = null, int count = 1)
    {

        s_fadeInOut.gameObject.transform.SetAsLastSibling();
        float elapsedtiem = 0;
        int alp = 0;
        m_count = count;
        if(inout)
        {
            alp = 1;
        }
        else
        {
            alp = 0;
        }

        bool update = true;

        while(update)
        {
            elapsedtiem += Time.deltaTime;
            m_fade.color = Color.Lerp(new Color(0, 0, 0, alp),new Color(0,0,0,Mathf.Abs(alp-1)) ,elapsedtiem);
            if(elapsedtiem>=1)
            {
                update = false;
                elapsedtiem = 0;
            }
            yield return null;
        }

        if (action != null)
            action();

        if (m_count == 0)
        {
            m_count++;
            StartCoroutine(IEFade(!inout));
        }
        else { m_count = 0; }

        yield return null;

    }


}
