using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour {
	 
	public float Velocidade = 10;
	Animator animator;
	Rigidbody rb;
	Vector3 direcao;
	public LayerMask MascaraChao;
	public GameObject textoGameOver;
	public bool vivo = true;

	void Start(){
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		Time.timeScale = 1;

	}
	// Update is called once per frame

	
	void Update () {
		float eixoX = Input.GetAxis ("Horizontal");
		float eixoY = Input.GetAxis ("Vertical");
		direcao = new Vector3(eixoX, 0, eixoY);
		
		 

		if (eixoX != 0 || eixoY != 0) {
			animator.SetBool("Movendo",true);
		} else {
			animator.SetBool("Movendo",false);
		}
		if (!vivo && Input.GetButtonDown("Fire1"))
		{
			SceneManager.LoadScene("Game");
			
		}
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + direcao * Time.deltaTime * Velocidade);

		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
		 
		Debug.DrawRay(raio.origin, raio.direction*100,Color.red);
		RaycastHit impacto;

		if (Physics.Raycast(raio,out impacto,100, MascaraChao))
		{
			Vector3 direcaoMira = impacto.point - transform.position;
			direcaoMira.y = transform.position.y;
			Quaternion novaRotacao = Quaternion.LookRotation(direcaoMira);
			
			rb.MoveRotation(Quaternion.Lerp(rb.rotation, novaRotacao, 0.1f));
		}
	}


		 
}
