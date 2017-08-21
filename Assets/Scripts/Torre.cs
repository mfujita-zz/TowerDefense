using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Torre : MonoBehaviour {
	public GameObject projetilPrefab;
	public float tempoDeRecarga = 1f;
	private float momentoDoUltimoDisparo;

	[SerializeField] private float raioDeAlcance;

	void Update () {
		Inimigo alvo = EscolheAlvo ();
		if (alvo != null) {
			Atira (alvo);
		}
	}
	
	private void Atira(Inimigo inimigo){
		float tempoAtual = Time.time;
		if (tempoAtual > momentoDoUltimoDisparo + tempoDeRecarga) 
		{
			momentoDoUltimoDisparo = tempoAtual;
			GameObject pontoDeDisparo = this.transform.Find ("CanhaoDaTorre/PontoDeDisparo").gameObject;
			Vector3 posicaoDoPontoDeDisparo = pontoDeDisparo.transform.position;

			GameObject projetilObject = (GameObject) Instantiate (projetilPrefab, posicaoDoPontoDeDisparo, Quaternion.identity);
			Missil missil = projetilObject.GetComponent<Missil>();
			missil.DefineAlvo(inimigo);
		}
	}

	private bool EstaNoRaioDeAlcance(GameObject inimigo) {
		Vector3 posicaoDoInimigoNoPlano = Vector3.ProjectOnPlane(inimigo.transform.position, Vector3.up);
		Vector3 posicaoDaTorreNoPlano = Vector3.ProjectOnPlane(this.transform.position, Vector3.up);
		float distanciaParaInimigo = Vector3.Distance (posicaoDaTorreNoPlano, posicaoDoInimigoNoPlano);
		return distanciaParaInimigo <= raioDeAlcance;
	}

	private Inimigo EscolheAlvo()
	{
		GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
		foreach (GameObject inimigo in inimigos) {
			if (EstaNoRaioDeAlcance(inimigo)){
				return inimigo.GetComponent<Inimigo>();
			}
		}
		return null;
	}
}
