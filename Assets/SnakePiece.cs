using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePiece : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Wall" || other.tag == "Player") {
			
			GameObject.FindObjectOfType<gameManager>().game_over=true;
		}
		else if (other.tag == "food")
		{
			GameObject.FindObjectOfType<gameManager>().EatFood(other.gameObject.transform.position);
			Destroy(other.gameObject);
		}
	}
}
