using UnityEngine;
using System.Collections;

public class marcador3D : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		print (Time.time);
		transform.localScale = transform.localScale + Mathf.Sin(Time.time*2f) * new Vector3(0.05f,0.05f,0.05f);

	}
}
