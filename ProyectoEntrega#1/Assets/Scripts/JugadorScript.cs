using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JugadorScript : MonoBehaviour
{
    private Vector3 posicion;
    //::obtenemos la referencia del Rigidbody
    private Rigidbody rb;
    //PARTICULAS
    public Transform particulas, particulasMalas, particulasWin, particulasWin2;
    private ParticleSystem systemaParticulas, systemaParticulasMalas, systemParticulasWin, systemParticulasWin2;
    private AudioSource audioRecoleccion;
    //5. Creamos variable pa que la esfera se mueva mas rapido
    public float speed;
    // Contador de tiempo
    public Text textoTemporizador, textoContador, textoGanar;
    public Image imgVictoria;
    private float _time = 0.0f;
    private float segundos, minutos;
    // Texto para los puntos
    private int contadorPuntos;

    private float posicionActual;
    //leer entrada desde el teclado
    //1.obtener la posicion de la esfera o objeto nivel horizontal y vertical:

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Object.DontDestroyOnLoad(transform.root.gameObject);
        audioRecoleccion = GetComponent<AudioSource>();
        systemaParticulas = particulas.GetComponent<ParticleSystem>();
        systemaParticulasMalas = particulasMalas.GetComponent<ParticleSystem>();
        systemParticulasWin = particulasWin.GetComponent<ParticleSystem>();
        systemParticulasWin2 = particulasWin2.GetComponent<ParticleSystem>();
        systemaParticulas.Stop();
        systemaParticulasMalas.Stop();
        systemParticulasWin.Stop();
        systemParticulasWin2.Stop();
        textoContador.text = "Contador " + contadorPuntos.ToString();
        textoGanar.text = "";
        posicionActual = transform.position.y;
        imgVictoria.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        minutos = (int)(_time / 60f);
        segundos = (int)(_time % 60f);
        textoTemporizador.text = minutos.ToString("00") + ":" + segundos.ToString("00");
        if (transform.position.y < posicionActual - 5.0f)
        {
            transform.position = new Vector3(-0.75f, 1.05f, -8.76f);
        }
    }

    //2. como queremos mover el objeto o esfera debemos aplicarle una fuerza
    //para eso manipulamos el Rigidbody:
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movimiento * speed);
    }

    /////////////Cuando queremos hacer objetos recolectables
    //other es el parametro de tipo collide con el que choca 
    //esta funcion es declarada automaticamente cuando un objeto choca con otro 
    void OnTriggerEnter(Collider other)
    {
        //identifica el objeto con el cual el otro objeto colisiona other.gameObject
        //el if es para saber si choca con la esfera ya que la esfera le configuraremos el tag Recolectable 
        if (other.gameObject.CompareTag("Recolectable"))
        {
            //particulas
            posicion = other.gameObject.transform.position;
            particulas.position = posicion;
            systemaParticulas = particulas.GetComponent<ParticleSystem>();
            systemaParticulas.Play();

            //desaparecer objeto recolectable
            other.gameObject.SetActive(false);

            //Puntos positivo
            actualizaTiempo(true);
            contadorPuntos++;
            audioRecoleccion.Play();

        }
        else if (other.gameObject.CompareTag("Trampa"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("RecolectableMalo"))
        {
            posicion = other.gameObject.transform.position;
            particulasMalas.position = posicion;
            systemaParticulasMalas = particulasMalas.GetComponent<ParticleSystem>();
            systemaParticulasMalas.Play();
            //desaparecer objeto recolectable
            other.gameObject.SetActive(false);
            //puntos negativos
            actualizaTiempo(false);
            contadorPuntos--;
            audioRecoleccion.Play();
        }
        else if (other.gameObject.CompareTag("Terminado"))
        {
            if (contadorPuntos == 15)
            {
                textoGanar.text = "Ganaste bien jugado" + "Su Puntaje es: " + contadorPuntos.ToString();
            }
            else if (contadorPuntos >= 10)
            {
                textoGanar.text = "Bueno, peor es nada" + "Su Puntaje es: " + contadorPuntos.ToString();
            }
            else if (contadorPuntos <= 9)
            {
                textoGanar.text = "Que perdedor camilo!!!" + "Ni pa que le enseño el puntaje";
            }
            systemParticulasWin.Play();
            systemParticulasWin2.Play();
            imgVictoria.enabled = true;
        }
        textoContador.text = "Contador " + contadorPuntos.ToString();
    }

    void actualizaTiempo(bool recolectablePositivo)
    {
        if (recolectablePositivo)
        {
            if (_time >= 0f)
            {
                _time -= 2.0f;
            }
            else
            {
                _time = 0f;
            }
        }
        else
        {
            _time += 2.0f;

        }
    }
}
