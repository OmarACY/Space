using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed; //Velovidad de la nave
	public float tilt;// Para la rotacion de la nave
	private Rigidbody rb; //Componente de fisica
	public Boundary boundary; //Demilitador para el area de juego
	public GameObject shot; //Objeto que se va a disparar
	public Transform[] shotSpawns; //Transform del objeto a disparar
    public SimpleTouchPad touchPad;
	public float fireRate;
	private float nextFire;
    //Audio de el disparo
    AudioSource fireSound;
    public float dampingRadius;
    public float velocityLag;
    private Vector3 target;
	void Start(){
		//Inicializa el componente rigidbody
		rb = GetComponent<Rigidbody> ();
        //Inicializa el componente de audio
        fireSound = GetComponent<AudioSource>();
	}

	void Update(){
      /*  bool triggered = false;
        if (Input.mousePresent && Input.GetMouseButton(0))
        {
            triggered = true;
        }
        else if (Input.touchCount > 0)
        {
            triggered = true;
        }
        */
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			//GameObject clone = 
            foreach (var shotSpawn in shotSpawns){
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
            }			
            fireSound.Play();
		}
	}

    void FixedUpdate(){

        /*
          //Mover el jugador con el touchPad
            Vector2 direction = touchPad.GetDirection();
            Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
            rb.velocity = movement * speed; //Fuerza para mover al jugador
         */

        // Metodo para mover con touch al jugador
        Vector3? touchPos = null;
        if (Input.mousePresent && Input.GetMouseButton(0))
        {
            touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        }
        else if (Input.touchCount > 0)
        {
            touchPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0.0f);
        }
        if (touchPos != null)
        {
            target = Camera.main.ScreenToWorldPoint(touchPos.Value);
            target.y = rb.position.y;
        }

        Vector3 offset = target - rb.position;

        float magnitude = offset.magnitude;
        if (magnitude > dampingRadius)
            magnitude = dampingRadius;
        float dampening = magnitude / dampingRadius;

        Vector3 desiredVelocity = offset.normalized * speed * dampening;

        rb.velocity += (desiredVelocity - rb.velocity) * velocityLag;

		//Demilitar el area del jugador
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f,0.0f,rb.velocity.x * -tilt);
	}
}
