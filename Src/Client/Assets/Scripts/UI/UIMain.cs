﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
public class UIMain :MonoSingleton<UIMain> {

    public Text avatarName;
    public Text avatarLevel;
	protected override void OnStart () {
        this.UpdateAvatar();
	}
	
    void UpdateAvatar()
    {
        this.avatarName.text = string.Format("{0} [{1}]", User.Instance.CurrentCharacter.Name, User.Instance.CurrentCharacter.Id);
        this.avatarLevel.text = User.Instance.CurrentCharacter.Level.ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void BackToCharSelect()
    {
        SceneManager.Instance.LoadScene("CharSelect");
        Services.UserService.Instance.SendGameLeave();
    }

    public void OnClickTest()
    {
        UITest test=UIManager.Instance.Show<UITest>();
        test.SetTitle("这是一个测试ui");
    }

    public void OnClicckBag()
    {
        UIBag bag = UIManager.Instance.Show<UIBag>();
    }

    public void OnClicckCharEquip()
    {
        UICharEquip CharEquip = UIManager.Instance.Show<UICharEquip>();
    }

    public void OnClickQuest()
    {
       UIQuestSystem QuestSystem = UIManager.Instance.Show<UIQuestSystem>();
    }
}
