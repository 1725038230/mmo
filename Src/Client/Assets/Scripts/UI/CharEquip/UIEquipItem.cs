using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEquipItem : MonoBehaviour,IPointerClickHandler
{
    public Image icon;
    public Text title;
    public Text level;
    public Text limitClass;
    public Text limitCategory;

    public Image background;
    public Sprite normalBg;
    public Sprite selectedBg;

    private bool selected;

    public UICharEquip UICharEquip; 
    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            this.background.overrideSprite = selected ?selectedBg: normalBg;
        }
    }

    public int index { get; set; }
    private UICharEquip owner;

    private Item item;
    bool IsEquiped = false;

    // Use this for initialization
     public  void SetEquipItem(int idx,Item item ,UICharEquip owner,bool equiped)
    {
        this.owner = owner;
        this.index = idx;
        this.item = item;
       
        this.IsEquiped = equiped;

        if (this.title != null) this.title.text = this.item.Define.Name;
        if (this.level != null) this.level.text = this.item.Define.Level.ToString();
        if (limitClass != null) this.limitClass.text = item.Define.LimitClass.ToString();
        if (this.limitCategory != null) this.limitCategory.text = item.Define.Category;
        if (icon != null) this.icon.overrideSprite = Resloader.Load<Sprite>(this.item.Define.Icon);
        

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.Selected)
        {
            if (this.IsEquiped)
                UnEquip();
             else
                DoEquip();
             this.Selected =false;
            UICharEquip.SelectedEquip = null;
        }
         else
         {
             this.Selected = true;
             UICharEquip.SelectedEquip = this;
         }
                
    }

    void DoEquip()
    {
        var msg = MessageBox.Show(string.Format("要装备[{0}]吗？", this.item.Define.Name), "确认", MessageBoxType.Confirm);
        msg.OnYes = () =>
        {

              var oldEquip = EquipManager.Instance.GetEquip(item.EquipInfo.Slot);
              if (oldEquip != null)
              {
                  var newmsg = MessageBox.Show(string.Format("要替换掉[{0}]吗？", oldEquip.Define.Name, "确认", MessageBoxType.Confirm));
                  newmsg.OnYes = () =>
                  {
                      this.owner.DoEquip(this.item);
                  };
              }
              else
                  this.owner.DoEquip(this.item);

         };
    }

    void UnEquip()
    {
        var msg = MessageBox.Show(string.Format("要取下装备[{0}]吗？", this.item.Define.Name),"确认",MessageBoxType.Confirm);
        msg.OnYes = () =>
          {
              this.owner.UnEquip(this.item);
          };

    }


}
