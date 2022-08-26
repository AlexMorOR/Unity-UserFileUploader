using System.IO;
using UnityEditor;

public class JSLibFileCreator
{
    [MenuItem("Assets/Create/JS Script", priority = 80)]
    private static void CreateJSLibFile()
    {
        var asset =
            "mergeInto(LibraryManager.library,\n" +
            "{\n" +
	            "\t// Your code here\n" +
            "});";

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        ProjectWindowUtil.CreateAssetWithContent(AssetDatabase.GenerateUniqueAssetPath(path + "/JSScript.jslib"), asset);
        AssetDatabase.SaveAssets();
    }
}
