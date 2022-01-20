using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField, Range(0.0f, 360.0f)] private float m_ViewAngle;
    private bool m_playerSpotted = false;
    [SerializeField] float dotP;
    [SerializeField] float playerAtangleDeg;
    [SerializeField] float playerAngleRad;
    [SerializeField,Range(1,10)] float ViewDistance;

    // Update is called once per frame
    void Update()
    {
        CheckSightLines();
        Move();
    }
    public void CheckSightLines()
	{
        float dot = Vector3.Dot(transform.forward.normalized, (Player.Instance.transform.position - transform.position).normalized);
        playerAngleRad = Mathf.Acos(dot);
        playerAtangleDeg = Mathf.Rad2Deg * playerAngleRad;
        dotP = dot;
        if (playerAtangleDeg <= m_ViewAngle * 0.5f)
		{
            m_playerSpotted = true;
		}
        else
		{
            m_playerSpotted = false;
		}
	}
    


	public void Move()
	{
		
	}

	private void OnDrawGizmos()
	{
        if(m_playerSpotted)
		{
            Gizmos.color = Color.red;
		}

		else
		{
            Gizmos.color = Color.green;
        }


        //Find a way to visualise View angle
        Vector3 directionRight = new Vector3( Mathf.Sin(Mathf.Deg2Rad * (m_ViewAngle * 0.5f)), 0, Mathf.Cos(Mathf.Deg2Rad * (m_ViewAngle * 0.5f)));
        Vector3 directionLeft = new Vector3(Mathf.Sin(Mathf.Deg2Rad * -(m_ViewAngle * 0.5f)), 0, Mathf.Cos(Mathf.Deg2Rad * -(m_ViewAngle * 0.5f)));

        Gizmos.DrawLine(transform.position, transform.position + directionRight * ViewDistance);
        Gizmos.DrawLine(transform.position, transform.position + directionLeft * ViewDistance);

        Gizmos.color = Color.magenta;
        if (Player.Instance != null)
		{
            Gizmos.DrawLine(transform.position, Player.Instance.transform.position);
		}

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
