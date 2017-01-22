using UnityEngine;

public class TorpedoProjectile : Projectile {
	// Use this for initialization
	public float forwardForce = 1.0f;
	public float boyancy = 1.0f;
	public float gravity = 1.0f;
	public float maxDegreesCorrection = 1.0f;
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		// trend towards the surface.
		var body = this.GetComponent<Rigidbody>();

		if(this.transform.position.y < 0)
		{
			body.AddForce(Vector3.up * boyancy);
		}
		if(this.transform.position.y > 0)
		{
			body.AddForce(Vector3.down * gravity);
		}


		var targetRotation = Quaternion.LookRotation((this.target.transform.position - this.transform.position));

		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, maxDegreesCorrection);

		body.AddRelativeForce(Vector3.forward * this.forwardForce);

	}
}