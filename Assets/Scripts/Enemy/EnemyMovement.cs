using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float m_LookRadius = 20.0f;
    private Transform m_Target = null;
    NavMeshAgent m_Agent = null;
   

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Target = Player.Instance.GetPlayer().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(m_Target.position, transform.position);
        if (Player.Instance.State == PlayerState.Alive)
        {
            m_Agent.SetDestination(m_Target.position);

            if (distance <= m_Agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 dir = (m_Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

}
