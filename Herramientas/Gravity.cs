using UnityEngine;

namespace _Scripts
{
	public class Gravity : MonoBehaviour
	{
		public float PullRadius;
		public float GravitationalPull;
		public float MinRadius;
		public float DistanceMultiplier;

		public LayerMask LayersToPull;

		void FixedUpdate()
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, PullRadius, LayersToPull);
			foreach (var collider in colliders)
			{
				Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
				
				if (rb == null) continue;

				Vector3 direction = transform.position - collider.transform.position;

				if (direction.magnitude < MinRadius) continue;

				float distance = direction.sqrMagnitude*DistanceMultiplier + 1;
				rb.AddForce(direction.normalized * (GravitationalPull / distance) * rb.mass * Time.fixedDeltaTime);
			}
		}

	}
}