using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disable_scriipt : MonoBehaviour
{
    public GameObject target_object;

    public void disable()
    {
        target_object.SetActive(false); // set game object inactive
    }

    public void enable()
    {
        target_object.SetActive(true); // set game object active
    }
}
