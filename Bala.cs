using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	private const float tiempo_desplazamiento_disparo = 3.5f;
	private float tiempo_transcurrido_disparo = 0.0f;
	private const float speed = 15.0f;
	public ubicacion ubi = new ubicacion();
	public bool empieza = false;
	private bool define_ubicacion = true;
	public bool dueno; // true: disparado por hero, false: disparado por enemy
	public string id_dueno;
	public GameObject principal;
	public disparado info_disparo;
	private string json_bytes;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(empieza && define_ubicacion){
			define_ubicacion = false;
			if(ubi.direccion){
				transform.Rotate(0, 0, ubi.angulo);
			}else{
				transform.Rotate(0, 0, ubi.angulo+180.0f);
			}
		}
		if(!define_ubicacion){
			tiempo_transcurrido_disparo = tiempo_transcurrido_disparo + Time.deltaTime;
			transform.Translate(Vector3.right * Time.deltaTime*speed);
			if (tiempo_transcurrido_disparo>tiempo_desplazamiento_disparo){
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "enemy"){
			Enemy golpeado = collision.gameObject.GetComponent<Enemy>();
			if(golpeado.id != id_dueno){
				Destroy(gameObject);
				if(dueno){
					Principal info_base = principal.GetComponent<Principal>();
					info_disparo.id_disparado = golpeado.id;
					info_disparo.id_autor = id_dueno;
					json_bytes = JsonUtility.ToJson(info_disparo);
					info_base._websocket.Send(json_bytes);
					Debug.Log("Le disparaste a tu enemigo");
					////////
					// Aquí se notifica al servidor
				}
			}
		}
		if(collision.gameObject.tag == "hero" && !dueno){
			Debug.Log("Te dispararon :'(");
			Destroy(gameObject);
		}
	}
}
