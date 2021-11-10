using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disable_scriipt : MonoBehaviour
{
    //variable
    public GameObject target_object;

    //disables gameobject
    public void disable()
    {
        target_object.SetActive(false); // set game object inactive
    }
    
    //enable gameobject
    public void enable()
    {
        target_object.SetActive(true); // set game object active
    }
}
