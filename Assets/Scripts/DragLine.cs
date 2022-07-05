using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    // Start and end positions
    private Vector3 StartPosition = Vector3.zero;
    private Vector3 EndPosition = Vector3.zero;
    // World offset of positions
    private Vector3 CameraOffset = new Vector3(0, 0, 11);
    private Vector3 PlayerOffset = new Vector3(0, 0, 1);
    
    private bool StartedOnPlayer = false;
    private LineRenderer m_LineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // On mouse down
        if (Input.GetMouseButtonDown(0))
        {
            // Create line renderer if none exists
            if (m_LineRenderer == null)
            {
                m_LineRenderer = gameObject.AddComponent<LineRenderer>();
                m_LineRenderer.positionCount = 2;
            }

            // Get world click position
            StartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
            EndPosition = StartPosition;

            // Perform raycast at start position against player
            LayerMask mask = LayerMask.GetMask("Player");
            RaycastHit2D hit = Physics2D.Raycast(StartPosition, -Vector2.up, 0.1f, mask);
            if (hit.collider != null)
            {
                // Enable line renderer and set start position
                StartedOnPlayer = true;
                m_LineRenderer.enabled = true;
                m_LineRenderer.SetPosition(0, Player.Instance.transform.position + PlayerOffset);
            }
            else
            {
                StartedOnPlayer = false;
            }
        }
        // While mouse button is down
        else if (Input.GetMouseButton(0))
        {
            // Update end position
            EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
            m_LineRenderer.SetPosition(1, EndPosition);
        }
        // On mouse button up
        else if (Input.GetMouseButtonUp(0))
        {
            // Disable line renderer
            m_LineRenderer.enabled = false;

            // Perform raycast at end positions agains rooms
            LayerMask mask = LayerMask.GetMask("Room");
            RaycastHit2D hit = Physics2D.Raycast(EndPosition, -Vector2.up, 0.1f, mask);

            // If room was hit, interact with it
            if (hit.collider != null && StartedOnPlayer)
            {
                Room room = hit.collider.gameObject.GetComponent<Room>();
                if (room != null)
                {
                    room.InteractWithRoom();
                }
            }
        }
    }
}
