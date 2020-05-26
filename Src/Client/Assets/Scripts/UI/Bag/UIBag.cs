using Managers;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : UIWindow {
    
    public Text Money;
    public Transform[] pages;
    public GameObject BagItem;
    List<Image> slots;

    
    void Start()
    {
        if(slots==null)
        {
            slots = new List<Image>();
            for(int page=0;page<this.pages.Length;page++)
            {
                slots.AddRange(this.pages[page].GetComponentsInChildren<Image>(true));
            }
        }
        StartCoroutine(InitBags());
        Debug.Log("BagStart");
       // BagManager.Instance.bagChange += this.UpdateUIBag;
    }

    IEnumerator InitBags()
    {
    
        for(int i=0;i<BagManager.Instance.Items.Length;i++)
        {
            ;
            var item = BagManager.Instance.Items[i];
            if(item.ItemId>0)
            {
                GameObject go = Instantiate(BagItem, slots[i].transform);
                var ui = go.GetComponent<UIIconItem>();
                var def = ItemManager.Instance.Items[item.ItemId].Define;
                ui.SetMainIcon(def.Icon, item.Count.ToString());
            }
        }
        for(int i= BagManager.Instance.Items.Length; i< slots.Count;i++)
        {
            slots[i].color=Color.gray;
        }
        this.SetMoney();
        Debug.Log("初始化");
        yield return null;
    }

    public void SetMoney()
    {
        this.Money.text = User.Instance.CurrentCharacter.Gold.ToString();
    }

    public void UpdateUIBag()//刷新
    {
        BagManager.Instance.Reset();
        ClearItems();
        StartCoroutine(InitBags());
    }
    
    void ClearItems()
    {
        foreach(var slot in slots)
        {
            if(slot.transform.childCount>0)
                Destroy(slot.transform.GetChild(0).gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("BagDestroy");
       // BagManager.Instance.bagChange -= this.UpdateUIBag;
    }
}
