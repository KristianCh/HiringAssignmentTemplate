using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Keep y position at floor level
        Vector3 newPos = transform.position;
        newPos.y = -7.7f;
        transform.position = newPos; 
    }
}
