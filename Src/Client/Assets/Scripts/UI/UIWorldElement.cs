﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldElement : MonoBehaviour {

    public Transform owner;
    public float height = 2f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(owner!=null)
        {
            this.transform.position = owner.position + Vector3.up * height;
        }
        this.transform.forward = Camera.main.transform.forward;
	}
}
