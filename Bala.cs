using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	private const float tiempo_desplazamiento_disparo = 3.5f;
	private float tiempo_transcurrido_disparo = 0.0f;
	private const float speed = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		tiempo_transcurrido_disparo = tiempo_transcurrido_disparo + Time.deltaTime;
        transform.Translate(Vector3.right * Time.deltaTime*speed);
        if (tiempo_transcurrido_disparo>tiempo_desplazamiento_disparo){
			Destroy(gameObject);
        }
	}
}
