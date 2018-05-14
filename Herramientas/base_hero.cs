using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_hero : MonoBehaviour {
	public float thrust = 450;
	public Rigidbody2D rb;
	public GameObject planeta;
	public GameObject principal;
	public GameObject m_camaraPrincipal;
	public bool camJump = false;
	public ubicacion ubi = new ubicacion();
	public string json_bytes;
	public float angulo = 0f;
	public float speed = 10.0f;
	public float x = 0f;
	public float y = 0f;
	public string id;
	// Use this for initialization
}
