using UnityEngine;
using System.Collections;

public class scr_PlayerControls : MonoBehaviour {
	private enum FacingDirectionType {
		Up, Down, Left, Right
	}
	
	public float walkingSpeed = 30.0f;

    private OTAnimatingSprite sprite;
	private FacingDirectionType facingDirection;
    private bool isMoving = false;
    private bool rightFootFirst = true;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<OTAnimatingSprite>();
		facingDirection = FacingDirectionType.Left;
		
	}
	
	// Update is called once per frame
	void Update () {
		GetDirectionInput();

	}
	
	void GetDirectionInput() {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //Debug.Log("Direction Input: Up");
                facingDirection = FacingDirectionType.Up;
                StartCoroutine(MoveATile(FacingDirectionType.Up, walkingSpeed));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                //Debug.Log("Direction Input: Down");
                facingDirection = FacingDirectionType.Down;
                StartCoroutine(MoveATile(FacingDirectionType.Down, walkingSpeed));
            }
            else if (Input.GetKey(KeyCode.A))
            {
               // Debug.Log("Direction Input: Left");
                facingDirection = FacingDirectionType.Left;
                StartCoroutine(MoveATile(FacingDirectionType.Left, walkingSpeed));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //Debug.Log("Direction Input: Right");
                facingDirection = FacingDirectionType.Right;
                StartCoroutine(MoveATile(FacingDirectionType.Right, walkingSpeed));
            }
        }
	}
	
	IEnumerator MoveATile(FacingDirectionType direction, float speed) {
        // Keep track of whether we're moving or not.
        isMoving = true;

		// Convert the speed to time, based on a forced FPS of 60.
		speed = speed/60.0f;
		// Find how many pixels to move each tick. (speed 0-60 = 1, 61-120 = 2, etc.)
		float speedInPixels = Mathf.Floor(speed) + 1;

        // Set the movement vector.
        Vector3 moveVector = new Vector3();

        rightFootFirst = !rightFootFirst;
        switch (direction)
        {
            case (FacingDirectionType.Up) :
                moveVector = Vector3.up;
                if (rightFootFirst)
                {
                    sprite.PlayOnce("walkUp_Right");
                }
                else
                {
                    sprite.PlayOnce("walkUp_Left");
                }
                break;
            case (FacingDirectionType.Down):
                moveVector = Vector3.down;
                if (rightFootFirst)
                {
                    sprite.PlayOnce("walkDown_Right");
                }
                else
                {
                    sprite.PlayOnce("walkDown_Left");
                }
                break;
            case (FacingDirectionType.Left):
                moveVector = Vector3.left;
                if (rightFootFirst)
                {
                    sprite.PlayOnce("walkLeft_Right");
                }
                else
                {
                    sprite.PlayOnce("walkLeft_Left");
                }
                break;
            case (FacingDirectionType.Right):
                moveVector = Vector3.right;
                if (rightFootFirst)
                {
                    sprite.PlayOnce("walkRight_Right");
                }
                else
                {
                    sprite.PlayOnce("walkRight_Left");
                }
                break;
        }
        // Save the current position, so we know when to stop.
        Vector3 originalPosition = transform.position;
		
		// Start the move loop.
		while (true) {
            if (Mathf.Abs(transform.position.x - originalPosition.x) < 16.0f &&
                Mathf.Abs(transform.position.y - originalPosition.y) < 16.0f)
            {
                //Move the required number of pixels.
                transform.Translate(moveVector * speedInPixels);
            }
            else
            {
                isMoving = false;
                break;
            }
            // Wait for the tick to happen.
            yield return new WaitForSeconds((1 / 60.0f)); // / speed); // 60 fps.
		}
	}
}
