using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotador2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate ( new Vector3 (45,45,45)* Time.deltaTime);
	}
}
