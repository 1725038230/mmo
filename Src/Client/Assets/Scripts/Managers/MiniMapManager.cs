using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Managers
{
    class MiniMapManager:Singleton<MiniMapManager>
    {
        public UIMiniMap miniMap;
        private Collider miniMapBoundingBox;
        public  Collider MiniMapBoundingBox
        {
            get { return miniMapBoundingBox; }
        }

        public Transform PlayerTransform
        {
            get
            {
                if (User.Instance.CurrentCharacterObject==null)
                    return null;
                return User.Instance.CurrentCharacterObject.transform;
            }
        }
        public Sprite LoadCurrentMiniMap()
        {
            return Resloader.Load<Sprite>("UI/MiniMap/" + User.Instance.CurrentMapData.MiniMap);
        }

        public void UpdateMinimap(Collider miniMapBoundingBox)
        {
            this.miniMapBoundingBox = miniMapBoundingBox;
            if (this.miniMap != null)
                this.miniMap.UpdateMap();
        }
    }
}
