using UnityEngine;

public class BerkahAlam: MonoBehaviour {

	public string berkahName;
	public int cooldown;
	public int damage;
	bool isParticleOn = false;
	private ParticleSystem particle;


	public void playParticle() {
		this.gameObject.SetActive( true );
		isParticleOn = true;
		particle = GetComponentInChildren<ParticleSystem>();
	}

	void Update() {
		if(isParticleOn == true && particle.isStopped) {
			Destroy( this.gameObject );
		}
	}

}
