using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeTextOverLifetime : MonoBehaviour {

    Text text;
    Color initialColor;
    Color targetColor;

    public float Lifetime = 3;
    float timeLived = 0f;

	void Start () {
        text = GetComponent<Text>();
        initialColor = text.color;
        targetColor = text.color;
        targetColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(Lifetime == 0) {
            Destroy(GetComponent(typeof(FadeTextOverLifetime)));
            return;
        }
        timeLived += Time.deltaTime;
        text.color = Color.Lerp(initialColor, targetColor, applyFunction(timeLived/Lifetime));
	}

    float applyFunction(float x) {
        //return x * x;
        float quad = x * x;
        return Mathf.Sin(quad) * (quad);
    }
}
