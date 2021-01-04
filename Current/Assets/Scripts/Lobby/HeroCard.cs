using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCard : MonoBehaviour
{

    private Image m_faceimage;
    private Image m_expimage;
    private Text m_nametext;
    private Text m_lvtext;
    private int m_ID;
    private CardDrag m_drag;
    public CharacterBook m_book;

    private float m_exp;
    public GameObject m_checkmark;


    public Image Faceimage
    {
        get { return m_faceimage; }
    }

    public int CardID
    {
        get { return m_ID; }
    }

    public void Init()
    {
        m_book = CharacterBook.Instance;
        m_faceimage = transform.Find("FaceTray/Face").GetComponent<Image>();
        m_expimage = transform.Find("EXP/EXP").GetComponent<Image>();
        m_nametext = transform.Find("Name").GetComponent<Text>();
        m_lvtext = transform.Find("Lv").GetComponent<Text>();
        m_drag = GetComponent<CardDrag>();
        m_drag.Init();
        m_checkmark = transform.Find("CheckMark").gameObject;
    }

    public void CreateCard(Hero hero, int idx)
    {
        m_ID = idx;
        hero.CardSet(m_ID);
        Init();
        m_faceimage.sprite = hero.Face;
        m_nametext.text = hero.MyStatus.Name;
        m_lvtext.text = hero.MyStatus.LV.ToString();
    }

    public void OpenBook()
    {
        m_book.InventoryReset();
        m_book.ItemInfo.ClickSlot(1000);
        CharacterBook.Instance.m_bookidx = CardID;
        m_book.PageOpen(CardID);
    }


}
