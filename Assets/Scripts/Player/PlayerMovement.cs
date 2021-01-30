using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 5;
	private Rigidbody _rb;
	private Vector2 _dir = Vector2.zero;
	private PlayerInputs _inputs;

	void Awake(){
		_rb = GetComponent<Rigidbody>();
		_inputs = new PlayerInputs();
		HandleActions();
	}

	void HandleActions(){
		_inputs.Game.Movement.performed += ctx => _dir = ctx.ReadValue<Vector2>();
		_inputs.Game.Movement.canceled += ctx => _dir = Vector2.zero;
	}

	void OnEnable() => _inputs.Enable(); 
	void OnDisable() => _inputs.Disable(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		var newDir = new Vector3(_dir.x, 0, _dir.y);
        _rb.MovePosition(_rb.position + newDir * speed);
    }
}
