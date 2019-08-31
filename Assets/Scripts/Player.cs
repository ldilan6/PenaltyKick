using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var colls = GetComponentsInChildren<Collider>();
        foreach(var c in colls)
        {
            if (c.gameObject.name == "Player")
                continue;

            Debug.Log(c.name + " disabled collider");
            c.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
