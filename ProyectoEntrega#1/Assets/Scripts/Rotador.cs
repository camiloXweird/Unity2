using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotador : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			//rota el objeto por medio de transforem la cantidad de grados que indica
			transform.Rotate ( new Vector3 (45,45,45)* Time.deltaTime);
			//* se multiplica para permitir que la rotacion se haga en los grados dados

	}
}
