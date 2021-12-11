using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelBuilder : MonoBehaviour
{
	public Room startRoomPrefab, endRoomPrefab;
	public List<Room> roomPrefabs = new List<Room>();
	public Vector2 iterationRange = new Vector2(3, 10);
	public GameObject navMeshSurface;

	public GameObject key;
	public GameObject playerPrefab;
	public bool RoomIsDone = false;
	public LayerMask chestAvoidanceLayer;


	private bool _RoomIsDone = false;

	List<Doorway> availableDoorways = new List<Doorway>();

	StartRoom startRoom;
	EndRoom endRoom;
	List<Room> placedRooms = new List<Room>();
	GameObject player;

	LayerMask roomLayerMask;

	void Start()
	{
		roomLayerMask = LayerMask.GetMask("Room");
		StartCoroutine(GenerateLevel());
		_RoomIsDone = false;
	}

	//private void Update() {
	//	if(Input.GetKey( KeyCode.R )) {
	//		ResetLevelGenerator();
	//	}
	//}

	private void Update()
	{
		if(Input.GetKey( KeyCode.R )) {
			ResetLevelGenerator();
		}
		if (_RoomIsDone == true)
		{
			player = Instantiate(playerPrefab) as GameObject;
			player.transform.position = startRoom.transform.position;
			player.transform.rotation = startRoom.transform.rotation;

			//GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
			Debug.Log("ROOM : " + placedRooms.Count);
			int numRoom = Random.Range(placedRooms.Count - 3, placedRooms.Count - 2);

			for (int i = 0; i < placedRooms.Count; i++)
			{
				if (i == numRoom)
				{
					Vector3 posChest = new PositionHelper().generateRandomPosition(
														placedRooms[i].transform.GetChild(0).gameObject.GetComponent<Collider>(),
														 0.5f,
														   chestAvoidanceLayer);

					if (GameObject.FindGameObjectsWithTag("Chest").Length < 1)
					{
						GameObject Key = Instantiate(key, posChest, Quaternion.identity) as GameObject;
						Key.transform.parent = placedRooms[i].transform;
					}
				}
			}


			var navNeshes = navMeshSurface.GetComponents<NavMeshSurface>();
			navNeshes[0].BuildNavMesh();
			navNeshes[1].BuildNavMesh();
			_RoomIsDone = false;
		}

	}

	public IEnumerator GenerateLevel()
	{
		WaitForSeconds startup = new WaitForSeconds(1);
		WaitForFixedUpdate interval = new WaitForFixedUpdate();

		yield return startup;

		// Place start room
		PlaceStartRoom();
		yield return interval;

		// Random iterations
		int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

		for (int i = 0; i < iterations; i++)
		{
			// Place random room from list
			PlaceRoom();
			yield return interval;
		}

		// Place end room
		PlaceEndRoom();
		yield return interval;

		// Level generation finished
		Debug.Log("Level generation finished");

		RoomIsDone = true;
		_RoomIsDone = true;

	}

	void PlaceStartRoom()
	{
		// Instantiate room
		startRoom = Instantiate(startRoomPrefab) as StartRoom;
		startRoom.transform.parent = this.transform;

		// Get doorways from current room and add them randomly to the list of available doorways
		AddDoorwaysToList(startRoom, ref availableDoorways);

		// Position room
		startRoom.transform.position = Vector3.zero;
		startRoom.transform.rotation = Quaternion.identity;
	}

	void AddDoorwaysToList(Room room, ref List<Doorway> list)
	{
		foreach (Doorway doorway in room.doorways)
		{
			int r = Random.Range(0, list.Count);
			list.Insert(r, doorway);
		}
	}

	void PlaceRoom()
	{
		// Instantiate room
		Room currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)]) as Room;
		currentRoom.transform.parent = this.transform;

		// Create doorway lists to loop over
		List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
		List<Doorway> currentRoomDoorways = new List<Doorway>();
		AddDoorwaysToList(currentRoom, ref currentRoomDoorways);

		// Get doorways from current room and add them randomly to the list of available doorways
		AddDoorwaysToList(currentRoom, ref availableDoorways);

		bool roomPlaced = false;

		// Try all available doorways
		foreach (Doorway availableDoorway in allAvailableDoorways)
		{
			// Try all available doorways in current room
			foreach (Doorway currentDoorway in currentRoomDoorways)
			{
				// Position room
				PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);
				Physics.SyncTransforms();    // <- Add new
											 // Check room overlaps
				if (CheckRoomOverlap(currentRoom))
				{
					continue;
				}

				roomPlaced = true;

				// Add room to list
				placedRooms.Add(currentRoom);

				// Remove occupied doorways
				currentDoorway.gameObject.SetActive(false);
				availableDoorways.Remove(currentDoorway);

				availableDoorway.gameObject.SetActive(false);
				availableDoorways.Remove(availableDoorway);

				// Exit loop if room has been placed
				break;
			}

			// Exit loop if room has been placed
			if (roomPlaced)
			{
				break;
			}
		}

		// Room couldn't be placed. Restart generator and try again
		if (!roomPlaced)
		{
			Destroy(currentRoom.gameObject);
			ResetLevelGenerator();
		}
	}

	void PositionRoomAtDoorway(ref Room room, Doorway roomDoorway, Doorway targetDoorway)
	{
		// Reset room position and rotation
		room.transform.position = Vector3.zero;
		room.transform.rotation = Quaternion.identity;

		// Rotate room to match previous doorway orientation
		Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
		Vector3 roomDoorwayEuler = roomDoorway.transform.eulerAngles;
		float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
		Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
		room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180f, 0);

		// Position room
		Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;
		room.transform.position = targetDoorway.transform.position - roomPositionOffset;
	}

	bool CheckRoomOverlap(Room room)
	{
		Bounds bounds = room.RoomBounds;
		bounds.Expand(-1f);

		Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);
		if (colliders.Length > 0)
		{
			// Ignore collisions with current room
			foreach (Collider c in colliders)
			{
				if (c.transform.parent.gameObject.Equals(room.gameObject))
				{
					continue;
				}
				else
				{
					Debug.LogError("Overlap detected");
					return true;
				}
			}
		}

		return false;
	}

	void PlaceEndRoom()
	{
		// Instantiate room
		endRoom = Instantiate(endRoomPrefab) as EndRoom;
		endRoom.transform.parent = this.transform;

		// Create doorway lists to loop over
		List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
		Doorway doorway = endRoom.doorways[0];

		bool roomPlaced = false;

		// Try all available doorways
		foreach (Doorway availableDoorway in allAvailableDoorways)
		{
			// Position room
			Room room = (Room)endRoom;
			PositionRoomAtDoorway(ref room, doorway, availableDoorway);
			Physics.SyncTransforms();
			// Check room overlaps
			if (CheckRoomOverlap(endRoom))
			{
				continue;
			}

			roomPlaced = true;

			// Remove occupied doorways
			doorway.gameObject.SetActive(false);
			availableDoorways.Remove(doorway);

			availableDoorway.gameObject.SetActive(false);
			availableDoorways.Remove(availableDoorway);

			// Exit loop if room has been placed
			break;
		}

		// Room couldn't be placed. Restart generator and try again
		if (!roomPlaced)
		{
			ResetLevelGenerator();
		}
	}

	public void ResetLevelGenerator()
	{
		Debug.LogError("Reset level generator");

		StopCoroutine("GenerateLevel");

		// Delete all rooms
		if (startRoom)
		{
			Destroy(startRoom.gameObject);
		}

		if (endRoom)
		{
			Destroy(endRoom.gameObject);
		}

		foreach (Room room in placedRooms)
		{
			Destroy(room.gameObject);
		}
		Destroy(player);

		// Clear lists
		placedRooms.Clear();
		availableDoorways.Clear();

		RoomIsDone = false;
		_RoomIsDone = false;
		// Reset coroutine
		StartCoroutine("GenerateLevel");
	}
}