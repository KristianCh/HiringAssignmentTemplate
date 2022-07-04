using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 Offset = new Vector3(5, 2, -10);
    private Vector3 Base = new Vector3(0, 2, -10);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance != null)
        {
            transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position + Offset, Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, PlayerTower.Instance.transform.position + Base, Time.deltaTime);
        }
    }
}
