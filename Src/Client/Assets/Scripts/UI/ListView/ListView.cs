using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[System.Serializable]
public class ItemSelectEvent : UnityEvent<ListView.ListViewItem>
{
}

public class ListView : MonoBehaviour
{
    public UnityAction<ListViewItem> onItemSelected;
    public class ListViewItem : MonoBehaviour, IPointerClickHandler
    {
        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                onSelected(selected);
            }
        }
        public virtual void onSelected(bool selected)
        {
        }

        public ListView owner;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!this.selected)
            {
                this.Selected = true;
            }
            if (owner != null && owner.SelectedItem != this)
            {
                owner.SelectedItem = this;
            }
        }
    }

    List<ListViewItem> items = new List<ListViewItem>();

    public bool IsContains(ListViewItem item)
    {
        foreach(var k in items)
        {
            if (k ==item) return true;
        }
        return false;
    }

    private ListViewItem selectedItem = null;
    public ListViewItem SelectedItem
    {
        get { return selectedItem; }
        set
        {
            if (selectedItem!=null && selectedItem != value)
            {
                selectedItem.Selected = false;
            }
            selectedItem = value;
            if (value == null) return;
            if (onItemSelected != null)
                onItemSelected.Invoke((ListViewItem)value);
        }
    }

    public void AddItem(ListViewItem item)
    {
        item.owner = this;
        this.items.Add(item);
    }

    public void RemoveAll()
    {
        foreach(var it in items)
        {
            Destroy(it.gameObject);
        }
        items.Clear();
    }
}
