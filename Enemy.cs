using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float thrust = 450;
	public Rigidbody2D rb;
	public GameObject planeta;
	public GameObject principal;
	public float x = 0f;
	public float y = 0f;
	public string id;
	public ubicacion ubi = new ubicacion();
	private ubicacion ubicacion_paso = new ubicacion();
	private float delta_angulo;
	private float delta_magnitud;
	private float tiempo_paso = 0.0f;
	private int cuenta = 0;
	public GameObject m_enemy;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		principal = GameObject.Find("Principal");
	}
	
	// Update is called once per frame
	void Update () {
		tiempo_paso = tiempo_paso + Time.deltaTime;
		Principal info_base = principal.GetComponent<Principal>();

		if(ubi != info_base.heros[id] && info_base.juego_iniciado){
			ubicacion_paso = ubi;
			delta_angulo = (ubi.angulo - info_base.heros[id].angulo)*0.25f;
			delta_magnitud = (ubi.magnitud - info_base.heros[id].magnitud)*0.25f;
			ubi = info_base.heros[id];
			cuenta = 0;
		}

		if(tiempo_paso > 0.025f && info_base.juego_iniciado && cuenta < 3){
			ubicacion_paso.angulo = ubicacion_paso.angulo - delta_angulo;
			ubicacion_paso.magnitud = ubicacion_paso.magnitud - delta_magnitud;
			tiempo_paso = 0.0f;
			cuenta = cuenta + 1;
		}

		if(cuenta == 4 && info_base.juego_iniciado){
			ubicacion_paso = info_base.heros[id];
		}

		if(info_base.juego_iniciado){
			y = Mathf.Cos(ubicacion_paso.angulo*Mathf.PI/180) * ubicacion_paso.magnitud;
			x = -Mathf.Sin(ubicacion_paso.angulo*Mathf.PI/180) * ubicacion_paso.magnitud;
			transform.position = new Vector3(x, y, -0.5f);
			if(delta_angulo < 0)transform.localScale = new Vector3(-5.0f, 5.0f, 1.0f);
			if(delta_angulo > 0)transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
		}

		float angle = (planeta.transform.position.x < transform.position.x)?
					360 - Vector3.Angle(Vector2.up, transform.position):
					Vector3.Angle(Vector2.up, transform.position);
		transform.eulerAngles = new Vector3(0, 0, angle);
	}
}
