using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_manager : MonoBehaviour
{
    //variables 
    [Header("Spawn Variables:")]
    public float height;
    public float max_height;
    public float max_height_boundary;
    public float min_height;
    public float min_height_boundary;
    public float width;
    public float max_width;
    public float max_width_boundary;
    public float min_width;
    public float min_width_boundary;

    public float spawn_time;
    public float spawn_time_min;
    public float spawn_time_max;

    public GameObject bunny_prefab;

    private bool start_calculating;
    private int algorithm;
    private game_controller controller;

    void Start()
    {
        controller = FindObjectOfType<game_controller>();
        StartCoroutine(game_loop());
    }
    //once a position meets the range requirement then instantiate bunny
    public void Update()
    {

        if (start_calculating)
        {
            algorithm = Random.Range(1, 3);

            switch (algorithm)
            {
                case 1:
                    height = Random.Range(min_height_boundary, max_height_boundary);
                    width = Random.Range(min_width, max_width);

                    if (height >= max_height || height <= min_height)
                    {
                            start_calculating = false;
                            Instantiate(bunny_prefab, new Vector2(width, height), Quaternion.identity);
                    }
                    break;
                case 2:
                    height = Random.Range(min_height, max_height);
                    width = Random.Range(min_width_boundary, max_width_boundary);

                    if (width >= max_width || width <= min_width)
                    {
                        start_calculating = false;
                        Instantiate(bunny_prefab, new Vector2(width, height), Quaternion.identity);
                    }
                    break;
            }
        }
    }

    public void start_game()
    {
        StartCoroutine(game_loop());
    }

    //while the game is started then keep spawning bunnies at different intervals
    public IEnumerator game_loop()
    {
        while (controller.game_started)
        {
            spawn_time = Random.Range(spawn_time_min, spawn_time_max);
            yield return new WaitForSeconds(spawn_time);
            start_calculating = true;
        }
    }
}
