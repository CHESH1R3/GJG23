using UnityEngine;

public class SetRipple : MonoBehaviour
{
    public Material rippleMaterial;

    public float rippleSpeed;
    public float stopSpeed;

    public float rippleThreshold;

    public bool is2003;

    private void Update()
    {
        Transition();
    }

    void Transition()
    {
        float vortexSize = rippleMaterial.GetFloat("_Size");
        float vortexPixelation = rippleMaterial.GetFloat("_Pixelation");

        if (is2003)
        {
            if (vortexPixelation != 1) vortexPixelation = 1;
            if (vortexSize != 250) vortexSize = Mathf.Lerp(vortexSize, 250, rippleSpeed * Time.deltaTime);
        }
        else
        {
            if (vortexPixelation != 256) vortexPixelation = 256;
            if (vortexSize != 0) vortexSize = Mathf.Lerp(vortexSize, 0, rippleSpeed * 2 * Time.deltaTime);
        }

        rippleMaterial.SetFloat("_Size", vortexSize);
        rippleMaterial.SetFloat("_Pixelation", vortexPixelation);
        rippleMaterial.SetVector("_Position", new Vector2(-transform.position.x, -transform.position.y));
    }
}