using System.Collections;
using UnityEngine;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor {

	void OnPostprocessTexture (Texture2D texture)
	{
		string lowercaseAssetPath = assetPath.ToLower();
		bool isInSpriteDirectory = lowercaseAssetPath.IndexOf("/sprites/") != -1;
		if(isInSpriteDirectory)
		{
			TextureImporter textureImporter = assetImporter as TextureImporter;
			textureImporter.textureType = TextureImporterType.Sprite;
		}
	}
}
