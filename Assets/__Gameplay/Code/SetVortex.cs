using UnityEngine;

public class SetVortex : MonoBehaviour
{
    public Material vortexMaterial;

    public float transitionSpeed;

    public bool is2003;

    private void Update()
    {
        Transition();
    }

    void Transition()
    {
        float vortexSize = vortexMaterial.GetFloat("_Size");
        float vortexPixelation = vortexMaterial.GetFloat("_Pixelation");

        if (is2003)
        {
            if (vortexPixelation != 1) vortexPixelation = 1;
            if (vortexSize != 250) vortexSize = Mathf.Lerp(vortexSize, 250, transitionSpeed * Time.deltaTime);
        }
        else
        {
            if (vortexPixelation != 256) vortexPixelation = 256;
            if (vortexSize != 0) vortexSize = Mathf.Lerp(vortexSize, 0, transitionSpeed * 2 * Time.deltaTime);
        }

        vortexMaterial.SetFloat("_Size", vortexSize);
        vortexMaterial.SetFloat("_Pixelation", vortexPixelation);
        vortexMaterial.SetVector("_Position", new Vector2(-transform.position.x, -transform.position.y));
    }


}