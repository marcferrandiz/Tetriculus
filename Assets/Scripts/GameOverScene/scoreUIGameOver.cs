using UnityEngine;
using System.Collections;

public class scoreUIGameOver : MonoBehaviour {

	private GameObject score;

	// Use this for initialization
	void Start () {
	
		score = GameObject.Find ("UIText");
		score.transform.position = new Vector3 (this.gameObject.transform.position.x + 6f,this.gameObject.transform.position.y - 6f,this.gameObject.transform.position.z + 3f);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
