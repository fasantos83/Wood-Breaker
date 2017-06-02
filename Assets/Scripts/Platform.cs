using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float Speed;
	public float HorizontalLimit;

	// Use this for initialization
	void Start () {
        HorizontalLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - GetComponent<SpriteRenderer>().bounds.extents.x;
    }
	
	// Update is called once per frame
	void Update () {
		float mouseX = Input.GetAxis ("Mouse X");
		transform.position += Vector3.right * mouseX * Speed * Time.deltaTime;
		float currX = Mathf.Clamp (transform.position.x, - HorizontalLimit, HorizontalLimit);
		transform.position = new Vector3 (currX, transform.position.y, transform.position.z);
	}
}
