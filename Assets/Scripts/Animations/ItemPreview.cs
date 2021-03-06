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
    private Transform parent;

    public GameObject camera;

	void Awake(){
		rectTransform = transform.GetComponent<RectTransform>();
        parent = transform.parent;
        camera = Camera.main.gameObject;
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
        parent.transform.LookAt(camera.transform);
    }

	public void ChangeItem(Item item){
		if(item == null) {
			image.enabled = false;
			item = null;
			GetComponent<Image>().enabled = false;
			return;
		}
		GetComponent<Image>().enabled = true;
		image.enabled = true;
		image.sprite = item.portrait;
		this.item = item;
	}
}
