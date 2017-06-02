using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static int TotalBlocks;
	public static int BrokenBlocks;
	public Image Stars;
	public GameObject CanvasGO;
	public static GameController Instance;
	public Ball Ball;
	public Platform Platform;

	void Awake(){
		Instance = this;
	}

	void Start(){
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			Platform.enabled = true;
			BrokenBlocks = 0;
			CanvasGO.SetActive (false);
		}
	}

	public void EndGame(){
		Stars.fillAmount = (float)BrokenBlocks / (float)TotalBlocks;
		CanvasGO.SetActive(true);
		Destroy (Ball.gameObject);
		Platform.enabled = false;
	}

	public void ChangeScene(string scene){
		SceneManager.LoadScene (scene);
	}

	public void QuitApplication(){
		Application.Quit();
	}
}
