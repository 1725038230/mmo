using Common.Data;
using Managers;
using Models;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UIWindow {

    public Text title;
    public Text money;
    public GameObject ShopItem;
    ShopDefine shop;
    public Transform[] itemRoot;
	void Start () {
        StartCoroutine(InitItems());
        User.Instance.goldChange +=this. SetMoney;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator InitItems()
    {
        int i = 0;
        foreach(var kv in DataManager.Instance.ShopItems[shop.ID])
        {
            if(kv.Value.Status>0)
            {
                GameObject go = Instantiate(ShopItem, itemRoot[i/22]);
                UIShopItem ui = go.GetComponent<UIShopItem>();
                ui.SetShopItem(kv.Key, kv.Value, this);
                i++;
            }
        }
        yield return null;
    }

    public void SetShop(ShopDefine shop)
    {
        this.shop = shop;
        this.title.text = shop.Name;
        this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
    }
    public UIShopItem selectedItem;
    public void SelectedShopItem(UIShopItem Item)
    {
        if (selectedItem != null)
            selectedItem.Selceted = false;
        selectedItem = Item;
    }

    public void OnClickBuy()
    {
        if (this.selectedItem == null)
        {
            MessageBox.Show("请选择要购买的道具", "购买提示");
            return;
        }
        if(!ShopManager.Instance.BuyItem(this.shop.ID,this.selectedItem.ShopItemID))
        {

        }
    }

    public void SetMoney()
    {
        this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
    }

    public void OnDestroy()
    {
        User.Instance.goldChange -= this.SetMoney;
    }
}
