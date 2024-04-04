Forest Stage Pack(2019/8/26 - by Fatty War, id3644 @gmail.com)
manual(google drive : https://drive.google.com/open?id=1KHBy1Ss5PQfxNuop0LA4xL_0048DNRoz2yLbntmpsMY)

Forest Stage Pack V1.01 1st (2021/2/05)
The package has been reorganized for URP compatibility.(see section 6)
 * Support for "Splatting Terrain Shader" is discontinued. (files have been backed up to the "Old" folder for compatibility).
 * Demo scenes and materials have been reorganized due to the discontinuation of support for terrain shaders. (Old demo scenes were kept in the "Old" folder)
 * Fixed Transparent Cutout shader to work in URP. (UnlitCutoutShadow.shader) 

Forest Stage Pack V1.01(2019/10/05)
 * Added Unity 2017 package.(2017,2019 Support / Tested 2018)
 * Tree mesh & prefab added. (new entry : Pine tree x3, Maple tree x3)
 * Modify existing object surface shading.(Texture shade, not shader!)
 
This document explains how to use the pack.

Introduction

    A low poly style forest stage pack for creating landscapes.
	
	* Includes hand painted terrain textures to match the low poly style.
    * Includes shader to blending terrain texture. (alpha + 5 channel texture blending)	!! This feature is no longer supported !!
    * With shader to express cartoon style water.
	* Sample scene included. (FPS, Top-Down, Arena)
    * Including example scene. (Shader Example, Multi Terrain Example)

1. Hand painted terrain textures


    A set of hand painted style floor textures for terrain blending.
		
	15 hand painted textures.(Ground A x 4. Ground B x 4. Desert x 3.Grass x 1, Stone A x 1. Stone B x 1. Water x 1.)

2. Splatting Terrain Shader
	
	//////////////////////////////////////////////////////////////////////////////
	 !! This feature is no longer supported !!
	 This feature only works with Unity built-in render pipeline.
	 For compatibility, the files have been backed up to the "Old" folder.
	//////////////////////////////////////////////////////////////////////////////

    This shader focuses on personal development, without the need for a professional 3D operator, to create terrain using only mask textures(using Photoshop or 2D tools).

    * Assets\ForestStagePack\Shaders\Splatting5Shadow.shader
    * Terrain shader example(assset\ForestStagePack\Materials\Sample\TerrainShaderSample.mat)


        * 5 texture blending.
		* Use mask texture. (R, G, B, A + Black / 5CH)
        * Multiple terrain possible(not automatic, need to work manually)
        * There are examples of using single terrain & multi terrain within the scene.(assset \ ForestStagePack \ Scenes \ MultiTerrainSample.unity)
        * Note) Multi-Terrain setting of the package is good performance but old work style, Forest Pack recommends single terrain type of small game.
		
3. Water Shader


    This shader can express cartoon-style water. (Assets\ForestStagePack\Shaders\ToonWater.shader)

    Water Shader Manual(google drive : https://docs.google.com/document/d/1KHBy1Ss5PQfxNuop0LA4xL_0048DNRoz2yLbntmpsMY/edit#heading=h.gp9aoxjnbn76)
		
		* Depth blending support (two colors)
        * Water edge support(water foam)
        * Mobile support
        * Animation Support
        * Surface effect support
		* Water shader not support orthographic camera
		
		* Note) You need the “CameraDepthTextureMode” script to use Water Shader on your mobile device. (Assets \ ForestStagePack \ Scripts \ CameraDepthTextureMode.cs)


        * To use the camera depth texture, follow these steps:     (URP project is not applicable)
			1Step.Attach the script to the camera.
			2Step.Change Depth Texture Mode to "Depth".

			-URP project is built into the camera, please turn on the depth texture function.(Camera / Rendering / Depth Texture > On)

4. Sample Scene


   FPS.unity
       Sample of landscape in first person view

   Top-Down.unity
       Sample level in quarter view


   Arena.unity
       Two-player stadium background sample

5.Example Scene


   TerrainShaderSample.unity (This feature has been deprecated, so files have been backed up to the "Old" folder.)
       Example of shader for terrain Splatting.


   MultiTerrainSample.unity (This feature has been deprecated, so files have been backed up to the "Old" folder.)
       * Example of using single terrain & multi terrain.

6. URP support. (Unity Editor 2018-LwRP/2019-URP or higher is required)
	
	- Unity Editor : Edit > Render Pipeline > Upgrade Project Materials





Thank you for your purchase.
