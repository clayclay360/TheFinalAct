using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_script : MonoBehaviour
{
    public float timer;

    void Start()
    {
        Destroy(gameObject, timer);
    }
}
