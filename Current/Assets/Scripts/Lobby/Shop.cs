using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Mng
{

    private Transform m_itemslot;
    private Transform m_resource;
    private Transform m_saleitem;






    List<Button> m_slots = new List<Button>();
    List<Image> m_itemimages = new List<Image>();
    List<Button> m_saleslots = new List<Button>();
    List<Image> m_saleimages = new List<Image>();
    List<Text> m_namenprice = new List<Text>();
    List<Item> m_saleitems = new List<Item>();
    List<GameObject> m_solds = new List<GameObject>();
    private ItemInfo m_iteminfo;
    ResourceState m_resourcestate;


    public override void Init()
    {
        m_itemslot = transform.Find("ItemSlot");
        m_saleitem = transform.Find("SaleItem");
        m_iteminfo = GetComponentInChildren<ItemInfo>();
        m_iteminfo.ItemInfoInit();
        m_slots.AddRange(m_itemslot.GetComponentsInChildren<Button>(true));
        m_saleslots.AddRange(m_saleitem.GetComponentsInChildren<Button>(true));
        m_resourcestate = GetComponentInChildren<ResourceState>();

        int idx = 0;
        foreach (var x in m_slots)
        {
            int idxx = idx;
            x.onClick.AddListener(() => { m_iteminfo.ClickSlot(idxx); });
            m_itemimages.Add(x.transform.Find("Item").GetComponent<Image>());

            Item item = Inventory.instance.GetItem(idx);
            if (item != null)
            {
                m_itemimages[idx].sprite = item.m_sprite;
            }
            idx++;
        }
        idx = 0;
        foreach (var x in m_saleslots)
        {
            int idxx = idx;
            x.onClick.AddListener(() => { m_iteminfo.ClickShop(idxx); });
            m_saleimages.Add(x.transform.Find("Item").GetComponent<Image>());
            m_namenprice.Add(x.transform.Find("ItemName").GetComponent<Text>());
            m_solds.Add(x.transform.Find("Sold").gameObject);
            idx++;
        }

        ShopReset();
  
        Button[] buttons = transform.Find("Buttons").GetComponentsInChildren<Button>(true);
        buttons[0].onClick.AddListener(Buy);
        buttons[1].onClick.AddListener(Sell);
    }

    public void InvenReset()
    {
        int idx = 0;
        foreach (var x in m_slots)
        {
            Item item = Inventory.instance.GetItem(idx);
            if (item != null)
                m_itemimages[idx].sprite = item.m_sprite;
            else
                m_itemimages[idx].sprite = SpriteMng.s_blanksprite;

            idx++;
        }
    }

    public void ShopReset()
    {
        int idx = 0;
    
        foreach (var x in m_saleslots)
        {
            Item item = null;
            m_saleslots[idx].interactable = true;
            m_namenprice[idx].text = string.Empty;
            item = GetShopItem(idx);

            if (item != null)
            {
                m_saleimages[idx].sprite = item.m_sprite;
                m_namenprice[idx].text = NameReduce(item.m_name) + "\n" + item.m_price;
                m_solds[idx].SetActive(false);
            }
            else
            {
                m_saleslots[idx].interactable = false;
                m_solds[idx].SetActive(true);
            }
            idx++;
        }
    }

    private void Sell()
    {
        if (m_iteminfo.m_focus != 1)
            return;

        int idx = m_iteminfo.m_targetidx;
        Item item = Inventory.instance.GetItem(idx);
        InGameResource.instance.ResourceGain(ResourceType.Gold, (int)(item.m_price * 0.3f));
        Inventory.instance.RemoveItem(idx);

        InvenReset();
        m_resourcestate.RecallResource();
    }



    private void Buy()
    {
        if (m_iteminfo.m_focus != 0)
            return;


        int idx = m_iteminfo.m_targetidx;
        Item item = GetShopItem(idx);
        bool active = m_saleslots[idx].transform.Find("Sold").gameObject.activeSelf;

        if (active)
            return;

        if (!InGameResource.instance.ResourceConsume(ResourceType.Gold, item.m_price))
            return;

        Inventory.instance.AddItem(item);
        m_itemimages[Inventory.instance.ItemCount()-1].sprite = item.m_sprite;
        InvenReset();
        m_resourcestate.RecallResource();


        m_saleslots[idx].interactable = false; 
        m_saleslots[idx].transform.Find("Sold").gameObject.SetActive(true);


    }


    private static Item GetShopItem(int idx)
    {
        List<int> daystuff = TableMng.Instance.Table(TableType.ShopTable, GameData.Instance.m_globalturn) as List<int>;
        if (idx < daystuff.Count)
            return TableMng.Instance.Table(TableType.ITEMTable, daystuff[idx]) as Item;

        return null;
    }

    private string NameReduce(string name)
    {
        char[] reducename = name.ToCharArray();

        if (name.Length>11)
        {
            reducename = "...........".ToCharArray();
            for(int i =0; i<8;i++)
            {
                reducename[i]=name.ToCharArray()[i];
            }
        }

        return new string(reducename);
    }



}
