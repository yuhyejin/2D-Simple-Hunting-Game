using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera m_Camera;
    public Transform m_Player;

    private void Start()
    {

    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 pos = m_Player.position;
        pos.Set(m_Player.position.x, m_Player.position.y, transform.position.z);
        m_Camera.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
}
