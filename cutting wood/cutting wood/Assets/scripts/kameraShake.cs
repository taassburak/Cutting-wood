using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameraShake : MonoBehaviour
{
    public IEnumerator Shake (float time, float mag)
    {
        Vector3 originalPos = transform.localPosition;

        float shakeTime = 0.0f;

        while (shakeTime < time)
        {
            float x = Random.Range(-1f, 1f) * mag;
            float y = Random.Range(-1f, 1f) * mag;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            shakeTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;


    }
}
