using System.Net.Mime;
using System.Linq;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using MyGame.MGSystem;

[ExecuteAlways]
// ReSharper disable once CheckNamespace
public class ScriptTemplateFixer : UnityEditor.AssetModificationProcessor
{
    //public static void OnWillCreateAsset(string metaFilePath)
    //{
    //    // ��ȡ�ļ�·��
    //    var filePath = metaFilePath.Replace(".meta", "");
    //    Debug.Log(filePath);

    //    // ��ȡ�ļ���׺
    //    switch (Path.GetExtension(filePath))
    //    {
    //        case ".cs": AddNameSpace(filePath); break;
    //        case ".txt":
    //            {
    //                var fileName = Path.GetFileNameWithoutExtension(filePath);
    //                // ������� .lua.txt ��β�Ļ���Ҫɾ���ļ��ڵ� .lua

    //                if (Path.GetExtension(fileName) == ".lua")
    //                {
    //                    RemoveDotLua(filePath);
    //                }

    //                break;
    //            }
    //        default: break;
    //    }
    //}


    ///// <summary>
    ///// ��������ռ�
    ///// </summary>
    ///// <param name="filePath"></param>
    //public static void AddNameSpace(string filePath)
    //{

    //    // ��ȡĿ¼·�� ����Assets/...
    //    var path = Path.GetDirectoryName(filePath);
    //    // ��ȡ���е�Ŀ¼
    //    var names = path.Split(Path.DirectorySeparatorChar).ToList();
    //    bool gotNamespace = false;
    //    // �������ɾ������Ҫ���ļ�Ŀ¼
    //    for (int index = 0; index < names.Count; index++)
    //    {
    //        var name = names[index];
    //        if (name == "Assets" || name == "Scripts")
    //        {
    //            names.RemoveAt(index);
    //            index--;
    //            continue;
    //        }
    //        else if(gotNamespace)
    //        {
    //            names.RemoveAt(index);
    //            index--;
    //            continue;
    //        }
    //        // ȥ���ļ����ܵĿո�
    //        names[index] = name.Replace(" ", "");
    //        gotNamespace = true;
    //    }
    //    if (names.Count == 0)
    //    {
    //        // ����Ĭ����
    //        names.Add(PlayerSettings.productName);
    //    }
    //    // �õ������ռ�֮���滻
    //    var nameSpace = string.Join('.', names);
    //    nameSpace = nameSpace.Insert(0, Constants.PROJECT_NAMESPACE + '.');
    //    var fileContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, filePath));

    //    File.WriteAllText(filePath, fileContents.Replace("#NAMESPACE#", nameSpace));
    //    AssetDatabase.Refresh();
    //}

    ///// <summary>
    ///// �Ƴ������� .lua ��׺
    ///// </summary>
    ///// <param name="filePath"></param>
    //public static void RemoveDotLua(string filePath)
    //{
    //    var fileContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, filePath));
    //    File.WriteAllText(filePath, fileContents.Replace(".lua", ""));
    //}
}
