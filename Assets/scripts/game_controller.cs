using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_controller : MonoBehaviour
{
    public bool game_started;
    public Text end_credit;
    public Text elmination_text;
    public Button play_button;


    public Animator curtain_animator;
    public Animator white_screen_animator;

    public Texture2D mouse_texture;

    private int elminations = 0;

    public void start_game()
    {
        game_started = true;
        curtain_animator.GetComponent<Animator>();
        Invoke("hide_play_button", .75f);
    }

    public void Update()
    {
        //get_mouse();
    }

    public void hide_play_button()
    {
        play_button.gameObject.SetActive(false);
    }

    public void display_end_credit()
    {
        end_credit.gameObject.SetActive(true);
        Invoke("lower_curtains",1);
    }

    private void lower_curtains()
    {
        curtain_animator.SetTrigger("decsend");
        StartCoroutine(restart_game());
    }

    public IEnumerator restart_game()
    {
        yield return new WaitForSeconds(2);
        white_screen_animator.SetTrigger("fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public void destroy_all_bunnies()
    {
        bunny_controller[] bunnies = FindObjectsOfType<bunny_controller>();
        
        foreach(bunny_controller b in bunnies)
        {
            b.death();
        }
    }

    public void destory_all_cards()
    {
        card_script[] cards = FindObjectsOfType<card_script>();

        foreach(card_script c in cards)
        {
            Destroy(c.gameObject);
        }
    }

    public void add_point()
    {
        elminations += 1;
        elmination_text.text = elminations.ToString();
    }

    private void get_mouse()
    {
        Cursor.SetCursor(mouse_texture, new Vector2(16, 16), CursorMode.Auto);
    }
}
