using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public List<Sprite> images;
	public Image image;
	private PlayerInputs _inputs;
	private int currentIndex = 0;
	void Awake()
	{
		_inputs = new PlayerInputs();
		_inputs.Menu.Action.performed += ctx => NextImage();
		ChangeImage(images[0]);
		image = GetComponent<Image>();
	}

	void OnEnable() => _inputs.Enable();
	void OnDisable() => _inputs.Disable();
	// Start is called before the first frame update


	public void NextImage()
	{
		Debug.Log($"Next image {currentIndex}");
		if (currentIndex + 1 >= images.Count)
		{
			LeanTween.color(GetComponent<RectTransform>(), new Color(1, 1, 1, 0), 0.25f);
			SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
			return;
		}
		currentIndex++;
		ChangeImage(images[currentIndex]);
	}
	public void ChangeImage(Sprite sprite)
	{
		LeanTween.color(GetComponent<RectTransform>(), new Color(1, 1, 1, 0), 0.25f)
		.setOnComplete(() =>
		{
			image.sprite = sprite;
			LeanTween.color(GetComponent<RectTransform>(), new Color(1, 1, 1, 1), 0.25f);
		});
	}
}
