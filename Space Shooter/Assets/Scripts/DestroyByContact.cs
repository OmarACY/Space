using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExposion;
    public int scoreValue;
    private GameController gameController;

    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null){
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("No se pudo encontrar el controlador del juego!!");
        }
    }

	void OnTriggerEnter(Collider other){

        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")){
			return;
		}

        //Instancia la explision
        if (explosion != null){            
            Instantiate(explosion, transform.position, transform.rotation);
        }

		if (other.CompareTag("Player")) {
			//Instancia la explicion del jugador
			Instantiate (playerExposion, transform.position, transform.rotation);
            gameController.GameOver();
		}
        //Suma al puntaje del jugador
        gameController.AddScore(scoreValue);
		//Destruye el objeto con el que choco el asteroide
		Destroy (other.gameObject);
		//Se autodestruye asi mismo
		Destroy (gameObject);
	}
}
