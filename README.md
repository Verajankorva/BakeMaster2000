# BakeMaster2000

Unity tool which bake lights also to SkinnedMeshRenderers.

Tested with Unity 2018.2.14

Tool bake static meshes as Unity always does, but skinned meshes will also get baked. Tool first copy skinning information in memory and makes meshes as normal static meshes. Then lights are baked and after the bake static "skinned meshes" are turned back to regular skinned meshes. However Skinned Mesh Renderer don't give same lightmap infomation to the shader as MeshRenderer does so tool will create BakeMasterData component to provide that infomation to a shader. You can write any custom shader you wish and I made BakeMaster_Lightmap.shader as an example how to use the lightmaps.
