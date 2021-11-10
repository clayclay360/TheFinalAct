using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunny_controller : MonoBehaviour
{
    //variables
    [Header("Bunny Variables:")]
    public float speed;
    public float range;
    public float death_time;
    public GameObject body, leg;
    public ParticleSystem ps;

    float wait_time;
    float x_scale;
    bool ready_to_hop, attack_ready, attacking;

    Rigidbody2D rb;
    player_controller player;
    game_controller controller;
    Animator animator;
    Collider2D col;

    void Start()
    {
        //get components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<player_controller>();
        col = GetComponent<Collider2D>();
        controller = FindObjectOfType<game_controller>();

        x_scale = transform.localScale.x;
        attack_ready = true;
    }

    private void Update()
    {
        //if not in range and is ready to hoop then bounce 
        if(Vector2.Distance(player.transform.position, transform.position) > range && ready_to_hop)
        {
            ready_to_hop = false;
            StartCoroutine(bounce());
        }
        //else if in range of attack and is ready to attack then attack
        else if(Vector2.Distance(player.transform.position, transform.position) <= range && attack_ready)
        {
            attack_ready = false;
            StartCoroutine(attack());
        }
    }

    public void move()
    {
        //get the direction of the player and move towards that direction
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        rb.velocity = dir * speed;
        
        //change the scale depending on the direction your moving
        if(dir.x < 0)
        {
            transform.localScale = new Vector2(-x_scale, transform.localScale.y);
        }
    }

    public void Restart()
    {
        ready_to_hop = true;
        attack_ready = true;
    }
    
    //wait for a specific period of time then play jump animation
    IEnumerator bounce()
    {
        wait_time = Random.Range(1, 2);
        yield return new WaitForSeconds(wait_time);
        animator.SetTrigger("hop");
    }

    //wait for a specific peroid of time then play attack animation
    IEnumerator attack()
    {
        wait_time = Random.Range(1, 3);
        yield return new WaitForSeconds(wait_time);
        animator.SetTrigger("attack");
    }

    //if hit by projectile die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            controller.add_point();
            death();
        }
    }
    
    //destroy yourself and instantiate particle system
    public void death()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /*note this is poorly written
    if you stay colliding with the player and is attacking, reduce player's life*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (attacking)
            {
                player_controller player = collision.GetComponent<player_controller>();
                player.deduct_life();
                attacking = false;
            }
        }
    }

    //cant even spell
    public void noy_attacking()
    {
        attacking = false;
    }
    public void is_attacking()
    {
        attacking = true;
    }
}
