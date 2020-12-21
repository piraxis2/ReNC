using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    None, Title, Lobby, Ingame
}

public enum Channel
{
    C1,
    C2
}


public class SceneMng : MonoBehaviour
{
    private static SceneMng s_scenes;

    private Dictionary<SceneType, Scene> m_sceneDic = new Dictionary<SceneType, Scene>();

    private SceneType m_currScene = SceneType.None;


    public static SceneMng Instance
    {
        get
        {
            if (s_scenes == null)
            {
                GameObject obj = new GameObject(typeof(SceneMng).Name, typeof(SceneMng));
                s_scenes = obj.GetComponent<SceneMng>();
                s_scenes.Init();
                DontDestroyOnLoad(s_scenes.gameObject);
            }
            return s_scenes;
        }


    }

    private void Init()
    {

    }


    public SceneType CurrScene
    {
        get { return m_currScene;  }
    }
    

    public void ScriptEnable(SceneType scene)
    {
        foreach (var pair in m_sceneDic)
        {
            if (pair.Key != scene)
                pair.Value.enabled = false;
            else
                pair.Value.enabled = true;
        }
    }

    public void Enable(SceneType scene, bool falseLoading = false, float targetTime = 2.0f)
    {
        if (m_sceneDic.ContainsKey(m_currScene))
            m_sceneDic[m_currScene].Exit();

        if (m_sceneDic.ContainsKey(scene))
        {
            // 변경된 스크립트만을 활성화 합니다.
            ScriptEnable(scene);

            m_currScene = scene;
            // 비동기로 신을 읽어들일 수 있도록 합니다.
            m_sceneDic[m_currScene].Enter(scene, falseLoading, targetTime);
        }
    }

    public void Event(Channel channel, bool falseLoading = false, float targetTime = 2.0f)
    {
        if (m_sceneDic.ContainsKey(m_currScene))
        {
            SceneType sceneType = m_sceneDic[m_currScene].GetScene(channel);

            Enable(sceneType, falseLoading, targetTime);
        }

    }

    public T AddScene<T>(SceneType sType, bool state = false) where T : Scene
    {
        if (!m_sceneDic.ContainsKey(sType))
        {

            GameObject obj = new GameObject(typeof(T).Name, typeof(T));
            T t = obj.GetComponent<T>();
            t.enabled = state;
            m_sceneDic.Add(sType, t);
            DontDestroyOnLoad(t.gameObject);
            m_sceneDic[sType].Init();
            return t;
        }

        return null;
    }

    public void Release()
    {
        Destroy(gameObject);
        s_scenes = null;
    }


    public string GetCurrSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

}
