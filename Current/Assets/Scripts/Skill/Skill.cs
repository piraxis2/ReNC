using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Node m_target;
    protected BaseChar m_caster;
    protected GameObject m_effect;
    protected int[] m_damage = new int[3];
    public string m_name;

    protected FxMng m_skillmng;


    public virtual void Init(FxMng fx)
    {
        for(int i= 0; i<3; i++)
        {
            m_damage[i] = 0;
        }
        m_skillmng = fx;
    }

    public virtual List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        return null;
    }

    public virtual void Skillshot(BaseChar caster, Node target)
    {
        List<Node> skillrange = SkillRange(NodeMng.instance.NodeArr, target, caster);
        StartCoroutine(IESkillaction(skillrange, caster));

        
    }

    public virtual IEnumerator IESkillaction(List<Node> skillrange,BaseChar caster)
    {
        yield return null;
    }

}
