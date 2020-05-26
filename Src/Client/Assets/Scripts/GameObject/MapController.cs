using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public Collider miniMapBoundingBox;
	void Start () {
        MiniMapManager.Instance.UpdateMinimap(miniMapBoundingBox);
	}
	
}
