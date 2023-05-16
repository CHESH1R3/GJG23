using UnityEngine;

public class SetFX : MonoBehaviour
{
    public Material fxMaterial;

    public bool is2003;

    private void Update()
    {
        Transition();
    }

    void Transition()   
    {
        int isPixelated = fxMaterial.GetInt("_Pixelation");
        float nodeOpacity = fxMaterial.GetFloat("_Node_Opacity");
        
        if (is2003)
        {
            isPixelated = 1; nodeOpacity = 0.25f;
        }
        else
        {
            isPixelated = 0; nodeOpacity = 0.125f;
        }

        fxMaterial.SetInt("_Pixelation", isPixelated);
        fxMaterial.SetFloat("_Node_Opacity", nodeOpacity);
    }
}