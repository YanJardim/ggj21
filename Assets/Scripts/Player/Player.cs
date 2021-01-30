using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerWeapon shovel, metalDetector;
	public Item hand;
	private PlayerInputs _inputs;
	private GameObject _spawnedWeapon;
	private ItemSpot _currentItemSpot = null;

	void Awake()
	{
		_inputs = new PlayerInputs();
		HandleActions();
	}

	void HandleActions()
	{
		_inputs.Game.Action.performed += ctx => OnPressActionButton();
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
		metalDetector?.Perform(transform.position);
	}

	void OnPressActionButton()
	{
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
