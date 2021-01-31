using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public List<ItemReturnSpot> npcs;
	public event Action OnGameFinish;

    void Awake()
    {
        npcs = new List<ItemReturnSpot>((FindObjectsOfType<ItemReturnSpot>()));
		ItemReturnSpot.OnReturnItem += OnReturnItem;
    }

	void OnReturnItem(ItemReturnSpot spot){
		var npcsDone = 0;
		npcs.ForEach((npc) => {
			if(npc.IsDone()){
				npcsDone++;
			}
			Debug.Log($"Is ${npc.name} Done: {npc.IsDone()}");
		});
		if(npcsDone == npcs.Count){
			Debug.Log("Done!");
			OnGameFinish?.Invoke();
		}
	}
}
