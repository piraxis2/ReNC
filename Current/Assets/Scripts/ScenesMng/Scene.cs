using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene : MonoBehaviour
{
    private Dictionary<Channel, SceneType> m_channel =
        new Dictionary<Channel, SceneType>();


    protected void AddChannel(Channel c, SceneType sType)
    {
        if (!m_channel.ContainsKey(c))
            m_channel.Add(c, sType);
    }

    public SceneType GetScene(Channel c)
    {
        if (m_channel.ContainsKey(c))
            return m_channel[c];


        Debug.Log(1);
        return SceneType.None;
    }

    

    public virtual void Init()
    {

    }

    public void FalseLoading(SceneType sT,
                            float targetTime = 2.0f,
                            System.Action<float> func = null)
    {
        StartCoroutine(IEFalseAsync(sT, targetTime, func));
    }
    // 실시간으로 신 파일을 읽어 들여서 처리 할 때 사용할 함수입니다.
    private IEnumerator IEFalseAsync(SceneType sc,
                                     float targetTime,
                                     System.Action<float> func = null)
    {
        AsyncOperation operation =
            SceneManager.LoadSceneAsync(sc.ToString());

        bool state = false;

        float elapsedTime = 0;

        while (!state)
        {
            elapsedTime += Time.deltaTime / targetTime;

            elapsedTime = Mathf.Clamp01(elapsedTime);

            if (elapsedTime >= 1.0f)
            {
                state = true;
                Enter();
            }


            func(elapsedTime);

            yield return null;
        }
        yield return null;
    }

    // 실시간으로 신 파일을 읽어 들여서 처리 할 때 사용할 함수입니다.
    private IEnumerator IELoadAsync(SceneType sc, System.Action<float> func = null)
    {
        AsyncOperation operation =
            SceneManager.LoadSceneAsync(sc.ToString());

        bool state = false;

        while (!state)
        {
            if (func != null)
                func(operation.progress);
            if (operation.isDone)
            {
                state = true;
                Enter();
            }


            yield return null;
        }

        yield return null;
    }

    public void LoadAsync(SceneType sT, System.Action<float> func = null)
    {
        // 연결된 함수가 있다면 연결된 함수가 호출되도록 하고,
        // 연결된 함수가 없다면 신 자신의 Progress함수를 연결하도록 합니다.
        StartCoroutine(IELoadAsync(sT, func));
    }

    public void Load(SceneType sT)
    {
        SceneManager.LoadScene(sT.ToString());
    }

    public void Enter(SceneType sT,
                       bool falseLoading = false,
                       float targetTime = 2.0f)
    {
        if (!falseLoading)
            LoadAsync(sT, Progress);
        else
            FalseLoading(sT, targetTime, Progress);
    }

    // 해당 시점에 로드가 완료된 신에서 호출될 함수입니다.
    public virtual void Enter()
    {

    }

    // 이전 신( 컴포넌트 ) 에서 호출될 함수입니다.
    public virtual void Exit()
    {

    }

    public virtual void Progress(float delta)
    {

    }

}
