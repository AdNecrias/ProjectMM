using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBarScript : MonoBehaviour {

    public GameObject ColorableImage;
    public GameObject ColorableBar;
    public Color hpColor = new Color(99/256, 165/256, 68/256, 1);

    /// <summary>
    /// 0 to 1 HP percent value, change me externally.
    /// </summary>
    public float percentHp = 0.7f;

    const float maxOffset = 0.42f;

    RawImage raw;
    Rect nRect;
    Image img;

    // Use this for initialization
    void Start () {
        raw = ColorableBar.GetComponent<RawImage>();
        nRect = raw.uvRect;

        img = ColorableImage.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        nRect.x = (1 - percentHp) * maxOffset;
        raw.uvRect = nRect;
        raw.color = hpColor;
        img.color = hpColor;
    }
}
