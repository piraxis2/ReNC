using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : Scene
{

    public override void Init()
    {
        AddChannel(Channel.C1, SceneType.Title);
        AddChannel(Channel.C2, SceneType.Ingame);
    }

    // 해당 시점에 로드가 완료된 신에서 호출될 함수입니다.
    public override void Enter()
    {
        LoadingMng.Instance.Fade(true);
    }

    public override void Progress(float delta)
    {

        if (delta < 1)
        {
            LoadingMng.Instance.Loading(true);
            return;
        }

        LoadingMng.Instance.Loading(false);
    }
    public override void Exit()
    {

    }

}
