using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_script : MonoBehaviour
{
    //variable
    public float timer;
    
    //destroy gameobject in certain amount of time
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
