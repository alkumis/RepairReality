using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	bool[] inputs;

	public CharacterState currentState = CharacterState.StandUp;

	public CharacterState prevState = CharacterState.StandUp;

	public Vector3 grabTilePos;

	public bool moving = false;

	public float moveSpeed = 0f;

	public Animator animator;

	public Ease moveEase;

	public float bumpDur = 0f;

	public float bumpStr = 0f;

	public int bumpVib = 0;

	public float bumpRand = 0f;

	public float bumpElast = 1f;

	public bool bumpSnapping = false;

	public bool bumpFade = true;

	public bool pathExists = false;

	public GameObject grabTile = null;

	public Tilemap brokenReality;

	public Tilemap environment;

	public Tilemap obstruction;

	public Tilemap passable;

    private void Start()
    {
		animator = GetComponent<Animator>();
		//TileBase tempTile = brokenReality.GetTile(new Vector3Int(0,0,0));
		//brokenReality.SetTile(new Vector3Int(0, 0, 0), null);
		//CheckForPath();
	}

    void Update()
	{
		CharacterUpdate();
    }

	public void CharacterUpdate ()
	{
		switch (currentState)
		{
			case CharacterState.StandUp:

				prevState = CharacterState.StandUp;
				grabTile.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, 0);

				CheckForPath();

				if (!moving)
				{
					animator.Play("StandUp");

					if (Input.GetKeyDown(KeyCode.UpArrow))
					{
                        if(pathExists)
						    currentState = CharacterState.WalkUp;

						else if (!pathExists)
							currentState = CharacterState.Bump;
					}

					else if (Input.GetKeyDown(KeyCode.DownArrow))
					{
						currentState = CharacterState.StandDown;
					}

					else if (Input.GetKeyDown(KeyCode.LeftArrow))
					{
						currentState = CharacterState.StandLeft;
					}

					else if (Input.GetKeyDown(KeyCode.RightArrow))
					{
						currentState = CharacterState.StandRight;
					}
				}

				break;

			case CharacterState.StandDown:

				prevState = CharacterState.StandDown;
				grabTile.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);

				CheckForPath();

				if (!moving)
				{
					animator.Play("StandDown");

					if (Input.GetKeyDown(KeyCode.UpArrow))
					{
						currentState = CharacterState.StandUp;
					}

					else if (Input.GetKeyDown(KeyCode.DownArrow))
					{
                        if(pathExists)
						    currentState = CharacterState.WalkDown;

						else if (!pathExists)
							currentState = CharacterState.Bump;
					}

					else if (Input.GetKeyDown(KeyCode.LeftArrow))
					{
						currentState = CharacterState.StandLeft;
					}

					else if (Input.GetKeyDown(KeyCode.RightArrow))
					{
						currentState = CharacterState.StandRight;
					}
				}

				break;

			case CharacterState.StandLeft:

				prevState = CharacterState.StandLeft;
				grabTile.transform.position = new Vector3(transform.position.x - 1f, transform.position.y, 0);

				CheckForPath();

				if (!moving)
				{
					animator.Play("StandLeft");

					if (Input.GetKeyDown(KeyCode.UpArrow))
					{
						currentState = CharacterState.StandUp;
					}

					else if (Input.GetKeyDown(KeyCode.DownArrow))
					{
						currentState = CharacterState.StandDown;
					}

					else if (Input.GetKeyDown(KeyCode.LeftArrow))
					{
                        if(pathExists)
						    currentState = CharacterState.WalkLeft;

						else if (!pathExists)
							currentState = CharacterState.Bump;
					}

					else if (Input.GetKeyDown(KeyCode.RightArrow))
					{
						currentState = CharacterState.StandRight;
					}
				}

				break;

			case CharacterState.StandRight:

				prevState = CharacterState.StandRight;
				grabTile.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, 0);

				CheckForPath();

				if (!moving)
				{
					animator.Play("StandRight");

					if (Input.GetKeyDown(KeyCode.UpArrow))
					{
						currentState = CharacterState.StandUp;
					}

					else if (Input.GetKeyDown(KeyCode.DownArrow))
					{
						currentState = CharacterState.StandDown;
					}

					else if (Input.GetKeyDown(KeyCode.LeftArrow))
					{
						currentState = CharacterState.StandLeft;
					}

					else if (Input.GetKeyDown(KeyCode.RightArrow))
					{
                        if(pathExists)
						    currentState = CharacterState.WalkRight;

						else if (!pathExists)
							currentState = CharacterState.Bump;
					}
				}

				break;

			case CharacterState.WalkUp:

				prevState = CharacterState.WalkUp;
				moving = true;
				transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1f, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);
				grabTile.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 2f, 0), moveSpeed).SetEase(moveEase);
				animator.SetTrigger("WalkUp");
				currentState = CharacterState.StandUp;

				break;

			case CharacterState.WalkDown:

				prevState = CharacterState.WalkDown;
				moving = true;
				transform.DOMove(new Vector3(transform.position.x, transform.position.y - 1f, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);
                grabTile.transform.DOMove(new Vector3(transform.position.x, transform.position.y - 2f, 0), moveSpeed).SetEase(moveEase);
				animator.SetTrigger("WalkDown");
				currentState = CharacterState.StandDown;

				break;

			case CharacterState.WalkLeft:

				prevState = CharacterState.WalkLeft;
				moving = true;
				transform.DOMove(new Vector3(transform.position.x - 1f, transform.position.y, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);
                grabTile.transform.DOMove(new Vector3(transform.position.x - 2f, transform.position.y, 0), moveSpeed).SetEase(moveEase);
				animator.SetTrigger("WalkLeft");
				currentState = CharacterState.StandLeft;

				break;

			case CharacterState.WalkRight:

				prevState = CharacterState.WalkRight;
				moving = true;
				transform.DOMove(new Vector3(transform.position.x + 1f, transform.position.y, 0), moveSpeed).OnComplete(CanMove).SetEase(moveEase);
                grabTile.transform.DOMove(new Vector3(transform.position.x + 2f, transform.position.y, 0), moveSpeed).SetEase(moveEase);
				animator.SetTrigger("WalkRight");
				currentState = CharacterState.StandRight;

				break;

			case CharacterState.Bump:

				moving = true;
				//transform.DOShakePosition(bumpDur, bumpStr, bumpVib, bumpRand, bumpSnapping, bumpFade).OnComplete(CanMove);

				if (prevState == CharacterState.StandUp)
					transform.DOPunchPosition(transform.up * bumpStr, bumpDur, bumpVib, bumpElast, bumpSnapping).OnComplete(CanMove);

				else if (prevState == CharacterState.StandDown)
					transform.DOPunchPosition(-transform.up * bumpStr, bumpDur, bumpVib, bumpElast, bumpSnapping).OnComplete(CanMove);

				else if (prevState == CharacterState.StandLeft)
					transform.DOPunchPosition(-transform.right * bumpStr, bumpDur, bumpVib, bumpElast, bumpSnapping).OnComplete(CanMove);

				else if (prevState == CharacterState.StandDown)
					transform.DOPunchPosition(transform.right * bumpStr, bumpDur, bumpVib, bumpElast, bumpSnapping).OnComplete(CanMove);

				currentState = prevState;

                break;
		}
	}

    public void CanMove()
    {
		moving = false;
	}

    public void CheckForPath()
    {
		if (!environment.HasTile(new Vector3Int((int)(grabTile.transform.position.x - 0.5f), (int)(grabTile.transform.position.y - 0.5f), 0)) && !passable.HasTile(new Vector3Int((int)(grabTile.transform.position.x - 0.5f), (int)(grabTile.transform.position.y - 0.5f), 0)))
		{
			grabTile.GetComponent<SpriteRenderer>().color = Color.red;
			pathExists = false;
		}

		else
		{
			grabTile.GetComponent<SpriteRenderer>().color = Color.white;
			pathExists = true;
		}
	}
}
