using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

	public GameObject[] Blocks;
	public int Rows;

	// Use this for initialization
	void Start () {
		CreateBlockGroup ();
	}

	void CreateBlockGroup(){
		Bounds bounds = Blocks[0].GetComponent<SpriteRenderer> ().bounds;
		float blockWidth = bounds.size.x;
		float blockHeight = bounds.size.y;
		float screenWidth, screenHeight, widthMultiplier;
		int columns;
		GetBlockInformation (blockWidth, out screenWidth, out screenHeight, out columns, out widthMultiplier);
		GameController.TotalBlocks = Rows * columns;
		for(int i = 0; i < Rows; i++){
			for(int j = 0; j < columns; j++){
				GameObject randomBlock = Blocks[Random.Range (0, Blocks.Length)];
				GameObject block = Instantiate (randomBlock);
				block.transform.position = new Vector3 (-(screenWidth * 0.5f) + (j * blockWidth * widthMultiplier), (screenHeight * 0.5f) - (i * blockHeight), 0);
				block.transform.localScale = new Vector3 (block.transform.localScale.x * widthMultiplier, block.transform.localScale.y, 1);
			}
		}
	}

	void GetBlockInformation(float blockWidth, out float screenWidth, out float screenHeight, out int columns, out float widthMultiplier){
		Camera cam = Camera.main;
		screenWidth = (cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) - cam.ScreenToWorldPoint(new Vector3(0, 0, 0))).x;
		screenHeight = (cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)) - cam.ScreenToWorldPoint(new Vector3(0, 0, 0))).y;
		columns = (int)(screenWidth / blockWidth);
		widthMultiplier = screenWidth / (columns * blockWidth);
	}
}
