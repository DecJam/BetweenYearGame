using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
	public SphereCollider collider;
	private List<IDamageable> Damageables = new List<IDamageable>();
	public float Damage;
	public float AttackDelay = 0.5f;
	public delegate void AttackEvent(IDamageable Target);
	public AttackEvent OnAttack;
	private Coroutine AttackCoroutine;

	private void Awake()
	{
		collider = GetComponent<SphereCollider>();
	}
	private void OnTriggerEnter(Collider other)
	{
		IDamageable damageable = other.GetComponent<IDamageable>();
		if(damageable != null)
		{
			Damageables.Remove(damageable);
			if (Damageables.Count == 0)
			{
				StopCoroutine(AttackCoroutine);
				AttackCoroutine = null;
			}
		}
	}

	private IEnumerator Attack()
	{
		WaitForSeconds wait = new WaitForSeconds(AttackDelay);
		yield return wait;

		IDamageable closestDamageable = null;
		float closestDistance = float.MaxValue;
		while(Damageables.Count > 0)
		{
			for(int i = 0; i < Damageables.Count; i++)
			{
				Transform damageableTransform = Damageables[i].GetTransform();
				float distance = Vector3.Distance(transform.position, damageableTransform.position);

				if(distance < closestDistance)
				{
					closestDistance = distance;
					closestDamageable = Damageables[i];
				}
			}

			if(closestDamageable != null)
			{
				OnAttack?.Invoke(closestDamageable);
				closestDamageable.TakeDamage(Damage);
			}

			closestDamageable = null;
			closestDistance = float.MaxValue;

			yield return wait;

			Damageables.RemoveAll(DisabledDamageable);
		}

		AttackCoroutine = null;

	}

	private bool DisabledDamageable(IDamageable Damageable)
	{
		return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;	
	}
}
