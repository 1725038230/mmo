using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using SkillBridge.Message;
using System;

public class UILogin : MonoBehaviour {

    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    public InputField username;
    public InputField password;
    // Use this for initialization
    void Start () {
        UserService.Instance.OnLogin = this.OnLogin;
	}

    private void OnLogin(SkillBridge.Message.Result result, string msg)
    {
        MessageBox.Show(string.Format("结果：{0} msg:{1}", result, msg));
        if(result==Result.Success)
        {
            string name = "CharSelect";
            SceneManager.Instance.LoadScene(name);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    
    public void OnClickRegister()
    {
        RegisterPanel.SetActive(true);
        LoginPanel.SetActive(false);
    }

    public void OnClickLoin()
    {
        if (string.IsNullOrEmpty(this.username.text))
        {
            MessageBox.Show("请输入账号");
            return;
        }
        if (string.IsNullOrEmpty(this.password.text))
        {
            MessageBox.Show("请输入密码");
            return;
        }
        Debug.Log(username.text + password.text);
        UserService.Instance.SendLogin(this.username.text, this.password.text);
    }
}
