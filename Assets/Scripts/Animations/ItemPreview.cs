using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{

	public Item item;
	public bool loop = false;
	public float time = 2;
	public Image image;
	private RectTransform rectTransform;

	void Awake(){
		rectTransform = transform.GetComponent<RectTransform>();
	}
    // Start is called before the first frame update
    void Start()
    {
        // LeanTween.scale(rectTransform, new Vector3(1, 1, 1), time).setEase(LeanTweenType.easeOutBounce).setRepeat(loop ? -1 : 1);
        LeanTween.move(rectTransform, transform.up / 20, time).setEase(LeanTweenType.easeInBounce).setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ChangeItem(Item item){
		image.sprite = item.portrait;
		this.item = item;
	}
}
