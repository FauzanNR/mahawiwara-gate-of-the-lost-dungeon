using UnityEngine;

public class BerkahAlam: MonoBehaviour {

	int cooldown;
	public AreaHelper damageArea;
	public int damage;
	bool isParticleOn = false;
	private ParticleSystem particle;


	public void playParticle() {
		this.gameObject.SetActive( true );
		isParticleOn = true;
		particle = GetComponentInChildren<ParticleSystem>();
		particle.Play();
	}

	void Update() {
		if(isParticleOn == true && particle.isStopped) {
			this.gameObject.SetActive( false );
		}

		if(damageArea.isTriggered) {
			print( "Particle Collosion" );
			if(damageArea.colliderObj().TryGetComponent( out NPCHealth npc )) {
				npc.getDamage( damage );
			}
		}
	}

	//private void OnParticleCollision(GameObject other) {
	//	if(other.transform.root.TryGetComponent( out NPCHealth npc )) {
	//		npc.getDamage( damage );
	//		print( "Particle Collosion" );
	//	}
	//}

	//private void OnTriggerEnter(Collider other) {
	//	print( other.gameObject.name );
	//	if(other.TryGetComponent( out NPCHealth npc )) {
	//		npc.getDamage( damage );
	//	}
	//}
}
