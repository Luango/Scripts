using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class YijoStatusUI : MonoBehaviour {
    public GameObject health;
    public GameObject mana;
    public GameObject yuan;
    static private bool damaged;
    static private bool casted; // Shot special arrow
    static private bool addedYuan;

	// Use this for initialization
	void Awake () {
        damaged = false;
        Text manaText = mana.GetComponent<Text>();
        manaText.text = "Mana: " + YijoStatus.curr_mana;

        Text healthText = health.GetComponent<Text>();
        //healthText.text = "Health: " + YijoStatus.curr_health;

        Text yuanText = yuan.GetComponent<Text>();
        //yuanText.text = "Yuans: " + YijoStatus.yuan;
    }
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            Text healthText = health.GetComponent<Text>();
            //healthText.text = "Health: "+ YijoStatus.curr_health;
        }

        if (casted)
        {
            Text manaText = mana.GetComponent<Text>();
            manaText.text = "Mana: " + YijoStatus.curr_mana;
        }

        if (addedYuan)
        {
            Text yuanText = yuan.GetComponent<Text>();
            yuanText.text = "Yuan: " + YijoStatus.yuan;
        }

        damaged = false;
        casted = false;
        addedYuan = false;
	}
    public void IsDamaged()
    {
        damaged = true;
    }
    public void IsCasted()
    {
        casted = true;
    }
    static public void UpdateHealth()
    {
        YijoStatusUI.damaged = true;
    }
    static public void UpdateMana()
    {
        YijoStatusUI.casted = true;
    }
    static public void UpdateYuan()
    {
        YijoStatusUI.addedYuan = true;
    }
}
