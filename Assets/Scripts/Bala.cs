using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	Rigidbody Rb;
	public float Velocidade;
	float time = 0;
	private void Start()
	{
		Rb = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()	{
		
		Rb.MovePosition((Rb.position + transform.forward * Velocidade * Time.deltaTime));
		time += Time.deltaTime;
		if (time > 3)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Inimigo")
		{
			Destroy(other.gameObject);
			
		}
		Destroy(gameObject);
	}
}
