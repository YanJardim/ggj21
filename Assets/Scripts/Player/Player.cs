using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerWeapon shovel, metalDetector;
	public Item hand;
	public bool isDigging = false;
	private PlayerInputs _inputs;
	private GameObject _spawnedWeapon;
	private ItemSpot _currentItemSpot = null;
	private Animator _animator;

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
		_animator.SetBool("isDigging", isDigging);
	}

	void HandleInteraction(){

	}

	void OnPressActionButton()
	{
		isDigging = true;
		if (_currentItemSpot == null || hand) return;

		shovel.Perform(transform.position);
		hand = _currentItemSpot.Take();
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
