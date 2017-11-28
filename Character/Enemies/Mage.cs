using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour {
    public Animator _anim_mage;
    public GameObject _Yijo;
    private bool facingYijo = false;

    private float shoot_frequency = 0.0f;
    private float attack_range = 50f;
    public GameObject _test_Arrow;
    public Transform shot_position;
   
    public float health;

	// Use this for initialization
	void Start () {
        health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
        if (_Yijo != null)
        {
            Detect_Shoot_Yijo();
        }

        // Hitted

        // Die
        if (health < 0)
        {
            // Start death animation
            _anim_mage.SetBool("Attack", false);
            _anim_mage.SetBool("Dead", true);

            // Remove object
            Destroy(transform.gameObject, 3f);
        }

        // Face Yijo
        LookAtYijo();

    }

    private void Detect_Shoot_Yijo()
    {

        // Attack Yijo
        // calculate the distance
        float _distanceToYijo = Vector3.Distance(shot_position.position, _Yijo.transform.position);
        shoot_frequency -= Time.deltaTime;
        if (_distanceToYijo < attack_range && health > 0f)
        {
            if (shoot_frequency < 0f)
            {
                // Attack
                Vector3 target_position = _Yijo.transform.position + new Vector3(0f, 2.5f, 0f);
                Attack_Yijo(target_position);
                shoot_frequency = 1.0f;
            }
            _anim_mage.SetBool("Attack", true);
        }
        else
        {
            _anim_mage.SetBool("Attack", false);
        }
    }

    private void Attack_Yijo(Vector3 target_position)
    {
        // Instantiate a Magic ball
        GameObject an_arrow = Instantiate(_test_Arrow, shot_position.position, Quaternion.identity) as GameObject;
        float orientation = an_arrow.GetComponent<Normal_Arrow>().Arrow_init_orientation(target_position.x - shot_position.position.x,
        target_position.y - shot_position.position.y, 5.0f, 0.31f);
        Vector3 velocity = new Vector3(Mathf.Cos(orientation), Mathf.Sin(orientation), 0f);

        an_arrow.GetComponent<Normal_Arrow>().velocity = velocity * 5f; // 5.0f is the speed
        an_arrow.GetComponent<Normal_Arrow>().shooter = transform.gameObject;
    }

    void Flip()
    {
        facingYijo = !facingYijo;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void LookAtYijo()
    {
        if (_Yijo != null)
        {
            if (_Yijo.transform.position.x < transform.position.x && !facingYijo)
            {
                Flip();
            }
            else if (_Yijo.transform.position.x > transform.position.x && facingYijo)
            {
                Flip();
            }
        }
    }

    public void getDamaged(float Damage)
    {
        health -= Damage;
    }
}
