using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : MonoBehaviour {
    public delegate void CloseHandler(UIWindow Sender, WindowResult result);
    public event CloseHandler OnClose;

    public virtual System.Type Type
    {
        get
        {
            return this.GetType();
        }
    }
    public enum WindowResult
    {
        None=0,
        Yes,
        No, 
    }

    public  void Close(WindowResult result=WindowResult.None)
    {
        UIManager.Instance.Close(this.Type);
        if(OnClose!=null)
        {
            this.OnClose(this,result);
        }
        this.OnClose = null;
    }

    public virtual void OnCloseClick()
    {
        this.Close();
    }

    public virtual void OnYesClcik()
    {
        this.Close(WindowResult.Yes);
    }

}
