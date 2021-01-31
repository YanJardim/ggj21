using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public Transform target;

	public float smoothSpeed = 0.125f;
	public float rotationSpeed = 2;
	public Vector3 offset;
	private PlayerInputs _playerInputs;
	private float rotationDir = 0;
	private float _angle = 0;
	private Rigidbody _rb;
	private Quaternion _rot;

	void Awake()
	{
		_playerInputs = new PlayerInputs();
		_playerInputs.Game.Camera.performed += ctx => rotationDir = ctx.ReadValue<float>();
		_playerInputs.Game.Camera.canceled += ctx => rotationDir = 0;
		_rb = GetComponent<Rigidbody>();
		_rot = transform.rotation;
	}

	void OnEnable() => _playerInputs.Enable();
	void OnDisable() => _playerInputs.Disable();

	void Update()
	{
		transform.LookAt(target);
		transform.RotateAround(target.position, transform.up, rotationDir * rotationSpeed * Time.deltaTime);
		// Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		// transform.position = smoothedPosition;

	}

}
