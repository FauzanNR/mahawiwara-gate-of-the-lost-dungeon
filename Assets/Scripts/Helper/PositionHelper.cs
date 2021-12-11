
using UnityEngine;

class PositionHelper {

	public Vector3 generateRandomPosition(Collider ground, float scale, LayerMask layer) {
		Debug.Log( "generateRandomPosition " );

		float moveAreaX = ground.bounds.size.x / 2;
		float moveAreaZ = ground.bounds.size.z / 2;
		float moveAreaY = ground.bounds.size.y / 2;
		Vector3 center = ground.bounds.center;

		var targetCoordsX = center.x + Random.Range( -moveAreaX * scale, moveAreaX * scale );
		var targetCoordsZ = center.z + Random.Range( -moveAreaZ * scale, moveAreaZ * scale );
		var targetCoordsY = center.y + Random.Range( -moveAreaY * scale, moveAreaY * scale );

		var position = new Vector3( targetCoordsX, targetCoordsY, targetCoordsZ );
		var i = 0;
		while(Physics.CheckSphere( position, 1, layer ) && i != 100) {
			i++;
			targetCoordsX = center.x + Random.Range( -moveAreaX * scale, moveAreaX * scale );
			targetCoordsZ = center.z + Random.Range( -moveAreaZ * scale, moveAreaZ * scale );
			position = new Vector3( targetCoordsX, targetCoordsY, targetCoordsZ );
			Debug.Log( "collide" );
		}
		return position;
	}

}
