using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {
	Rigidbody rb;
	public LayerMask MascaraChao;
	 
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Movimentar(Vector3 direcao, float velocidade)	{

		Vector3 posicao = rb.position + direcao.normalized * Time.deltaTime * velocidade;
	 
			
		rb.MovePosition(posicao);
	 
		 
	}

	public void RotacaoJogador()
	{
		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);


		RaycastHit impacto;

		if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
		{
			Vector3 direcaoMira = impacto.point - transform.position;
			direcaoMira.y = transform.position.y;
			Rotacionar(direcaoMira);
			 
		}
	}

	public void Rotacionar(Vector3 direcao)
	{
		if (direcao.magnitude != 0)
		{
			Quaternion novaRotacao = Quaternion.LookRotation(direcao);
			novaRotacao = Quaternion.Lerp(rb.rotation, novaRotacao, 0.1f);
			rb.MoveRotation(novaRotacao);
		}
	
	}
}
