using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ItemReturnSpot : MonoBehaviour
{
	public List<Item> list;
	public List<Item> items;

    public bool ReturnItem(Item item){
		if(!list.Contains(item)) {
			Debug.LogWarning("We don't accept this kind of item");
			return false;
		};
		if(items.Contains(item)){
			Debug.LogWarning("We already have this item!");
			return false;
		}

		items.Add(item);
		return true;
	}

	public bool CheckList(){
		return list.All(items.Contains);
	}
}
