using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Settings")]
	public float diggingTime = 2;
	public bool isDigging = false;
	[Header("References")]
	public PlayerWeapon shovel, metalDetector;
	public Item hand;
	private PlayerInputs _inputs;
	private GameObject _spawnedWeapon;
	private ItemSpot _currentItemSpot = null;
	private Animator _animator;
	private float diggingTimer = 0;
	void Awake()
	{
		_inputs = new PlayerInputs();
		_animator = GetComponent<Animator>();
		HandleActions();
	}

	void HandleActions()
	{
		_inputs.Game.Dig.performed += ctx => isDigging = true;
		_inputs.Game.Dig.canceled += ctx => isDigging = false;
		_inputs.Game.Action.canceled += ctx => HandleInteraction();
	}

	void OnEnable()
	{
		_inputs.Enable();
		ItemSpot.OnPlayer += OnPlayerHitItemSpot;
	}

	void OnDisable()
	{
		_inputs.Disable();
		ItemSpot.OnPlayer -= OnPlayerHitItemSpot;
	}

	void Update()
	{
		HandleAction();
		metalDetector?.Perform(transform.position);
	}

	void HandleAction()
	{
		if(isDigging){
			if (_currentItemSpot == null || hand) {
				isDigging = false;
				return;
			};
			diggingTimer += Time.deltaTime;
			if(diggingTimer > diggingTime){
				hand = _currentItemSpot.Take();
				isDigging = false;
			}
		}
		_animator.SetBool("isDigging", isDigging);
	}

	void HandleInteraction(){

	}

	void OnPressActionButton()
	{
		isDigging = true;
		

		shovel.Perform(transform.position);
		
		Debug.Log($"Over {hand.name}");
	}
	void DestroyWeapon()
	{
		Destroy(_spawnedWeapon);
	}

	void OnPlayerHitItemSpot(bool isOver, ItemSpot itemSpot)
	{

		if (!isOver)
		{
			_currentItemSpot = null;
			return;
		}

		_currentItemSpot = itemSpot;
	}
}
