using UnityEngine;

public class BerkahAlam: MonoBehaviour {
	int cooldown;
	public int damage;
	ParticleSystem particle;


	public void playParticle() {
		particle = GetComponent<ParticleSystem>();
		particle.gameObject.SetActive( true );
		particle.Play();
	}

	private void OnParticleCollision(GameObject other) {
		if(other.transform.root.TryGetComponent( out NPCHealth npc )) {

			npc.getDamage( damage );
		}
	}
}
