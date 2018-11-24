using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bake : MonoBehaviour
{
    [MenuItem("My stuff/Bake me")]
    static void BakeMeMenu()
    {
        BakeMaster2000 bm = new BakeMaster2000();
        bm.BakeMe();
    }
}

public class BakeMaster2000
{ 
    struct SkinData
    {
        public GameObject m_go;
        public Transform[] m_bones;
        public Transform m_rootBone;

        public SkinData(GameObject go, Transform[] bones, Transform rootBone)
        {
            m_go = go;
            m_bones = bones;
            m_rootBone = rootBone;
        }
    }

    public void BakeMe()
    {
        SkinData[] data = CopySkinRenderers();
        PrepareObjects(data);
        Lightmapping.Bake();
        FixObjects(data);
    }

    private void FixObjects(SkinData[] data)
    {
        for (int i=0; i < data.Length; ++i)
        {
            GameObject go = data[i].m_go;
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            Material[] materials = mr.sharedMaterials;
            MeshFilter mf = go.GetComponent<MeshFilter>();
            Mesh m = mf.sharedMesh;
            Vector4 st = mr.lightmapScaleOffset;
            int ind = mr.lightmapIndex;
            GameObject.DestroyImmediate(mf);
            GameObject.DestroyImmediate(mr);
            go.isStatic = false;

            SkinnedMeshRenderer r = go.AddComponent<SkinnedMeshRenderer>();
            BakeMasterData b = go.AddComponent<BakeMasterData>();
            b.m_lightmapST = st;
            b.m_lightmapIndex = ind;
            b.m_renderer = r;
            r.bones = data[i].m_bones;
            r.rootBone = data[i].m_rootBone;
            r.sharedMaterials = materials;
            r.sharedMesh = m;
        }
    }

    private void PrepareObjects(SkinData[] data)
    {
        for (int i=0; i < data.Length; ++i)
        {
            GameObject go = data[i].m_go;
            SkinnedMeshRenderer r = go.GetComponent<SkinnedMeshRenderer>();
            BakeMasterData bmd = go.AddComponent<BakeMasterData>();
            if(bmd != null)
            {
                GameObject.DestroyImmediate(bmd);
            }
            Material[] materials = r.sharedMaterials;
            Mesh m = r.sharedMesh;
            GameObject.DestroyImmediate(r);
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            MeshFilter mf = go.AddComponent<MeshFilter>();
            mf.sharedMesh = m;
            mr.sharedMaterials = materials;
            go.isStatic = true;
        }
    }

    private SkinData[] CopySkinRenderers()
    {
        SkinnedMeshRenderer[] renderers = null;
        renderers = GameObject.FindObjectsOfType<SkinnedMeshRenderer>();
        SkinData[] data = new SkinData[renderers.Length];

        for (int i = 0; i < renderers.Length; ++i)
        {
            SkinnedMeshRenderer r = renderers[i];
            data[i] = new SkinData(r.gameObject, r.bones, r.rootBone);
        }
        
        return data;
    }
}
