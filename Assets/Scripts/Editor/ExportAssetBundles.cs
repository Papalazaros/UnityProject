using System.IO;
using UnityEditor;
using UnityEngine;

public static class ExportAssetBundles
{
    [MenuItem("Assets/Build AssetBundle")]
    private static void ExportResource()
    {
        const string folderName = "AssetBundles";
        string filePath = Path.Combine(Application.streamingAssetsPath, folderName);
        BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
    }
}