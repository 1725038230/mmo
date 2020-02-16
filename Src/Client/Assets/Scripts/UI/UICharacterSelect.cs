using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using Services;
using SkillBridge.Message;
public class UICharacterSelect : MonoBehaviour {

    public GameObject panelCreate;
    public GameObject panelSelect;

    public GameObject btnCreateCancel;

    public InputField charName;
    CharacterClass charClass;

    public Transform uiCharList;
    public GameObject uiCharInfo;

    public List<GameObject> uiChars = new List<GameObject>(); 

    public Image[] titles;

    public Text descs;


    public Text[] names;

    private int selectCharacterIdx = -1;

    public UICharacterView characterView;

    // Use this for initialization
    void Start()
    {
        InitCharacterSelect(true);
        UserService.Instance.OnCreateCharacter = OnCharacterCreate;
    }


    public void InitCharacterSelect(bool init)
    {
        panelCreate.SetActive(false);
        panelSelect.SetActive(true);
        
        if (init)
        {
            foreach (var old in uiChars)
            {
                Destroy(old);
            }
            uiChars.Clear();
        }
        Debug.LogFormat("数量：" + User.Instance.Info.Player.Characters.Count);
        for(int i=0;i<User.Instance.Info.Player.Characters.Count;i++)
        {
            GameObject go = Instantiate(uiCharInfo, uiCharList);
            UICharInfo charinfo = go.GetComponent<UICharInfo>();
            charinfo.info = User.Instance.Info.Player.Characters[i];
            Button button =go.GetComponent<Button>();
            int idx = i;
            button.onClick.AddListener(() =>
            {
                OnSelectCharacter(idx);
            });
            uiChars.Add(go);
            go.SetActive(true);
        }
    }

    public void InitCharacterCreate()
    {
        panelCreate.SetActive(true);
        panelSelect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickCreate()
    {
        if(string.IsNullOrEmpty(charName.text))
        {
            MessageBox.Show("请输入角色昵称");
            return;
        }
        UserService.Instance.SendCharacter(charName.text,charClass);
    }

    public void OnSelectClass(int charClass)
    {
        this.charClass = (CharacterClass)charClass;
        characterView.CurrectCharacter = charClass - 1; 
       // DataManager.Instance.LoadData();
        for (int i = 0; i < 3; i++)
        {
            titles[i].gameObject.SetActive(i == charClass - 1);
            //names[i].text = DataManager.Instance.Characters.Count.ToString();        
        }

        descs.text = DataManager.Instance.Characters[charClass].Description;
        //descs.text = DataManager.Instance.Characters.Count.ToString(); ;
    }


    void OnCharacterCreate(Result result, string message)
    {
        if (result == Result.Success)
        {
            InitCharacterSelect(true);

        }
        else
            MessageBox.Show(message, "错误", MessageBoxType.Error);
    }

    public void OnSelectCharacter(int idx)
    {
        Debug.Log("idx" + idx);
        this.selectCharacterIdx = idx;
        var cha = User.Instance.Info.Player.Characters[idx];
        Debug.LogFormat("Select Char:{0},  {1}   ,{2}", cha.Id, cha.Name, cha.Class);
        
        characterView.CurrectCharacter = (int)cha.Class - 1;
        for(int i=0;i<uiChars.Count;i++)
        {
            uiChars[i].GetComponent<UICharInfo>().Slected = i == idx;
        }
    }
    public void OnClickPlay()
    {
        if (selectCharacterIdx >= 0)
        {
         
            UserService.Instance.SendGameEnter(selectCharacterIdx);
        }
    }
}
