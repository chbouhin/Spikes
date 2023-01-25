using UnityEngine;
using System.Collections;

public class AnimateSinusMvtAlongY : MonoBehaviour {

    public float amplitude;
    public float pulsation;
    float elapsedTime;

    Vector3 startPos;

    // Use this for initialization
    IEnumerator Start () {
        elapsedTime = 0;
        startPos = transform.position;

        while (true)
        {
            transform.position = startPos + Vector3.up * Mathf.Sin(pulsation * elapsedTime) * amplitude;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
