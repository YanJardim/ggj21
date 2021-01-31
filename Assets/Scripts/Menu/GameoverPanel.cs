using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{
	public AudioClip success;
	public Sprite secondImage;
	private Image image;
	// Start is called before the first frame update
	void OnEnable()
	{
		AudioManager.Instance.PlaySFX(success);
		transform.localScale = new Vector3(0, 0, 0);
		LeanTween.scale(GetComponent<RectTransform>(), new Vector3(1, 1, 1), 1.2f).setEase(LeanTweenType.easeInBounce);
		Invoke("ChangeImage", 2.0f);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void ChangeImage()
	{
		transform.localScale = new Vector3(0, 0, 0);
		LeanTween.scale(GetComponent<RectTransform>(), new Vector3(1, 1, 1), 1.2f).setEase(LeanTweenType.easeInBounce);
		image.sprite = secondImage;
	}
}
