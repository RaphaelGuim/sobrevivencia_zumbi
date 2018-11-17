using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {
	public GameObject BotaoSair;
	public void Jogar()
	{
		StartCoroutine(MudarCena("game"));
		
	}
	public void Sair()
	{

		StartCoroutine(SairJogo());
	}
	IEnumerator SairJogo()
	{
		yield return new WaitForSeconds(0.4f);
		Application.Quit();
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
	IEnumerator MudarCena(string name)
	{
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadScene(name);
	}

	public void Start()
	{
		#if UNITY_STANDALONE || UNITY_EDITOR
			BotaoSair.SetActive(true);
		#endif
	}
}
