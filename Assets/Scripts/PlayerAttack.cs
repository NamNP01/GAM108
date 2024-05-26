using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator anim;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    //public GameObject firePrefab;
    public GameObject attackPrefab;
    public GameObject firePoint;
    private Vector2 fireDirection;
    public float fireSpeed = 8;
    private float attackTime;
    public float attackTimeDuration = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            fireDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            fireDirection = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.J)&& attackTime <= 0)
        {
            Attack();
            attackTime = attackTimeDuration;
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    CastSpell();
        //}
    }
    //void CastSpell()
    //{
    //    anim.SetTrigger("CastSpell");
    //    GameObject newBullet = Instantiate(firePrefab, transform.position, Quaternion.identity);
    //    Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
    //    if (bulletRb != null)
    //    {

    //        bulletRb.velocity = fireDirection * fireSpeed;
    //    }
    //}
    void Attack()
    {   
        anim.SetTrigger("Attack");
        GameObject newBullet = Instantiate(attackPrefab,firePoint.transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        attackTime = attackTimeDuration;
        if (bulletRb != null)
        {

            bulletRb.velocity = fireDirection * fireSpeed;
        }
    }
}

