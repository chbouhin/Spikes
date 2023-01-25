using UnityEngine;
using System.Collections;

public class AnimatePlusOne : MonoBehaviour {

    public TextMesh text;

	// Use this for initialization
	IEnumerator Start () {

        float elapsedTime = 0;
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b,0);
        float duration = 1;
        while (elapsedTime < duration)
        {
            float k = elapsedTime / duration;
            Color color = Color.Lerp(startColor, targetColor, k*k*k);
            text.color = color;
            transform.position += .5f*Vector3.up * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
