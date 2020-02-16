using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Network;
using SkillBridge.Message;
namespace GameServer.Services
{
    class HelloWorldService:Singleton<HelloWorldService>
    {
        public void Init()
        {

        }
        public void Start()
        {
            MessageDistributer<NetConnection<NetSession>>.Instance.Subscribe<FirstTestRequst>(this.OnFirstTestRequest);
        }
        void OnFirstTestRequest(NetConnection<NetSession>sender, FirstTestRequst request)
        {
            Log.InfoFormat("hello:{0}",request.Helloworld);
        }
    }
}
