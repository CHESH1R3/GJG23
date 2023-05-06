using UnityEngine;

public class SetRipple : MonoBehaviour
{
    public Material rippleMaterial;

    public float rippleCount;
    public float rippleSpeed;
    public float rippleStrength;

    public float rippleMax;

    public float rippleThreshold;

    public bool is2003;

    private void Update()
    {
        Transition();
    }

    void Transition()
    {
        float rippleCount = rippleMaterial.GetFloat("_Ripple Count");
        float rippleSpeed = rippleMaterial.GetFloat("_Ripple Speed");

        if (!is2003)
        {
            if (rippleCount != rippleMax) rippleCount = Mathf.Lerp(rippleCount, rippleMax, this.rippleCount * Time.deltaTime);
        }
        else
        {
            if (rippleCount != 0) rippleCount = Mathf.Lerp(rippleCount, 0, this.rippleCount * 2 * Time.deltaTime);
        }

        rippleMaterial.SetFloat("_Size", rippleCount);
        rippleMaterial.SetFloat("_Pixelation", rippleSpeed);
        rippleMaterial.SetVector("_Position", new Vector2(-transform.position.x, -transform.position.y));
    }
}