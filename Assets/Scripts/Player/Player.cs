using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Settings")]
	public float diggingTime = 2;
	public float sphereRadius = 10;
	public bool isDigging = false;
	public bool showGizmos = true;
	public LayerMask actionMask;
	[Header("References")]
	public PlayerWeapon shovel, metalDetector;
	public Animator animator;
	public Item hand;
	private PlayerInputs _inputs;
	private GameObject _spawnedWeapon;
	private ItemSpot _currentItemSpot = null;
	private ItemReturnSpot _currentItemReturnSpot = null;
	private float diggingTimer = 0;
	void Awake()
	{
		_inputs = new PlayerInputs();
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
		if (isDigging)
		{
            if (_currentItemSpot != null) {
                GameObject child = transform.GetChild(1).gameObject;
                Debug.Log(child.name);
                var currentRotation = child.transform.rotation.eulerAngles;
                child.transform.LookAt(_currentItemSpot.top.transform.position);
                child.transform.rotation.SetEulerAngles(0f, transform.rotation.eulerAngles.y, 0f);
            }
           
            if (_currentItemSpot == null || hand)
			{
				isDigging = false;
				return;
			};
			if(!_currentItemSpot.item){
				isDigging = false;
				return;
			}
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
		animator.SetBool("isDigging", isDigging);
	}

	void HandleInteraction()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius, actionMask);
		foreach (var collider in colliders)
		{
			Debug.Log($"Interaction {collider?.name}");
			switch (collider.tag)
			{
				case "ReturnItem":
					HandleReturnItem(collider.GetComponent<ItemReturnSpot>());
					break;
				case "TrashCan":
					hand = null;
					break;
			}

		}
	}

	void HandleReturnItem(ItemReturnSpot spot)
	{
		if (spot && hand)
		{
			if (spot.ReturnItem(hand))
			{
				hand = null;
			}
			return;
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

	void OnDrawGizmos()
	{
		if (!showGizmos) return;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, sphereRadius);
	}
}
