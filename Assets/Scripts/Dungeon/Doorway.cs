using UnityEngine;

public class Doorway : MonoBehaviour
{
	void OnDrawGizmos()
	{
		Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(ray);
	}
}