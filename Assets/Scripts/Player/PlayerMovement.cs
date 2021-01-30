using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 5;
	public float rotateSpeed = 5;
	public Transform modelTransform;
	private Rigidbody _rb;
	private Vector2 _dir = Vector2.zero;
	private PlayerInputs _inputs;
	private Vector3 currentPos, previousPos;

	void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_inputs = new PlayerInputs();
		HandleActions();
		currentPos = transform.position;
	}

	void HandleActions()
	{
		_inputs.Game.Movement.performed += ctx => _dir = ctx.ReadValue<Vector2>();
		_inputs.Game.Movement.canceled += ctx => _dir = Vector2.zero;
	}

	void OnEnable() => _inputs.Enable();
	void OnDisable() => _inputs.Disable();

	// Start is called before the first frame update
	void Update()
	{
		HandleRotation();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		previousPos = currentPos;
		currentPos = transform.position;
		var newDir = new Vector3(_dir.x, 0, _dir.y);
		_rb.MovePosition(_rb.position + newDir * speed);
	}

	void HandleRotation()
	{
		if(_dir == Vector2.zero) return;
		var newRot = Quaternion.Normalize(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentPos - previousPos), Time.fixedDeltaTime * rotateSpeed));
		newRot.x = 0;
		newRot.z = 0;
		transform.rotation = newRot;
	}
}
