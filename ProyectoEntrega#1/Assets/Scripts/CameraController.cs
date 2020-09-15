using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//para asociar ael objeto a nuestra camara creamos la referencia a la esfera o objeto
		public GameObject jugador;
	//offset controlar la posicion de la camara
		private Vector3 offset ;
	//inicialisaremos la posicion de la camara en el metodo start


	// Use this for initialization
	void Start () {
			   //pos de la camara    //pos de la esfera
		offset=transform.position - jugador.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	//este metodo se ejecuta por cada frame despues del update
	
	void LateUpdate(){
		//se asigna la posicion de la camara
		transform.position= jugador.transform.position +offset;
	}

	//El paso siguiente es darle movimiento el cubo que se creara encima del plano con o,5 de scala y 45 grados
	// y creamos el script
}
