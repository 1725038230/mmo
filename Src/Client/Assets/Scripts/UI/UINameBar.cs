using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINameBar : MonoBehaviour
{

    public Text AvataName;

    public Character character;
    void Start()
    {
        if (character != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateInfo();
    }

    void UpdateInfo()
    {
        if (character != null)
        {
            string name = this.character.Name + "Lv:" + this.character.Info.Id;
            if (name != this.AvataName.text)
            {
                AvataName.text = name;

            }
        }
    }
}