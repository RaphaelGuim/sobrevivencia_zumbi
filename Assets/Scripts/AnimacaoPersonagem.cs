using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour {

	private Animator animator;

	public void Awake()
	{
		animator = GetComponent<Animator>();
	}
	public void Atacar(bool atacar)
	{
		animator.SetBool(Tags.Atacando, atacar);
	}
	public void Mover(float mover)
	{
		animator.SetFloat(Tags.Movendo, mover);
	}
}
