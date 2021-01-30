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
	private ItemReturnSpot _currentItemReturnSpot = null;
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
		_inputs.Game.Action.performed += ctx => HandleInteraction();
	}

	void OnEnable()
	{
		_inputs.Enable();
		ItemSpot.OnPlayer += OnPlayerHitItemSpot;
		ItemReturnSpot.OnPlayer += OnPlayerHitReturnSpot;
	}

	void OnDisable()
	{
		_inputs.Disable();
		ItemSpot.OnPlayer -= OnPlayerHitItemSpot;
		ItemReturnSpot.OnPlayer -= OnPlayerHitReturnSpot;
	}

	void Update()
	{
		HandleAction();
		metalDetector?.Perform(transform.position);
	}

	void HandleAction()
	{
		if (isDigging)
		{
			if (_currentItemSpot == null || hand)
			{
				isDigging = false;
				return;
			};
			diggingTimer += Time.deltaTime;
			if (diggingTimer > diggingTime)
			{
				hand = _currentItemSpot.Take();
				isDigging = false;
				diggingTimer = 0;
			}
		}
		else
		{
			diggingTimer = 0;
		}
		_animator.SetBool("isDigging", isDigging);
	}

	void HandleInteraction()
	{
		Debug.Log($"Interaction {_currentItemReturnSpot?.name}");
		if (_currentItemReturnSpot && hand)
		{
			if (_currentItemReturnSpot.ReturnItem(hand))
			{
				hand = null;
			}
		}
	}

	void OnPressActionButton()
	{
		isDigging = true;
		shovel.Perform(transform.position);
	}
	void DestroyWeapon()
	{
		Destroy(_spawnedWeapon);
	}

	void OnPlayerHitItemSpot(bool isOver, GameObject obj)
	{
		var itemSpot = obj.GetComponent<ItemSpot>();
		_currentItemSpot = isOver ? itemSpot : null;
	}
	void OnPlayerHitReturnSpot(bool isOver, GameObject obj)
	{
		Debug.Log($"Hit return {obj.name}");
		var itemReturnSpot = obj.GetComponent<ItemReturnSpot>();
		_currentItemReturnSpot = isOver ? itemReturnSpot : null;
	}
}
