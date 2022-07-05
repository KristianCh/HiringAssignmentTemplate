using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Offset from player
    private Vector3 PlayerOffset = new Vector3(5, 0, 0);
    // Base offset
    private Vector3 BaseOffset = new Vector3(0, 2, -10);

    // Speed of camera
    private float FollowSpeed = 3;

    // Update is called once per frame
    void Update()
    {
        // Follow player if exists
        if (Player.Instance != null)
        {
            transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position + PlayerOffset + BaseOffset, Time.deltaTime * FollowSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, BaseOffset, Time.deltaTime * FollowSpeed);
        }
    }
}
