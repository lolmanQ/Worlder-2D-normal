using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController player;
	public PlayerSettings playerSettings;
	public bool onGround;
	public LayerMask groundLayer;
	public GameObject modelObj;
	float gravity;
	public int airJumpsLeft;
	Rigidbody rb;
	bool isRuning, dashIsReady, facingRight = true, hasControl = true, physicsActive = true, isGlide;
	float dashTimer;

	/// <summary>
	/// istället för att det ska stå
	/// public Transform respawnPoint;
	/// ska det stå
	/// [SerializeField]
	/// private Transform respawnPoint;
	/// </summary>

	[SerializeField]
	private Transform respawnPoint;

	private Vector3 velocity = Vector3.zero;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		gravity = playerSettings.gravity;
		isRuning = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (physicsActive)
		{
			DoPhysics();
		}

		isGlide = false;
		// && dashTimer <= 0
		if (!dashIsReady && onGround)
		{
			dashIsReady = true;
		}
		else if(!dashIsReady)
		{
			dashTimer -= Time.deltaTime;
		}

		if (hasControl)
		{

			if (Input.GetKey(KeyCode.D))
			{
				//rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (playerSettings.speed * Time.deltaTime), -playerSettings.maxSpeed, playerSettings.maxSpeed), rb.velocity.y, 0);
				Move(playerSettings.speed);
			}
			if (Input.GetKey(KeyCode.A))
			{
				//rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x - (playerSettings.speed * Time.deltaTime), -playerSettings.maxSpeed, playerSettings.maxSpeed), rb.velocity.y, 0);
				Move(-playerSettings.speed);
			}

			if (Input.GetKeyDown(KeyCode.Space) && airJumpsLeft > 0)
			{
				rb.velocity = new Vector3(rb.velocity.x, 0, 0);
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + playerSettings.jumpSpeed, 0);
				airJumpsLeft -= 1;
				onGround = false;
			}
			else if (onGround)
			{
				
			}

			if (Input.GetKey(KeyCode.LeftShift) && !onGround)
			{
				isGlide = true;
				rb.velocity = new Vector3(rb.velocity.x, -playerSettings.glideSpeed);
			}

			if (Input.GetKeyDown(KeyCode.LeftControl) && dashIsReady)
			{
				dashIsReady = false;
				dashTimer = playerSettings.dashCooldown;
				hasControl = false;
				physicsActive = false;
				Invoke("EndDash", playerSettings.dashLength);
				rb.velocity = new Vector3(0, 0);
				if (facingRight)
				{
					Dash(playerSettings.dashPower);
				}
				else
				{
					Dash(-playerSettings.dashPower);
				}
			}
		}

		
	}

	void Move(float move)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move, rb.velocity.y);
		// And then smoothing it out and applying it to the character
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, playerSettings.moveDamp);
	}

	void Dash(float move)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move, rb.velocity.y);
		// And then smoothing it out and applying it to the character
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.005f);
	}

	private void FixedUpdate()
	{
		if(rb.velocity.x > 0.5)
		{
			facingRight = true;
			modelObj.transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else if(rb.velocity.x < -0.5)
		{
			facingRight = false;
			modelObj.transform.eulerAngles = new Vector3(0, -180, 0);
		}
	}

	void DoPhysics()
	{
		if (!isGlide)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - gravity * Time.deltaTime);
		}
		if (onGround)
		{
			rb.velocity = new Vector3(rb.velocity.x / playerSettings.moveSlow, rb.velocity.y);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Ground"))
		{
			airJumpsLeft = playerSettings.amountOfJumps;
			onGround = true;
		}
		if (collision.gameObject.CompareTag("Damage"))
		{
			GameManager.main.Death();
			Respawn();
		}
	}
	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			onGround = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			onGround = false;
		}
	}

	void EndDash()
	{
		hasControl = true;
		physicsActive = true;
		rb.velocity = new Vector3(0, 0);
	}

	void Respawn()
	{
		transform.position = respawnPoint.position;
		rb.velocity = new Vector3(0, 0);
	}
}
