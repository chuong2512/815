using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDElay : MonoBehaviour
{
    public int time = 4;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
