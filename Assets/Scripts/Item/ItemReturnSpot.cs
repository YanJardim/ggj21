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

	void Start(){
		itemPreview.ChangeItem(list[0]);
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
		items.Add(item);
		return true;
	}

	public bool CheckList(){
		return list.All(items.Contains);
	}
}
