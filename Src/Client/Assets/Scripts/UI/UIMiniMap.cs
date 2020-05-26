using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using Managers;
public class UIMiniMap : MonoBehaviour{

    public Image miniMap;
    public Image arrow;
    public Text mapName;
    public Collider MiniMapBondingBox;

    private Transform playerTransform;
	void Start () {
        MiniMapManager.Instance.miniMap = this;
            UpdateMap();
	}
	
    public void UpdateMap()
    {
        this.mapName.text = User.Instance.CurrentMapData.Name;
        this.miniMap.overrideSprite = MiniMapManager.Instance.LoadCurrentMiniMap();
        this.miniMap.SetNativeSize();
        this.miniMap.transform.localPosition = Vector3.zero;
        this.MiniMapBondingBox = MiniMapManager.Instance.MiniMapBoundingBox;
        playerTransform = null;
    }
	// Update is called once per frame
	void Update () {
        if(playerTransform==null)
        {
            playerTransform = MiniMapManager.Instance.PlayerTransform;
        }
        if(MiniMapBondingBox==null||playerTransform==null)return;

        float realWidth = MiniMapBondingBox.bounds.size.x;
        float realHeight = MiniMapBondingBox.bounds.size.z;

        float relaX = playerTransform.transform.position.x-MiniMapBondingBox.bounds.min.x;
        float relaY = playerTransform.transform.position.z - MiniMapBondingBox.bounds.min.z;
        float pivotX = relaX / realWidth;
        float pivotY = relaY / realHeight;

        this.miniMap.rectTransform.pivot = new Vector2(pivotX, pivotY);
        this.miniMap.rectTransform.localPosition = Vector2.zero;
        this.arrow.transform.eulerAngles = new Vector3(0, 0, -playerTransform.eulerAngles.y);
    }
}
