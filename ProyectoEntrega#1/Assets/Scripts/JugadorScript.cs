using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorScript : MonoBehaviour {

	//PARTICULAS
	public Transform particulas;
	public Transform particulasMalas;
	private ParticleSystem systemaParticulas;
	private ParticleSystem systemaParticulasMalas;
	private AudioSource audioRecoleccion;

	private Vector3 posicion;
	//5. Creamos variable pa que la esfera se mueva mas rapido
	public int speed;
	private int cambio=0;
	private int PuntajeTotal=global.getColision();

	//leer entrada desde el teclado
	//1.obtener la posicion de la esfera o objeto nivel horizontal y vertical:

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical   = Input.GetAxis ("Vertical");

		//3.una vez obtenida la referencia aplicaremos una fuerza con AddForce
		//recibe como parametro vector 3 posiciones donde se aplicara la fuerza a la esfera
		Vector3 movimiento = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//4.ahora generamos la fuerza
		rb.AddForce(movimiento*speed);
		//finalmente podemos controlar la esfera con ñas flechas 

		//5. crear script para asociar la camara; 
	}
	//2. como queremos mover el objeto o esfera debemos aplicarle una fuerza
	//para eso manipulamos el Rigidbody:

	//::obtenemos la referencia del Rigidbody
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
	 	Object.DontDestroyOnLoad(transform.root.gameObject);
		rb=GetComponent<Rigidbody> ();
		audioRecoleccion=GetComponent<AudioSource>();
		systemaParticulas=particulas.GetComponent<ParticleSystem> ();
		systemaParticulasMalas=particulasMalas.GetComponent<ParticleSystem>();
		systemaParticulas.Stop();
		systemaParticulasMalas.Stop();
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	/////////////Cuando queremos hacer objetos recolectables
	//other es el parametro de tipo collide con el que choca 
	//esta funcion es declarada automaticamente cuando un objeto choca con otro 
	void OnTriggerEnter (Collider other){
		//identifica el objeto con el cual el otro objeto colisiona other.gameObject
		//el if es para saber si choca con la esfera ya que la esfera le configuraremos el tag Recolectable 
		if(other.gameObject.CompareTag ("Recolectable")){
			//particulas
			posicion= other.gameObject.transform.position;
			particulas.position = posicion;
			systemaParticulas=particulas.GetComponent<ParticleSystem> ();
			systemaParticulas.Play();
			//desaparecer objeto recolectable
			other.gameObject.SetActive(false);
			cambio++;
			//Puntos positivos

			PuntajeTotal++;
			global.setColision(PuntajeTotal);
			Debug.Log("su puntaje es " + PuntajeTotal);
			audioRecoleccion.Play();

		}else if(other.gameObject.CompareTag ("Trampa")){
				other.gameObject.SetActive(false);
		}else if(other.gameObject.CompareTag ("RecolectableMalo")){
			
			posicion= other.gameObject.transform.position;
			particulasMalas.position = posicion;
			systemaParticulasMalas=particulasMalas.GetComponent<ParticleSystem> ();
			systemaParticulasMalas.Play();
			//desaparecer objeto recolectable
			other.gameObject.SetActive(false);
			//puntos negativos
			PuntajeTotal--;
			Debug.Log("su puntaje es " + PuntajeTotal);
			audioRecoleccion.Play();
		}

		if(cambio==7){
				global.setColision(0);
				 if(PuntajeTotal==7){
			    	Debug.Log("Felicidades tu puntuacion fue la maxima " + PuntajeTotal + " Te llevas Oro");
					 		}else if(PuntajeTotal>=4){
		 					Debug.Log(":( Tu puntacion fue mas de la mitad " + PuntajeTotal + " Te llevas Plata");
		 				}else if(PuntajeTotal>=1){
		 						Debug.Log(":( Tu puntacion fue muy mala " + PuntajeTotal + " Te llevas Bronce");
		 			}else{
		 		Debug.Log(":( Tu puntacion es horrible retirate de este juego " + PuntajeTotal + " Te llevas Nada debes pagar");
				}
			if(SceneManager.GetActiveScene().name=="Scense1"){
				SceneManager.LoadScene(1);
				Debug.Log("Bienvenido a la segunda escena");
			}else{
				Debug.Log("Fin de juego");
				SceneManager.LoadScene(2);

			}	
		}
	}
}
