using System;
using System.Collections;
using System.Collections.Generic;
using Common.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIShopItem : MonoBehaviour,ISelectHandler
{


    public Image icon;
    public Text title;
    public Text price;
    public Text count;

    public Image background;
    public Sprite normalBg;
    public Sprite selectedBg;
    private bool selected;
    public bool Selceted
    {
        get { return selected; }
        set
        {
            selected = value;
            this.background.overrideSprite = selected ? selectedBg : normalBg;
        }
    }

    public int ShopItemID { set; get; }

    private UIShop shop;
    private ItemDefine item;
    private ShopItemDefine ShopItem { get; set; } 

    public void SetShopItem(int id, ShopItemDefine shopItem, UIShop owner)
    {
        this.shop = owner;
        this.ShopItemID = id;
        this.ShopItem = shopItem;
        this.item = DataManager.Instance.Items[this.ShopItem.ItemID];
    //    if(ShopItem.Count==1)
        this.title.text = item.Name;
      //  else this.title.text = item.Name + "X" + ShopItem.Count;
        this.price.text = shopItem.Price.ToString();
        this.count.text = shopItem.Count.ToString();
        this.icon.overrideSprite = Resloader.Load<Sprite>(item.Icon);
    }
    public void OnSelect(BaseEventData eventData)
    {
        this.shop.SelectedShopItem(this);
        this.Selceted = true;
    }
}
