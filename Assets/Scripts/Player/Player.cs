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
	public AudioClip diggingSound;
	public Animator animator;
	public ItemPreview itemPreview;
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
		itemPreview.ChangeItem(null);
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
			if (_currentItemSpot != null && _currentItemSpot.top)
			{
				LookAtTarget(_currentItemSpot.top.transform);
			}

			if (_currentItemSpot == null || hand)
			{
				isDigging = false;
				return;
			};
			if (!_currentItemSpot.item)
			{
				isDigging = false;
				return;
			}
			diggingTimer += Time.deltaTime;

			if (diggingTimer > diggingTime)
			{
				hand = _currentItemSpot.Take();
				itemPreview.ChangeItem(hand);
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
				case "Chest":
					var chest = collider.GetComponent<Chest>();
					if (chest.hasItem()){
						if(hand) return;
						var item = chest.Give();
						itemPreview.ChangeItem(item);
						hand = item;
					}
					else if (chest.Take(hand))
					{
						itemPreview.ChangeItem(null);
						hand = null;
					}
					break;
			}

		}
	}

	void HandleReturnItem(ItemReturnSpot spot)
	{
		if (spot)
			LookAtTarget(spot.transform);
		if (spot && hand)
		{
			if (spot.ReturnItem(hand))
			{
				hand = null;
				itemPreview.ChangeItem(null);
				animator.SetTrigger("cheer");
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
	void LookAtTarget(Transform target)
	{
		GameObject child = transform.GetChild(1).gameObject;
		var currentRotation = child.transform.rotation.eulerAngles;
		child.transform.LookAt(target.position);
		child.transform.rotation.SetEulerAngles(0f, transform.rotation.eulerAngles.y, 0f);
	}
	void OnDrawGizmos()
	{
		if (!showGizmos) return;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, sphereRadius);
	}
}
