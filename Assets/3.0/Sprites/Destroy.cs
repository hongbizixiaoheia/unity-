using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyGameObject()
    {
        // Destroy this gameobject, this can be called from an Animation Event.
        Destroy(gameObject);
    }
}
