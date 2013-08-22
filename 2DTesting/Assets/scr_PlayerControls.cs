using UnityEngine;
using System.Collections;

public class scr_PlayerControls : MonoBehaviour {
	private enum FacingDirectionType {
		Up, Down, Left, Right
	}
	
	private float walkingSpeed = 20.0f;
	
	private FacingDirectionType facingDirection;

	// Use this for initialization
	void Start () {
		facingDirection = FacingDirectionType.Left;
		
		StartCoroutine(MoveATile(0, 1.250f));
	}
	
	// Update is called once per frame
	void Update () {
		GetDirectionInput();
		
		
	}
	
	void GetDirectionInput() {
		if (Input.GetKey(KeyCode.W)) {
			Debug.Log("Up");
			facingDirection = FacingDirectionType.Up;
		}
		else if (Input.GetKey(KeyCode.S)) {
			Debug.Log("Down");
			facingDirection = FacingDirectionType.Down;
		}
		else if (Input.GetKey(KeyCode.A)) {
			Debug.Log("Left");
			facingDirection = FacingDirectionType.Left;
		}
		else if (Input.GetKey(KeyCode.D)) {
			Debug.Log("Right");
			facingDirection = FacingDirectionType.Right;
		}
	}
	
	IEnumerator MoveATile(int direction, float speed) {
		// Convert the speed to time, based on a forced FPS of 60.
		speed = speed/60.0f;
		// Find how many pixels to move each tick. (speed 0-60 = 1, 61-120 = 2, etc.)
		float speedInPixels = Mathf.Floor(speed) + 1;
		
		// Start the move loop.
		while (true) {
			// Wait for the tick to happen.
			yield return new WaitForSeconds((1/60.0f)/speed); // 60 fps.
			//Move the required number of pixels.
			transform.Translate(Vector3.left * speedInPixels); // * speed);
		}
	}
}
