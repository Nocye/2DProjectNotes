using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// AssetPostprocessor�� ��ͼ��ģ�͡���������Դ����ʱ���ã����Զ�������Ӧ����
/// ����ͼƬʱ�Զ�����ͼƬ�Ĳ���
/// </summary>
public class TextureImportSetting : AssetPostprocessor
{
    /// <summary>
    /// ͼƬ����֮ǰ���ã�������ͼƬ�ĸ�ʽ,Tag,��ͬƽ̨ͼƬ��ѹ��ģʽ
    /// </summary>
    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter)assetImporter;
        importer.textureType = TextureImporterType.Sprite; // ����ΪSprite����
        importer.mipmapEnabled = false; // ����mipmap
        //importer.spritePackingTag = "tag"; // ����Sprite�Ĵ��Tag
        //���ò�ͬƽ̨��ͼƬѹ��ģʽ
        TextureImporterPlatformSettings textureSettings = importer.GetPlatformTextureSettings("Standalone");
        textureSettings.overridden = true;
        textureSettings.format = TextureImporterFormat.DXT5;
        importer.SetPlatformTextureSettings(textureSettings);
        importer.mipmapEnabled = false;
    }

    /// <summary>
    /// ͼƬ�Ѿ���ѹ�������浽ָ��Ŀ¼��֮�����
    /// </summary>
    /// <param name="texture"></param>
    void OnPostprocessTexure(Texture2D texture)
    {
        Debug.Log(texture.name);
    }

    /// <summary>
    /// ������Դ�����롢ɾ�����ƶ����֮�����
    /// </summary>
    /// <param name="importedAssets"></param>
    /// <param name="deletedAssets"></param>
    /// <param name="movedAssets"></param>
    /// <param name="movedFromAssetPaths"></param>
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            Debug.Log("Reimported Asset: " + str);
        }
        foreach (string str in deletedAssets)
        {
            Debug.Log("Deleted Asset: " + str);
        }

        for (int i = 0; i < movedAssets.Length; i++)
        {
            Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        }
    }
}