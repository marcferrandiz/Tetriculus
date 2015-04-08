using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){

			print ("Space Clicked!");
			GameObject score = GameObject.Find ("UIText");
			Application.LoadLevel("testingLogic");
			Destroy(score);
		}
	
	}

}
