using UnityEngine;

public class Doorway : MonoBehaviour
{
	void OnDrawGizmos()
	{
		Ray ray = new Ray(transform.position, transform.rotation * Vector3.left);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(ray);
	}
}