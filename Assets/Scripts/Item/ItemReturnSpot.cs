using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ItemReturnSpot : MonoBehaviour
{
	public List<Item> list;
	public List<Item> items;
	public Animator animator;
	public ItemPreview itemPreview;
	public AudioClip successAudio;
	public AudioClip nopeSound;
	public Item currentItem;

	void Awake()
	{
		// list.Sort();
		ChangeToNextItem();
	}

	public bool ReturnItem(Item item)
	{
		if (!list.Contains(item) || currentItem != item)
		{
			Debug.LogWarning("We don't accept this kind of item");
			AudioManager.Instance.PlaySFX(nopeSound);
			animator.SetTrigger("nope");
			return false;
		};
		if (items.Contains(item))
		{
			Debug.LogWarning("We already have this item!");
			animator.SetTrigger("nope");
			AudioManager.Instance.PlaySFX(nopeSound);
			return false;
		}
		animator.SetTrigger("success");
		AudioManager.Instance.PlaySFX(successAudio);
		ChangeToNextItem();
		items.Add(item);
		return true;
	}

	public void ChangeToNextItem()
	{
		if (IsDone())
		{
			itemPreview.ChangeItem(null);
		};
		var index = 0;
		if (!currentItem)
		{
			currentItem = list[0];
			index = -1;
		}
		else
		{
			index = list.IndexOf(currentItem);
		}
		var newItem = list[++index];
		itemPreview.ChangeItem(newItem);
		currentItem = newItem;
		Debug.Log($"New item: {newItem.name}");
	}

	public bool IsDone()
	{
		return list.All(items.Contains);
	}
}
