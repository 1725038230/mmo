using Common.Data;
using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestSystem : UIWindow {

    public TextAlignment title;

    public GameObject itemPrefab;
    public ListView listMain;
    public ListView listBranch;
    public TabView Tabs;

    public UIQuestInfo questInfo;

    private bool showAvailableList = false;

    public ListView.ListViewItem selectedItem=null;

    private ListView.ListViewItem SelectedItem
    {
       set
       {
            selectedItem = value;
            if (!listMain.IsContains(value))
                listMain.SelectedItem = null;
            if (!listBranch.IsContains(value))
                listBranch.SelectedItem = null;
       }
    }
  //  public SelectedItem selectedItem;

	void Start () {
        this.listMain.onItemSelected += this.OnQuestSelected;
        this.listBranch.onItemSelected += this.OnQuestSelected;
        this.Tabs.OnTabSelect += OnSelectedTab;
        RefreshUI();
	}
	
	// Update is called once per frame
   void OnSelectedTab(int idx)
   {
        showAvailableList = idx ==1;
        RefreshUI();
   }

    private void OnDestroy()
    {
        
    }

    void RefreshUI()
    {
        ClearAllQuestList();
        InitAllQuestItems();
    }

    void ClearAllQuestList()
    {
        this.listMain.RemoveAll();
        this.listBranch.RemoveAll();
    }
    void InitAllQuestItems()
    {
        foreach(var kv in QuestManager.Instance.allQuests)
        {
            if(showAvailableList)
            {
                if (kv.Value.Info != null) continue;
            }
            else
            {
                if (kv.Value.Info == null) continue;
            }

            GameObject go = Instantiate(itemPrefab, kv.Value.Define.Type == QuestType.Main ? listMain.transform : listBranch.transform);
            UIQuestItem ui = go.GetComponent<UIQuestItem>();
            ui.SetQuestInfo(kv.Value);
            if (kv.Value.Define.Type == QuestType.Main)
                this.listMain.AddItem(ui);
            else
                this.listBranch.AddItem(ui);
        }
    }
    
    public void OnQuestSelected(ListView.ListViewItem item)
    {
        SelectedItem = item;
        UIQuestItem questItem = item as UIQuestItem;
        this.questInfo.SetQuestInfo(questItem.quest);
    }
}
