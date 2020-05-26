using System;
using System.Collections;
using System.Collections.Generic;
using Common.Data;
using UnityEngine;

namespace Managers{

    class TestManager:Singleton<TestManager>
    {
        public void Init()
        {
           // NPCManager.Instance.RegisterNpcEvent(Common.Data.NpcFunction.InvokeShop, OnNpcInvokeShop);
           // NPCManager.Instance.RegisterNpcEvent(Common.Data.NpcFunction.InvokeInstance, OnNpcInvokeShopInstance);
        }

       /* private bool OnNpcInvokeShop(NpcDefine npc)
        {
            Debug.LogFormat("TestManager.OnNpcInvokeShop :NPC:[{0}:{1}]  Type:{2} Func:{3}", npc.ID, npc.Name, npc.Type, npc.Function);
            UITest test = UIManager.Instance.Show<UITest>();
            test.SetTitle(npc.Name);
            return true;
        }

        private bool OnNpcInvokeShopInstance(NpcDefine npc)
        {
            Debug.LogFormat("TestManager.OnNpcInvokeShop :NPC:[{0}:{1}]  Type:{2} Func:{3}", npc.ID, npc.Name, npc.Type, npc.Function);
            MessageBox.Show("点击了NPC：" + npc.Name, "NPC对话");
            return true;
        }*/

    }
}
