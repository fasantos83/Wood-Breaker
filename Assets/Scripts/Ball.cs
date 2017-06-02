using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector3 Direction;
	public float Speed;
	public GameObject BlockParticle;
	public GameObject LeafParticle;

	// Use this for initialization
	void Start () {
		Direction.Normalize ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Direction * Speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collider){
		Vector2 normal = collider.contacts [0].normal;
		Platform platform = collider.transform.GetComponent<Platform> ();
		EdgeCreator edgeCreator = collider.transform.GetComponent<EdgeCreator> ();
		if ((platform != null && normal != Vector2.up) || (edgeCreator != null && normal == Vector2.up)) {
			GameController.Instance.EndGame();
		}else{
			GameObject colliderGO = collider.gameObject;
			if (colliderGO.name.Contains ("Bloco")) {
				GameController.BrokenBlocks++;
				Bounds colliderBounds = collider.transform.GetComponent<SpriteRenderer> ().bounds;
				Vector3 creationPosition = new Vector3 (collider.transform.position.x + colliderBounds.extents.x, collider.transform.position.y, collider.transform.position.z); //+ colliderBounds.extents.y
				GameObject particles = Instantiate (BlockParticle, creationPosition, Quaternion.identity);
				ParticleSystem particleComponent = particles.GetComponent<ParticleSystem> ();
				Destroy (particles, particleComponent.main.duration + particleComponent.main.startLifetimeMultiplier);
				Destroy (colliderGO);
			} else if (colliderGO.name.Contains ("Platform")) {
				Bounds colliderBounds = collider.transform.GetComponent<SpriteRenderer> ().bounds;
				Vector3 creationPosition = new Vector3 (collider.transform.position.x , collider.transform.position.y + colliderBounds.extents.y, collider.transform.position.z);
				GameObject particles = Instantiate (LeafParticle, creationPosition, Quaternion.identity);
				ParticleSystem particleComponent = particles.GetComponent<ParticleSystem> ();
				Destroy (particles, particleComponent.main.duration + particleComponent.main.startLifetimeMultiplier);
			}
			Direction = Vector2.Reflect (Direction, normal);
			Direction.Normalize ();
		}

	}
}
