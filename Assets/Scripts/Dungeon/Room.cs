using UnityEngine;

public class Room : MonoBehaviour
{
	public Doorway[] doorways;
	public MeshCollider meshCollider;

	public float minX, maxX, minZ, maxZ;

	public Bounds RoomBounds
	{
		get { return meshCollider.bounds; }
	}
}