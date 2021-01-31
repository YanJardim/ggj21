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
	public Item currentItem;

	void Awake(){
		ChangeCurrentItemRandom();
	}

    public bool ReturnItem(Item item){
		if(!list.Contains(item)) {
			Debug.LogWarning("We don't accept this kind of item");
			return false;
		};
		if(items.Contains(item)){
			Debug.LogWarning("We already have this item!");
			return false;
		}
		animator.SetTrigger("success");
		AudioManager.Instance.PlaySFX(successAudio);
		ChangeCurrentItemRandom();
		items.Add(item);
		return true;
	}

	public void ChangeCurrentItemRandom(){
		if(IsDone()){
			itemPreview.ChangeItem(null);
		};
		var newItem = list.Find(x => !items.Contains(x));
		itemPreview.ChangeItem(newItem);
	}

	public bool IsDone(){
		return list.All(items.Contains);
	}
}
