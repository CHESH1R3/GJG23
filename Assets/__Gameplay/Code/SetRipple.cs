using UnityEngine;

public class SetRipple : MonoBehaviour
{
    public Material rippleMaterial;

    public float rippleCount;
    public float rippleSpeed;
    public float rippleStrength;

    private void Update()
    {
        Sync();
    }

    void Sync()
    {
        rippleMaterial.SetFloat("_Ripple_Count", rippleCount);
        rippleMaterial.SetFloat("_Ripple_Speed", rippleSpeed);
        rippleMaterial.SetFloat("_Ripple_Strength", rippleStrength);

        rippleMaterial.SetVector("_Ripple_Center", new Vector2(transform.position.x, transform.position.y));
    }
}