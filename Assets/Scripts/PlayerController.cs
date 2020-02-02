using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	bool[] inputs;

	public CharacterState currentState = CharacterState.StandUp;

	public Vector3 grabTilePos;

	public bool moving = false;

	public float moveSpeed = 0f;

	public Animator animator;

	public Ease moveEase;

    private void Start()
    {
		animator = GetComponent<Animator>();
    }

    void Update()
	{
		//inputs[(int)KeyInput.GoRight] = Input.GetKey(KeyCode.RightArrow);
		//inputs[(int)KeyInput.GoLeft] = Input.GetKey(KeyCode.LeftArrow);
		//inputs[(int)KeyInput.GoDown] = Input.GetKey(KeyCode.DownArrow);
		//inputs[(int)KeyInput.Jump] = Input.GetKey(KeyCode.Space);

		CharacterUpdate();

		//Debug.Log(currentState);
    }

	public void CharacterUpdate ()
	{
		switch (currentState)
		{
			case CharacterState.StandUp:

				grabTilePos = new Vector3(transform.position.x, transform.position.y + 1f, 0);

				if (!moving)
				{

					if (Input.GetKey(KeyCode.UpArrow))
					{
						currentState = CharacterState.WalkUp;
					}

					else if (Input.GetKey(KeyCode.DownArrow))
					{
						currentState = CharacterState.WalkDown;
					}

					else if (Input.GetKey(KeyCode.LeftArrow))
					{
						currentState = CharacterState.WalkLeft;
					}

					else if (Input.GetKey(KeyCode.RightArrow))
					{
						currentState = CharacterState.WalkRight;
					}

				}

				break;

			case CharacterState.StandDown:

				grabTilePos = new Vector3(transform.position.x, transform.position.y - 1f, 0);

				if (!moving)
				{
					if (Input.GetKey(KeyCode.UpArrow))
					{
						currentState = CharacterState.WalkUp;
					}

					else if (Input.GetKey(KeyCode.DownArrow))
					{
						currentState = CharacterState.WalkDown;
					}

					else if (Input.GetKey(KeyCode.LeftArrow))
					{
						currentState = CharacterState.WalkLeft;
					}

					else if (Input.GetKey(KeyCode.RightArrow))
					{
						currentState = CharacterState.WalkRight;
					}
				}

				break;

			case CharacterState.StandLeft:

				grabTilePos = new Vector3(transform.position.x - 1f, transform.position.y, 0);

				if (!moving)
				{
					if (Input.GetKey(KeyCode.UpArrow))
					{
						currentState = CharacterState.WalkUp;
					}

					else if (Input.GetKey(KeyCode.DownArrow))
					{
						currentState = CharacterState.WalkDown;
					}

					else if (Input.GetKey(KeyCode.LeftArrow))
					{
						currentState = CharacterState.WalkLeft;
					}

					else if (Input.GetKey(KeyCode.RightArrow))
					{
						currentState = CharacterState.WalkRight;
					}
				}

				break;

			case CharacterState.StandRight:

				grabTilePos = new Vector3(transform.position.x + 1f, transform.position.y, 0);

				if (!moving)
				{
					if (Input.GetKey(KeyCode.UpArrow))
					{
						currentState = CharacterState.WalkUp;
					}

					else if (Input.GetKey(KeyCode.DownArrow))
					{
						currentState = CharacterState.WalkDown;
					}

					else if (Input.GetKey(KeyCode.LeftArrow))
					{
						currentState = CharacterState.WalkLeft;
					}

					else if (Input.GetKey(KeyCode.RightArrow))
					{
						currentState = CharacterState.WalkRight;
					}
				}

				break;

			case CharacterState.WalkUp:

				moving = true;

				transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1f, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);

				animator.SetTrigger("WalkUp");

				currentState = CharacterState.StandUp;

				break;

			case CharacterState.WalkDown:

				moving = true;

				transform.DOMove(new Vector3(transform.position.x, transform.position.y - 1f, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);

				animator.SetTrigger("WalkDown");

				currentState = CharacterState.StandDown;

				break;

			case CharacterState.WalkLeft:

				moving = true;

				transform.DOMove(new Vector3(transform.position.x - 1f, transform.position.y, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);

				animator.SetTrigger("WalkLeft");

				currentState = CharacterState.StandLeft;

				break;

			case CharacterState.WalkRight:

				Debug.Log("Moving right");

				moving = true;

				transform.DOMove(new Vector3(transform.position.x + 1f, transform.position.y, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);

				animator.SetTrigger("WalkRight");

				currentState = CharacterState.StandRight;

				break;
		}
	}

    public void CanMove()
    {
		moving = false;
	}
    
}
