using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class player_controller : MonoBehaviour
{
    bool ready_to_throw, cards_thrown;
    float local_x;

    Vector2 mouse_position;
    Animator animator;
    Ray ray;
    Quaternion rot;
    game_controller controller;
    Collider2D col;

    [Header("Card Info:")]
    public int max_number_of_cards;
    public int number_of_cards;
    public float wait_time;
    [Header("Spawn Info:")]
    public Transform spawn_transform;
    [Header("GameObject Info:")]
    public GameObject card_prefab;
    public GameObject head_gameobject, body_gameobject;
    [Header("Player Info:")]
    public int lives;
    public Image[] hearts, cards;
    public float fade_time;

    void Start()
    {
        controller = FindObjectOfType<game_controller>();
        col = GetComponent<Collider2D>();

        lives = 3;
        ready_to_throw = true;
        local_x = body_gameobject.transform.localScale.x;

        number_of_cards = max_number_of_cards;
        get_components();
    }

    private void get_components()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mouse_position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        look_at_card();
        card_restock();

        if (Input.GetMouseButton(0) && ready_to_throw && controller.game_started)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ready_to_throw = false;

            if (0.5f <= mouse_position.x)
            {
                body_gameobject.transform.localScale = new Vector2(local_x, body_gameobject.transform.localScale.y);
            }
            else
            {
                body_gameobject.transform.localScale = new Vector2(-local_x, body_gameobject.transform.localScale.y);
            }

            cards_thrown = false;
            throw_cards();
        }
    }

    private void look_at_card()
    {
        if (0.5f <= mouse_position.x)
        {
            head_gameobject.transform.localScale = new Vector2(1, head_gameobject.transform.localScale.y);
        }
        else
        {
            head_gameobject.transform.localScale = new Vector2(-1, head_gameobject.transform.localScale.y);
        }
    }

    private void throw_cards()
    {
        animator.SetTrigger("throw");
    }

    public void get_card()
    {
        if (!cards_thrown)
        {
            if (body_gameobject.transform.localScale.x > 0)
            {
                rot = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                rot = Quaternion.Euler(0, 180, 0);
            }

            StartCoroutine(instance_card());
            cards_thrown = !cards_thrown;
        }
    }

    private IEnumerator instance_card()
    {
        for(int i = 0; i < number_of_cards; i++)
        {
            GameObject card = Instantiate(card_prefab, spawn_transform.position, rot);
            card_script script = card.GetComponent<card_script>();
            script.target_position = new Vector2(ray.origin.x, ray.origin.y);
            cards[i].enabled = true;
            yield return new WaitForSeconds(wait_time);
        }
    }

    private void card_restock()
    {
        if(number_of_cards == 0)
        {
            animator.SetTrigger("throw");
            number_of_cards = 3;
            ready_to_throw = true;
        }
    }

    public void deduct_life()
    {
        lives -= 1;
        hearts[lives].gameObject.SetActive(false);
        col.enabled = false;
        if (lives > 0)
        {
            StartCoroutine(fade());
        }
        else
        {
            animator.SetTrigger("death");
            controller.game_started = false;
            controller.destroy_all_bunnies();
            controller.display_end_credit();
            controller.destory_all_cards();
            foreach(Image c in cards)
            {
                c.enabled = false;
            }
        }
    }

    public void display_cards()
    {
        cards[number_of_cards].enabled = false;
    }

    IEnumerator end_game()
    {
        yield return new WaitForSeconds(0.25f);
        controller.display_end_credit();
    }

    private IEnumerator fade()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(fade_time);
            transform.position = new Vector3(transform.position.x, transform.position.y, 1000);
            yield return new WaitForSeconds(fade_time);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        yield return new WaitForSeconds(1);
        col.enabled = true;
    }
}
