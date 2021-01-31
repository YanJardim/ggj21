using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	public ItemPreview itemPreview;
	public Item currentItem;
	public AudioClip nopeSound;
	// Start is called before the first frame update
	void Start()
	{
		itemPreview.ChangeItem(null);
	}

	public bool Take(Item item)
	{
		if (currentItem)
		{
			AudioManager.Instance.PlaySFX(nopeSound);
			return false;
		}
		itemPreview.ChangeItem(item);
		currentItem = item;
		return true;
	}

	public Item Give(){
		if (!currentItem)
		{
			AudioManager.Instance.PlaySFX(nopeSound);
			return null;
		}
		var aux = currentItem;
		currentItem = null;
		itemPreview.ChangeItem(null);
		return aux;
	}

	public bool hasItem(){
		return currentItem;
	}
}
