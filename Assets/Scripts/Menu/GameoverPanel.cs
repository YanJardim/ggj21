using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
		transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(GetComponent<RectTransform>(), new Vector3(1, 1, 1), 1.2f).setEase(LeanTweenType.easeInBounce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
