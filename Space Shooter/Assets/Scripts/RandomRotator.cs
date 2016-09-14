using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	private Rigidbody rb;
	//Caida de rotacion
	public float tumble;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		//Velocidad angular para la rotacion aleatoria
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
