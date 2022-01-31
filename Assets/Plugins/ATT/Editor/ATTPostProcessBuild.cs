using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace ATT
{
    public static class IosPostProcessBuild
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget != BuildTarget.iOS) return;

            var settings = AssetDatabase.LoadAssetAtPath<ATTSettings>("Assets/Plugins/ATT/ATTSettings.asset");

            // add framework
            var projectPath = PBXProject.GetPBXProjectPath(buildPath);
            var pbx = new PBXProject();
            pbx.ReadFromFile(projectPath);
            var targetGUID = pbx.GetUnityFrameworkTargetGuid();
            pbx.AddFrameworkToProject(targetGUID, "AppTrackingTransparency.framework", false);
            pbx.WriteToFile(projectPath);

            // add info.plist
            var plistPath = Path.Combine(buildPath, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);
            plist.root.SetString("NSUserTrackingUsageDescription", settings.usageDescription);
            plist.WriteToFile(plistPath);
        }
    }
}