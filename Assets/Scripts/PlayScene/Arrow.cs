using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public string PositionArrow;


	// Use this for initialization
	void Start () {
	
		switch(PositionArrow){
			case "top":
				print ("Top");
				gameObject.transform.position = new Vector3(0f,5.0f,0.0f);
				gameObject.transform.eulerAngles = new Vector3(270f,0f,0f);
			break;
			case "down":
				print ("Down");
				gameObject.transform.position = new Vector3(3.22f,-4.5f,0.0f);
				gameObject.transform.eulerAngles = new Vector3(90f,180f,0f);
				break;
			case "right":
				print ("Right");
				gameObject.transform.position = new Vector3(14.22f,3.0f,0.0f);
				gameObject.transform.eulerAngles = new Vector3(0f,90f,270f);
				break;
			case "left":
				print ("Left");
				gameObject.transform.position = new Vector3(-7.22f,3.0f,0.0f);
				break;

		}
	}
	
	// Update is called once per frame
	void Update () {

		// Move every object res
		switch(PositionArrow){
		case "top":
			//transform.Translate(Vector3.forward * Time.deltaTime);
			break;
		case "down":

			break;
		case "right":

			break;
		case "left":

			break;
			
		}

	


	}
}
