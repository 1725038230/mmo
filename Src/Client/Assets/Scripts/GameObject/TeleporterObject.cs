﻿using Common.Data;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterObject : MonoBehaviour {

    public int ID;
    Mesh mesh = null;
	void Start () {
        this.mesh = this.GetComponent<MeshFilter>().sharedMesh;
	}
	
	// Update is called once per frame
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(mesh!=null)
        {
            Gizmos.DrawWireMesh(this.mesh, this.transform.position + Vector3.up * this.transform.localScale.y * .5f, this.transform.rotation, this.transform.localScale);
        }
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.ArrowHandleCap(0, this.transform.position, this.transform.rotation, 1f, EventType.Repaint);
    }
#endif

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        PlayerInputController playerInputController = other.GetComponent<PlayerInputController>();
        if(playerInputController!=null&&playerInputController.isActiveAndEnabled)
        {
            TeleporterDefine td = DataManager.Instance.Teleporters[this.ID];
            if(td==null)
            {
                Debug.LogErrorFormat("TeleportObject:Character [{0}] Enter Teleporter[{1}}]", playerInputController.character.Info.Name, this.ID);
                return;
            }
            Debug.LogFormat("TeleportObject:Character [{0}] Enter Teleporter [{1}:{2}]", playerInputController.character.Info.Name, td.ID, td.Name);
            if(td.LinkTo>0)
            {
                if (DataManager.Instance.Teleporters.ContainsKey(td.LinkTo))
                    MapService.Instance.SendMapTeleporter(this.ID);
                else
                    Debug.LogErrorFormat("TeleporTer ID:{0} LinkID {1} error!", td.ID, td.LinkTo);
            }
        }
    }
}
