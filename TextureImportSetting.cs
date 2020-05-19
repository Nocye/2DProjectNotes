using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// AssetPostprocessor： 贴图、模型、声音等资源导入时调用，可自动设置相应参数
/// 导入图片时自动设置图片的参数
/// </summary>
public class TextureImportSetting : AssetPostprocessor
{
    /// <summary>
    /// 图片导入之前调用，可设置图片的格式,Tag,不同平台图片的压缩模式
    /// </summary>
    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter)assetImporter;
        importer.textureType = TextureImporterType.Sprite; // 设置为Sprite类型
        importer.mipmapEnabled = false; // 禁用mipmap
        //importer.spritePackingTag = "tag"; // 设置Sprite的打包Tag
        //设置不同平台的图片压缩模式
        TextureImporterPlatformSettings textureSettings = importer.GetPlatformTextureSettings("Standalone");
        textureSettings.overridden = true;
        //小坑:如果format枚举选择为ASTC_RGBA,则编辑器会报错,但可以正常修改,改为ASTC_RGB则没有问题,都可以正常修改为RGB(A) Compressed ASTC 6x6 block         
        //猜测可能是Unity内部的枚举处理没有更新
        textureSettings.format = TextureImporterFormat.DXT5;
        importer.SetPlatformTextureSettings(textureSettings);
        importer.mipmapEnabled = false;
    }

    /// <summary>
    /// 图片已经被压缩、保存到指定目录下之后调用
    /// </summary>
    /// <param name="texture"></param>
    void OnPostprocessTexure(Texture2D texture)
    {
        Debug.Log(texture.name);
    }

    /// <summary>
    /// 所有资源被导入、删除、移动完成之后调用
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