using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerWeapon hand;
	public Transform weaponHolderTransform;
	private PlayerInputs _inputs;
	private GameObject _spawnedWeapon;
	public ItemSpot _currentItemSpot = null;

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

	void Start()
	{
		SpawnWeapon();
	}

	void Update()
	{
		if (hand.recurring)
		{
			hand.Perform(transform.position);
		}
	}

	void OnPressActionButton()
	{
		if (hand.recurring || _currentItemSpot == null) return;

		hand.Perform(transform.position);
		_currentItemSpot.Take();
	}
	void DestroyWeapon()
	{
		Destroy(_spawnedWeapon);
	}
	void SpawnWeapon()
	{
		_spawnedWeapon = Instantiate(hand.model, weaponHolderTransform);
		_spawnedWeapon.transform.localPosition = Vector3.zero;
	}
	void ChangeWeapon(PlayerWeapon weapon)
	{
		DestroyWeapon();
		this.hand = weapon;
		SpawnWeapon();
	}

	void OnPlayerHitItemSpot(bool isOver, ItemSpot itemSpot)
	{
		Debug.Log($"Over {isOver} {itemSpot.name}");
		if (!isOver)
		{
			_currentItemSpot = null;
			return;
		}

		_currentItemSpot = itemSpot;
	}
}
