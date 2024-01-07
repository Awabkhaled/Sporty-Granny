using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject snake_piece;
	public GameObject food;
    bool growSnake=false;
	int starting_count = 30;
    List<Vector3> positions = new List<Vector3>();
    static float elapse = .1f;
	List<GameObject> snake = new List<GameObject>();
    Vector3 direction = new Vector3(0, 0, elapse);
    bool is_locked = false;
    public bool game_over = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < starting_count; i++)
        {
            positions.Add(new Vector3( 0, 0, (i - starting_count)*elapse));
            GameObject new_snake_piece = Instantiate(snake_piece);
            new_snake_piece.transform.position = positions[i];
            if (i == starting_count - 1)
            {
                new_snake_piece.AddComponent<SnakePiece>();
            }
            if(i>=starting_count-30)
            {
                new_snake_piece.tag = "Untagged";

            }
            snake.Add(new_snake_piece);
        }
		

        StartCoroutine(MoveSnake());
		StartCoroutine(CreateFood());
	}

    // Update is called once per frame
    void Update()
    {

        if (is_locked == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && direction.z == 0) { direction = new Vector3(0, 0, elapse); is_locked = true; }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && direction.z == 0) { direction = new Vector3(0, 0, -elapse); is_locked = true; }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction.x == 0) { direction = new Vector3(-elapse, 0, 0); is_locked = true; }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && direction.x == 0) { direction = new Vector3(elapse, 0, 0); is_locked = true; }
        }


    }
    IEnumerator MoveSnake()
    {
        yield return new WaitForSeconds(0.02f);
		if (game_over) yield break;

        Vector3 lastPosition = positions[0];
        is_locked = false;
        positions.RemoveAt(0);
        positions.Add(positions[positions.Count - 1] + direction);

        for (int i = 0; i < positions.Count; i++)
        {
            snake[i].transform.position = positions[i];
        }

        if (growSnake)
        {
            
            positions.Insert(0, lastPosition);
			GameObject new_snake_piece = Instantiate(snake_piece);
			new_snake_piece.transform.position = positions[0];
            snake.Insert(0,new_snake_piece);
            growSnake = false;
		}
        StartCoroutine(MoveSnake());
    }

    IEnumerator CreateFood()
    {
        yield return new WaitForSeconds(3f);
        bool valid = true;
        int x, z;
        do
        {
            valid = true;
            x = Random.Range(-17, 17);
            z = Random.Range(-14, 14);
            for(int i = 0; i < positions.Count; i++)
            {
                if (positions[i].x==x && positions[i].z==z) {  valid = false; break; }
            }
        }
        while(!valid);

        GameObject new_food = Instantiate(food);
        new_food.transform.position = new Vector3(x, 0, z);

		StartCoroutine(CreateFood());
	}

    public void EatFood(Vector3 food_position)
    {
        growSnake = true;
		
	}
}
