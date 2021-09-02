using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_script : MonoBehaviour
{
    public float speed, rotate_speed;
    [HideInInspector]
    public Vector2 target_position;

    Rigidbody2D rb;
    Ray ray;
    player_controller controller;
    Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<player_controller>();
        col = GetComponent<Collider2D>();
        StartCoroutine(turnon_collider());
    }

    void Update()
    {
        get_input();
    }

    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        move_towards_target(target_position);
        rb.velocity = speed * transform.right;
    }

    void get_input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            target_position = new Vector2(ray.origin.x, ray.origin.y);
        }
    }

    void move_towards_target(Vector2 target_position)
    {
        Vector2 direction = target_position - rb.position;
        direction.Normalize();
        
        float rotate_amount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = -rotate_amount * rotate_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bunny" || collision.tag == "Wall")
        {
            controller.number_of_cards -= 1;
            controller.display_cards();
            Destroy(gameObject);
        }
    }

    IEnumerator turnon_collider()
    {
        yield return new WaitForSeconds(0.25f);
        col.enabled = true;
    }
}
