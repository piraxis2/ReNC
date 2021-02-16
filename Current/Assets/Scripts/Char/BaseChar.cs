using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassType
{
    Hunter, Knight, Mage, Warrior, Lord
}

public enum Attacktype
{
    Direct, Howitzer, Invisible, Wide
}


public class BaseChar : MonoBehaviour
{


    //private varialbe
    #region

[SerializeField]
    private bool m_foe = false;

    private int m_uniqueID;
    private int m_factoryID;
    private int m_handID;
    private int m_cardID;
    private int m_star = 1;
    private int m_tier;
    private int m_idx;

    protected int m_count;

    protected bool m_initbool = false;

    protected bool m_isonline = false;

    protected bool m_isonfield = false;

    protected bool m_exclude = false;

    protected Node m_currnode;
    protected Node m_prevnode;
    protected Node m_standbynode;

    protected Node m_tecticsnode;

    private MainMng m_mainmng;
    protected NodeMng m_nodemng;

    private BaseChar m_fusionparent;



    private bool m_running = false;
    private bool m_attacking = false;
    private bool m_dying = false;




    public bool m_rapidshot = false;



    public List<Node> m_Rangelist;
    protected List<Node> m_skillRangelist;
    protected List<BaseChar> m_attackablelist;




    public Animator m_animator;
    public Animator m_starani;
    protected Status m_status;

    protected GameObject m_projectile;
    protected GameObject m_hitfx;
    protected FxMng m_fxmng;
    protected SpriteRenderer m_sprite;
    protected Sprite m_face;

    public int m_projectileangle;
    private int m_targetlayer;

    protected string m_classname;
    protected ClassType m_classtype;
    protected Skillname m_skill;
    protected Attacktype m_attacktype;
    #endregion
    //1 public property
    #region


    public bool FOE
    {
        get { return m_foe; }
    }

    public int UniqueID
    {
        get { return m_uniqueID; }
    }

    public int FactoryID
    {
        get { return m_factoryID; }
    }

    public int CardID
    {
        get { return m_cardID; }
    }

    public int HandID
    {
        get { return m_handID; }
    }

    public int Star
    {
        get { return m_star; }
    }

    public int Tier
    {
        get { return m_tier; }
    }

    public int IDX
    {
        get { return m_idx; }
    }

    public ClassType ClassType
    {
        get { return m_classtype; }
    }

    public Skillname Skill
    {
        get { return m_skill; }
    }

    public Attacktype ProjectileType
    {
        get { return m_attacktype; }
    }


    public string Classname
    {
        get { return m_classname; }
    }

    public int TargetLayer
    {
        get { return m_targetlayer; }
    }

    public NodeMng NodeMng
    {
        get { return m_nodemng; }
    }

    public Node CurrNode
    {
        get { return m_currnode; }
    }
    public Node TecticsNode
    {
        get { return m_tecticsnode; }
    }
    
    public Status MyStatus
    {
        get { return m_status; }
    }

    public List<BaseChar> AttackableList
    {
        get { return m_attackablelist; }
    }

    public List<Node> RangeList
    {
        get { return m_Rangelist; }
    }

    public List<Node> SkillRangeList
    {
        get { return m_skillRangelist; }
    }


    public bool IsOnline
    {
        get { return m_isonline; }
    }

    public bool IsOnField
    {
        get { return m_isonfield; }
    }

    public bool IsExclude
    {
        get { return m_exclude; }
    }

    public bool Running
    {
        get { return m_running; }
    }

    public bool Attacking
    {
        get { return m_attacking; }
    }

    public bool Dying
    {
        get { return m_dying; }
    }

    public GameObject Projectile
    {
        get { return m_projectile; }
    }

    public GameObject Hitfx
    {
        get
        {
            return m_hitfx;
        }
    }

    public SpriteRenderer CharSprite
    {
        get { return m_sprite; }
    }

    public Sprite Face
    {
        get { return m_face; }
    }

    public BaseChar FusionParent
    {
        get { return m_fusionparent; }
    }


    #endregion //  /
    //2 public set method
    #region

    public void SetFoe(bool foe)
    {
        m_foe = foe;
    }

    public void SetUniqueID(int val)
    {
        m_uniqueID = val;
    }
    public void SetFactoryID(int val)
    {
        m_factoryID = val;
    }

    public void SetCardID(int val)
    {
        m_cardID = val;
    }

    public void SetHandID(int val)
    {
        m_handID = val;
    }
    public void SetTier(int val)
    {
        m_tier = val;
    }

    public void SetIDX(int val)
    {
        m_idx = val;
    }

    public void SetCount(int val)
    {
        m_count = val;
    }

    public void SetTecticsNode(Node node)
    {
        m_tecticsnode = node;
    }

    public void SetTargetLayer(int val)
    {
        m_targetlayer = val;
    }

    public void SetRunning(bool val)
    {
        m_running = val;
    }

    public void SetDying(bool val)
    {
        m_dying = val;
    }

    public void SetAttacking(bool val)
    {
        m_attacking = val;
    }

    public void RapidOnOff(bool val)
    {
        m_rapidshot = val;
    }

    public void ReSetExclud()
    {
        m_exclude = false;
    }

    public void SetFusionParent(BaseChar mom)
    {
        m_fusionparent = mom;
    }

    #endregion

    protected virtual void StatusSet()
    {
        m_status.baseset(m_status.AD, m_status.MaxLife);
        m_status.Init();
    }

    public virtual void Init()
    {
        if (m_status == null)
        {
            m_status = new Status(this);
            StatusSet();
        }

        RayforNode();
        m_prevnode = m_currnode;
        if (m_currnode != null)
            transform.position = m_currnode.transform.position;
        m_mainmng = transform.root.GetComponent<MainMng>();
        m_fxmng = transform.root.GetComponentInChildren<FxMng>();
        m_nodemng = GameObject.Find("NodeMng").GetComponent<NodeMng>();
        m_animator = GetComponent<Animator>();
        m_starani = transform.Find("Spinstar").GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();

        if (m_sprite != null)
            m_face = m_sprite.sprite;


        MyStatus.PassiveCalculation();
        MyStatus.StatReLoad();
        RangeSet();
        GetComponent<DragHelper>().Init();

    }

    public virtual void ReLoadChar()
    {
        MyStatus.PassiveCalculation();
        MyStatus.StatReLoad();
        RangeSet();
    }
    
    public void SetColor(Color color)
    {
        m_sprite.color = color;

        m_face = m_sprite.sprite;
    }

    public void Fusion(bool IsFusion)
    {
        if (IsFusion)
        {
            m_star++;
            m_starani.SetInteger("lv", m_star);
            MyStatus.Upgrade();
        }

        else
        {
            m_star = 1;
            m_starani.SetInteger("lv", m_star);
            m_status = new Status(this);
            StatusSet();

        }
    }

    public void RangeClear()
    {
        m_Rangelist.Clear();
    }

    public virtual void RangeSet()
    {

        RangeClear();

        if (m_currnode == null)
            return;

        if (m_status.Range == 1)
        {
            for (int row = -m_status.Range; row <= m_status.Range; row++)
            {
                for (int col = -m_status.Range; col <= m_status.Range; col++)
                {
                    if (!(CurrNode.Row + row > 7 || CurrNode.Row + row < 0 || CurrNode.Col + col > 7 || CurrNode.Col + col < 0))
                    {
                        m_Rangelist.Add(NodeMng.instance.NodeArr[CurrNode.Row + row, CurrNode.Col + col]);
                    }
                }
            }
            return;
        }

        for (int row = -m_status.Range; row <= m_status.Range; row++)
        {
            for (int col = -m_status.Range; col <= m_status.Range; col++)
            {
                if (Mathf.Abs(col) <= Mathf.Abs(Mathf.Abs(row) - m_status.Range))
                {
                    if (!(CurrNode.Row + row > 7 || CurrNode.Row + row < 0 || CurrNode.Col + col > 7 || CurrNode.Col + col < 0))
                    {
                        m_Rangelist.Add(NodeMng.instance.NodeArr[CurrNode.Row + row, CurrNode.Col + col]);
                    }
                }
            }
        }
    }

    public virtual List<BaseChar> RangeCall()
    {


        List<BaseChar> attackable = new List<BaseChar>();

        foreach (var x in m_Rangelist)
        {
            if (x.CurrCHAR != null)
            {
                if (Dying)
                    continue;



                if (FOE)
                {
                    if (!x.CurrCHAR.FOE)
                    {
                        if (!x.CurrCHAR.Dying)
                            attackable.Add(x.CurrCHAR);
                    }
                }
                else
                {
                    if (x.CurrCHAR.FOE)
                    {
                        if (!x.CurrCHAR.Dying)
                            attackable.Add(x.CurrCHAR);
                        attackable.Add(x.CurrCHAR);
                    }
                }
                //if (x.CurrCHAR.gameObject.layer == 9)
                //{
                //    attackable.Add(x.CurrCHAR);
                //}
            }
        }

        return attackable;

    }


    public virtual List<BaseChar> FoeRangeCall(int inrange = 0)
    {
        int range = inrange;

        List<BaseChar> attackable = new List<BaseChar>();
        List<Node> ragelist = new List<Node>();


        for (int row = -range; row <= range; row++)
        {
            for (int col = -range; col <= range; col++)
            {
                if (Mathf.Abs(col) <= Mathf.Abs(Mathf.Abs(row) - range))
                {
                    if (!(CurrNode.Row + row > 7 || CurrNode.Row + row < 0 || CurrNode.Col + col > 7 || CurrNode.Col + col < 0))
                    {
                        ragelist.Add(NodeMng.instance.NodeArr[CurrNode.Row + row, CurrNode.Col + col]);
                    }
                }

            }
        }


        for (int i = 0; i < attackable.Count; i++)
        {
            Debug.Log(attackable[i].name);
        }


        foreach (var x in ragelist)
        {
            if (x.CurrCHAR != null)
            {
                if (Dying)
                    continue;



                if (x.CurrCHAR == this)
                    continue;


                if (!FOE)
                {
                    if (!x.CurrCHAR.FOE)
                    {
                        if (!x.CurrCHAR.Dying)
                            attackable.Add(x.CurrCHAR);
                    }
                }
                else
                {
                    if (x.CurrCHAR.FOE)
                    {
                        if (!x.CurrCHAR.Dying)
                            attackable.Add(x.CurrCHAR);
                    }
                }

                //if (x.CurrCHAR.gameObject.layer == 9)
                //{
                //    attackable.Add(x.CurrCHAR);
                //}
            }
        }

        return attackable;

    }







    public virtual PixelFx FxCall()
    {
        return null;
    }

    public virtual PixelFx ProjectileCall()
    {
        return FxMng.Instance.FxCall("Arrow");
    }


    public void ClearNode()
    {
        m_currnode = null;
        m_prevnode = null;
        m_tecticsnode = null;
    }

    public void RayforNode()
    {
        RaycastHit hitobj;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.down, out hitobj, 1f, 1 << 8))
        {
            m_currnode = hitobj.transform.GetComponent<Node>();
            m_currnode.CharEE(this);
        }

        //else
        //{
        //    if (m_currnode != null)
        //    {
        //        m_currnode.CharEE(null);
        //        m_currnode = null;
        //    }
        //}

        if (m_currnode != null)
            m_currnode.Setbool(true);

        if (m_prevnode != m_currnode)
        {
            if (m_prevnode != null)
            {
                m_prevnode.CharEE(null);
                m_prevnode.Setbool(false);
                m_prevnode = m_currnode;
                RangeSet();
            }
        }
    }

    public void Isonline(bool se)
    {
        m_isonline = se;
    }

    public void Isonfield(bool se)
    {
        m_isonfield = se;
        if (se)
            m_standbynode = CurrNode;
        else
            m_standbynode = null;
    }



    private IEnumerator IERay()
    {
        while (true)
        {
            if (gameObject.activeInHierarchy)
            {
                RayforNode();
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            RayforNode();
        }
    }

    public virtual void AttackAction(Node target)
    {

        SetAttacking(true);


        if (MyStatus.Range == 1)
            StartCoroutine(ActionContainer.Instance.IEAttack(this, target));
        else if (MyStatus.Range >= 2)
            StartCoroutine(ActionContainer.Instance.IELongRange(this, target));
    }



    private Vector3 deathpoint = new Vector3();

    public void KillThis(string state = null)
    {

        deathpoint = transform.position;

        if (state == "sale")
        {
            ItemMng.instance.ItemReTurn(MyStatus.m_Equipment, deathpoint);
        }

        GetComponent<DragHelper>().m_oripos = transform.parent.position;
        ClearNode();
        Isonline(false);
        Fusion(false);
        Isonfield(false);
        gameObject.SetActive(false);


        if (state == "fusion")
        {
            transform.position = transform.parent.position;
            m_exclude = true;
        }
        else if(state == "sale")
        {
            deathpoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            TraceUp();

        }
       

    }

    public void TraceUp()
    {
        ReSetExclud();
        BaseChar upstream = FusionParent;
        if(upstream!=null)
        {
            m_fusionparent = null;
            upstream.TraceUp();
            upstream.KillThis();
        }

    }

    public void ReturnStandby()
    {
        m_status.SetLife(m_status.MaxLife);
        transform.position = m_standbynode.transform.position;
        gameObject.SetActive(true);
    }


    public virtual void DestroyThis()
    {

       foreach(var x in MyStatus.m_fxrunninglist)
        {
            if(x!=null)
                x.ShutActive();
        }

        CharMng.Instance.TotalHeros.RemoveAt(CharMng.Instance.FindID(UniqueID));
        if (FOE)
        { CharMng.Instance.CurrHeros.RemoveAt(CharMng.Instance.FindHeroID(UniqueID)); }
        else
        { CharMng.Instance.CurrEnemys.RemoveAt(CharMng.Instance.FindEnemyID(UniqueID)); }


        CurrNode.Setbool(false);
        CurrNode.NodeClean();


        gameObject.SetActive(false);
        m_dying = false;
        m_status.SetLife(m_status.MaxLife);




        if (!FOE)
        {
            if (FactoryID >= m_count)
            {
                Destroy(gameObject);
                return;
            }
            transform.SetParent(MobPooling.Instance.transform.Find(GetComponent<BaseChar>().GetType().Name));
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

    }
}
