using UnityEngine;

public class GlobalMaterialReplacer : MonoBehaviour
{
    [Header("Shared material to assign as Element 0")]
    public Material sharedMaterialElement0;

    [Header("Template material for Element 1 (will be duplicated per object)")]
    public Material newMaterialTemplate;

    // Property names (GLTF-style)
    private static readonly string COLOR_PROP = "baseColorFactor";
    private static readonly string TEXTURE_PROP = "baseColorTexture";
    private static readonly string ROUGHNESS_PROP = "roughnessFactor";
    private static readonly string METALLIC_PROP = "metallicFactor";

    void Start()
    {
        if (sharedMaterialElement0 == null || newMaterialTemplate == null)
        {
            Debug.LogError("Both sharedMaterialElement0 and newMaterialTemplate must be assigned.");
            return;
        }

        Renderer[] allRenderers = FindObjectsOfType<Renderer>();

        foreach (Renderer rend in allRenderers)
        {
            Material original = rend.sharedMaterial;
            Material newInstance = new Material(newMaterialTemplate);

            // Copy Color
            if (original.HasProperty(COLOR_PROP) && newInstance.HasProperty(COLOR_PROP))
            {
                newInstance.SetColor(COLOR_PROP, original.GetColor(COLOR_PROP));
            }

            // Copy Texture
            if (original.HasProperty(TEXTURE_PROP) && newInstance.HasProperty(TEXTURE_PROP))
            {
                newInstance.SetTexture(TEXTURE_PROP, original.GetTexture(TEXTURE_PROP));
            }

            // Copy Roughness
            if (original.HasProperty(ROUGHNESS_PROP) && newInstance.HasProperty(ROUGHNESS_PROP))
            {
                newInstance.SetFloat(ROUGHNESS_PROP, original.GetFloat(ROUGHNESS_PROP));
            }

            // Copy Metallic
            if (original.HasProperty(METALLIC_PROP) && newInstance.HasProperty(METALLIC_PROP))
            {
                newInstance.SetFloat(METALLIC_PROP, original.GetFloat(METALLIC_PROP));
            }

            // Assign the materials
            rend.materials = new Material[] { sharedMaterialElement0, newInstance };
        }

        Debug.Log("All properties transferred. Materials updated successfully.");
    }
}
