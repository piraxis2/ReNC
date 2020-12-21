using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceState : Mng
{
    private Text m_goldtext;
    private Text m_cresttext;
    private Text m_relictext;

    public override void Init()
    {
        Text[] texts = GetComponentsInChildren<Text>(true);
        m_goldtext = texts[0];
        m_cresttext = texts[1];
        m_relictext = texts[2];
        RecallResource();
    }

    public void RecallResource()
    {
        m_goldtext.text = InGameResource.instance.GetResource(ResourceType.Gold).ToString();
        m_cresttext.text = InGameResource.instance.GetResource(ResourceType.Crest).ToString();
        m_relictext.text = InGameResource.instance.GetResource(ResourceType.relic).ToString();
    }

}
