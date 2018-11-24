using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeMasterData : MonoBehaviour
{
    public Vector4 m_lightmapST = Vector4.one;
    public int m_lightmapIndex = 0;
    public SkinnedMeshRenderer m_renderer = null;

    private void Start()
    {
        for (int i=0; i < m_renderer.sharedMaterials.Length; ++i)
        {
            m_renderer.sharedMaterials[i].SetTextureScale(
                "_Lightmap",
                new Vector2(m_lightmapST.x, m_lightmapST.y));
            m_renderer.sharedMaterials[i].SetTextureOffset(
                "_Lightmap",
                new Vector2(m_lightmapST.z, m_lightmapST.w));
            m_renderer.sharedMaterials[i].SetTexture("_Lightmap", LightmapSettings.lightmaps[m_lightmapIndex].lightmapColor);
        }
    }
}
