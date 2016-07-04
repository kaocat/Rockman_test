using UnityEngine;
using System.Collections;

public class shotbase : MonoBehaviour {

    public float speed = 1;

	// Use this for initialization
	void Start () {
		Shot();
		Destroy(this.gameObject , 3.0f);
	}
	// Update is called once per frame
	void Update () {
	
	}

	void Shot() {
		GameObject tmp = this.gameObject;
		Rigidbody tmpphy = tmp.AddComponent<Rigidbody>();
		tmpphy.useGravity = false;
		tmpphy.freezeRotation = true ;
		tmpphy.velocity = transform.right * speed;
		tmp.gameObject.SetActive(true);
	}

	// hit enemy call
	void Hit() {


		Destroy(this.gameObject , 0f);
	}

	void OnTriggerEnter()
	{
		Debug.Log("--------------------------Hit------------------------------");
		Hit();
	}
}
