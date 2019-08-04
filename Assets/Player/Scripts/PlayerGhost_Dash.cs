using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost_Dash : MonoBehaviour
{
    
    [SerializeField]
    private AnimationCurve FadeCurve;
    [SerializeField]
    private float time;
    private float passedTime;

    SpriteRenderer spriteRenderer;
    private Color color;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        passedTime = 0f;
    }

    void Update()
    {
        passedTime += Time.deltaTime;

        float t = Mathf.Clamp01(passedTime / time);
        color.a = FadeCurve.Evaluate(t) * .8f;
        spriteRenderer.color = color;
    }

}
