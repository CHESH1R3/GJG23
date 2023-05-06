using UnityEngine;

public class SetRipple : MonoBehaviour
{
    public Material rippleMaterial;

    public float rippleCount;
    public float rippleSpeed;
    public float rippleStrength;

    public float rippleCountMax;
    public float rippleStrengthMax;

    public float rippleCountThreshold;
    public float rippleMaxThreshold;

    public bool is2003;

    private void Update()
    {
        Transition();
    }

    void Transition()
    {
        float matRippleCount = rippleMaterial.GetFloat("_Ripple_Count");
        float matRippleSpeed = rippleMaterial.GetFloat("_Ripple_Speed");

        if (!is2003)
        {
            if (matRippleCount != rippleCountMax) matRippleCount = Mathf.Lerp(matRippleCount, rippleCountMax, rippleCount * Time.deltaTime);
            if (matRippleCount + rippleCountThreshold >= rippleCountMax)
            {
                if (matRippleCount != 0) matRippleCount = Mathf.Lerp(matRippleCount, 0, rippleCount * Time.deltaTime);
            }
        }
        else
        {
        }

        rippleMaterial.SetFloat("_Ripple_Count", matRippleCount);
        rippleMaterial.SetFloat("_Ripple_Speed", matRippleSpeed);
    }
}