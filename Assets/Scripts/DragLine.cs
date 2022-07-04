using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    private Vector3 StartPosition = Vector3.zero;
    private Vector3 EndPosition = Vector3.zero;
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
        if (Input.GetMouseButtonDown(0))
        {
            StartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
            LayerMask mask = LayerMask.GetMask("Player");
            RaycastHit2D hit = Physics2D.Raycast(StartPosition, -Vector2.up, 0.1f, mask);
            if (hit.collider != null)
            {
                StartedOnPlayer = true;
                if (m_LineRenderer == null)
                {
                    m_LineRenderer = gameObject.AddComponent<LineRenderer>();
                }
                m_LineRenderer.enabled = true;
                m_LineRenderer.positionCount = 2;
                m_LineRenderer.SetPosition(0, Player.Instance.transform.position + PlayerOffset);
            }
            else
            {
                StartedOnPlayer = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + CameraOffset;
            m_LineRenderer.SetPosition(1, EndPosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_LineRenderer.enabled = false;

            LayerMask mask = LayerMask.GetMask("Room");
            RaycastHit2D hit = Physics2D.Raycast(EndPosition, -Vector2.up, 0.1f, mask);
            if (hit.collider != null && StartedOnPlayer)
            {
                Room room = hit.collider.gameObject.GetComponent<Room>();
                room.InteractWithRoom();
            }
        }
    }
}
