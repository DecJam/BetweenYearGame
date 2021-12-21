using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform m_Player = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = m_Player.position;
        pos.y = 17;
        pos.z -= 15;
        gameObject.transform.position = pos;
    }
}
