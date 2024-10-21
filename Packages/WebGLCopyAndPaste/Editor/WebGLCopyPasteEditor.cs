using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
#if UNITY_2021_3_OR_NEWER
using UnityEditor.Build;
#endif

internal class WebGLCopyPasteEditor : EditorWindow
{
    const string Define = "WEBGL_COPY_AND_PASTE_SUPPORT_TEXTMESH_PRO";

    [MenuItem("Tools/WebGLCopyPaste/EnableTMPSupport")]
    public static void EnableTMPSupport()
    {
        EnableTMP();
    }

    private static void EnableTMP()
    {
        BuildTargetGroup target = BuildTargetGroup.WebGL;

#if UNITY_2021_3_OR_NEWER
        NamedBuildTarget namedTarget = NamedBuildTarget.FromBuildTargetGroup(target);
        string defines = PlayerSettings.GetScriptingDefineSymbols(namedTarget).Trim();
#else
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target).Trim();
#endif

        List<string> list = defines.Split(';', ' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        if (list.Contains(Define)) return;

        list.Add(Define);
        defines = list.Aggregate((a, b) => a + ";" + b);

#if UNITY_2021_3_OR_NEWER
        PlayerSettings.SetScriptingDefineSymbols(namedTarget, defines);
#else
        PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
#endif
    }

    private static bool IsObsolete(BuildTargetGroup group)
    {
        var attrs = typeof(BuildTargetGroup)
            .GetField(group.ToString())
            .GetCustomAttributes(typeof(ObsoleteAttribute), false);

        return attrs != null && attrs.Length > 0;
    }
}