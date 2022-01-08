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
			Destroy( this.gameObject );
		}
	}

}
