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
    //    // 获取文件路径
    //    var filePath = metaFilePath.Replace(".meta", "");
    //    Debug.Log(filePath);

    //    // 获取文件后缀
    //    switch (Path.GetExtension(filePath))
    //    {
    //        case ".cs": AddNameSpace(filePath); break;
    //        case ".txt":
    //            {
    //                var fileName = Path.GetFileNameWithoutExtension(filePath);
    //                // 如果是以 .lua.txt 结尾的话需要删除文件内的 .lua

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
    ///// 添加命名空间
    ///// </summary>
    ///// <param name="filePath"></param>
    //public static void AddNameSpace(string filePath)
    //{

    //    // 获取目录路径 基于Assets/...
    //    var path = Path.GetDirectoryName(filePath);
    //    // 获取所有的目录
    //    var names = path.Split(Path.DirectorySeparatorChar).ToList();
    //    bool gotNamespace = false;
    //    // 倒序遍历删除不需要的文件目录
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
    //        // 去掉文件可能的空格
    //        names[index] = name.Replace(" ", "");
    //        gotNamespace = true;
    //    }
    //    if (names.Count == 0)
    //    {
    //        // 设置默认名
    //        names.Add(PlayerSettings.productName);
    //    }
    //    // 得到命名空间之后替换
    //    var nameSpace = string.Join('.', names);
    //    nameSpace = nameSpace.Insert(0, Constants.PROJECT_NAMESPACE + '.');
    //    var fileContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, filePath));

    //    File.WriteAllText(filePath, fileContents.Replace("#NAMESPACE#", nameSpace));
    //    AssetDatabase.Refresh();
    //}

    ///// <summary>
    ///// 移除名称中 .lua 后缀
    ///// </summary>
    ///// <param name="filePath"></param>
    //public static void RemoveDotLua(string filePath)
    //{
    //    var fileContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, filePath));
    //    File.WriteAllText(filePath, fileContents.Replace(".lua", ""));
    //}
}
