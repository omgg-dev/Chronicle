using System.IO;
using UnityEditor;
using UnityEngine;

public static class Exporter {

    private static readonly string _ExportPath = "ExportedPackages/Chronicle.unitypackage";

    [MenuItem("OMGG/Export Package")] // Allow Unity to call this method from the menu bar
    public static void ExportPackage()
    {
        // All path to export from Assets folder
        string[] assetsToExport = {
            "Assets/OMGG/Package/Chronicle/Demo",
            "Assets/OMGG/Package/Chronicle/Scripts",
            "Assets/OMGG/Package/Chronicle/Resources",
            "Assets/OMGG/Package/Chronicle/Editor"
        };

        // Create an export folder if needed
        string dir = Path.GetDirectoryName(_ExportPath);

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        // Export the package
        AssetDatabase.ExportPackage(assetsToExport, _ExportPath, ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);

        Debug.Log($"Export completed: {_ExportPath}");
    }
}
