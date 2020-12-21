using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInit : MonoBehaviour
{
    Camera m_camera;
    Image[] m_logo = new Image[2];
    Text m_text;

    public static bool Load = false;

    public static void LoadTable()
    {
        TableMng.Instance.AddTable<ItemTable>(TableType.ITEMTable);
        TableMng.Instance.AddTable<ShopingTable>(TableType.ShopTable);
        TableMng.Instance.AddTable<PERKTable>(TableType.PERKTable);
        TableMng.Instance.AddTable<PassiveTable>(TableType.PassiveTable);
        TableMng.Instance.AddTable<SkillInfoTable>(TableType.SkillInfoTable);
        TableMng.Instance.AddTable<DungeonTable>(TableType.DungeonTable);
        TableMng.Instance.AddTable<PlayerSkillTable>(TableType.PlayerSkillTable);
        TableMng.Instance.AddTable<BuildTable>(TableType.BuildTable);
    }

    public static void RegisterScene()
    {
        SceneMng.Instance.AddScene<Title>(SceneType.Title);
        SceneMng.Instance.AddScene<Lobby>(SceneType.Lobby);
        SceneMng.Instance.AddScene<Ingame>(SceneType.Ingame);
    }

    void GameSetting()
    {

    }

    void NextScene()
    {
        SceneMng.Instance.Enable(SceneType.Title);
    }

    void FadeOut()
    {
        LoadingMng.Instance.Fade(false, NextScene, 1);
    }

    void Logo()
    {

        m_camera = GetComponent<Camera>();
        m_logo = GetComponentsInChildren<Image>();
        m_text = GetComponentInChildren<Text>();
        StartCoroutine(LogoColor());

    }
   
  

    IEnumerator LogoColor()
    {

        float elapsedtime = 0;
        float speed = 1;
        Color targetcolor = new Color(0.5f, 0.3f, 0.3f);
        Color oricolor = m_camera.backgroundColor;
        elapsedtime = 0;

        while (elapsedtime<1)
        {
            elapsedtime += Time.deltaTime * speed;
            m_logo[0].color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, elapsedtime);
            m_text.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, elapsedtime);

            yield return null;
        }
        elapsedtime = 0;

        while (elapsedtime<1)
        {
            elapsedtime += Time.deltaTime * speed;
            m_camera.backgroundColor = Color.Lerp(oricolor, targetcolor, elapsedtime);
            yield return null;
        }

        yield return null;

    }


   
    void Awake()
    {

        Logo();
        LoadTable();
        RegisterScene();
        GameSetting();
        Load = true;
        Invoke("FadeOut", 6);
    }

}
