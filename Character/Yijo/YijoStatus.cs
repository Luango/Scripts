using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YiStatus : MonoBehaviour {
    public int curr_health;
    public int max_health;
    public int health_recover_speed = 0;
    public bool damaged;
    public float damagedTime = 0;

    static public int curr_mana;
    public int max_mana;
    public int mana_recover_speed = 0;
    public bool casted;

    static public int yuan;

    // Current arrow type.
    public enum Arrow_Type {Normal, Space};
    public static Arrow_Type current_Arrow;

    // Equipments: Head, Armor, Pants, Feet.
    struct Equipment
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            AddYuan();
            collision.gameObject.GetComponent<Coins>().PickedUp();
        }
    }
    // Use this for initialization
    void Awake () {
        current_Arrow = Arrow_Type.Normal;
        max_health = 3;
        curr_health = max_health;
        yuan = 1000;
	}

    void Update()
    {
        HealthRecover();
        ManaRecover();

        if (curr_health <= 0f)
        {
            Debug.Log("SI LE");
            Destroy(this.gameObject);
        }
        if (damagedTime>0f)
        {
            damagedColor();
            damagedTime -= Time.deltaTime;
        }
        else
        {
            healthyColor();
        }
    }

    private void HealthRecover()
    {
        if (curr_health < max_health)
        {
            curr_health += health_recover_speed;
        }
    }

    private void ManaRecover()
    {
        if (curr_mana < max_mana)
        {
            curr_mana += mana_recover_speed;
        }
    }

    public void ChangeArrow(Arrow_Type arrow_type)
    {
        current_Arrow = arrow_type;
    }

    public void Damaged(int damage)
    {
        curr_health -= 1;
        YijoStatusUI.UpdateHealth();
        
        damagedColor();
        damaged = true;
        damagedTime = Constants.PlayerDamagedTime;
    }

    private void damagedColor()
    {
        Component[] SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer Sprite in SpriteRenderers)
        {
            Sprite.color = Color.red;
        }
    }
    private void healthyColor()
    {
        Component[] SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer Sprite in SpriteRenderers)
        {
            Sprite.color = Color.white;
        }
    }

    // Cast speel arrow
    public void Casted(int manaCosts)
    {
        curr_mana -= manaCosts;
        YijoStatusUI.UpdateMana();
    }

    public void AddYuan()
    {
        yuan += 1;
        YijoStatusUI.UpdateYuan();
    }
}
