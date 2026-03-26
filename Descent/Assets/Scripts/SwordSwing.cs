using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    private bool isSwinging = false;

    public float swingDuration = 0.25f;
    public float rightStartAngle = -140f;
    public float rightEndAngle = 60f;
    public float leftStartAngle = 140f;
    public float leftEndAngle = -60f;
    public float rightRestAngle = -25f;
    public float leftRestAngle = 25f;

    public void Swing(bool facingRight)
    {
        if (!isSwinging)
        {
            StartCoroutine(SwingRoutine(facingRight));
        }
    }

    private IEnumerator SwingRoutine(bool facingRight)
    {
        isSwinging = true;

        float elapsed = 0f;
        float startAngle = facingRight ? rightStartAngle : leftStartAngle;
        float endAngle = facingRight ? rightEndAngle : leftEndAngle;
        float restAngle = facingRight ? rightRestAngle : leftRestAngle;

        while (elapsed < swingDuration)
        {
            float t = elapsed / swingDuration;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, restAngle);
        isSwinging = false;
    }
}