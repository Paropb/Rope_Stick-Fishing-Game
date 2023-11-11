using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Security.Cryptography;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class RGSaveLoadManagerMethodJsonEncrypted : RGSaveLoadManagerEncrypter, IRGSaveLoadManagerMethod
    {
        /// <summary>
        /// Saves the specified object at the specified location to disk, converts it to json and encrypts it
        /// </summary>
        /// <param name="objectToSave"></param>
        /// <param name="saveFile"></param>
        public void Save(object objectToSave, FileStream saveFile)
        {
            string json = JsonUtility.ToJson(objectToSave);
            // if you prefer using NewtonSoft's JSON lib uncomment the line below and commment the line above
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectToSave);
            /*
           using (Class1 cls1 = new Class1(), cls2 = new Class2())
           {
               // the code using cls1, cls2
           } // call the Dispose on cls1 and cls2
           */
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(memoryStream))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                memoryStream.Position = 0;
                Encrypt(memoryStream, saveFile, Key);
            }
            saveFile.Close();
        }

        /// <summary>
        /// Loads the specified file, decrypts it and decodes it
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="saveFile"></param>
        /// <returns></returns>
        public object Load(Type objectType, FileStream saveFile)
        {
            object savedObject = null;
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader streamReader = new StreamReader(memoryStream))
            {
                try
                {
                    Decrypt(saveFile, memoryStream, Key);
                }
                catch (CryptographicException ce)
                {
                    Debug.LogError("[RGSaveLoadManager] Encryption key error: " + ce.Message);
                    return null;
                }
                memoryStream.Position = 0;
                savedObject = JsonUtility.FromJson(streamReader.ReadToEnd(), objectType);
                // if you prefer using NewtonSoft's JSON lib uncomment the line below and commment the line above
                //savedObject = Newtonsoft.Json.JsonConvert.DeserializeObject(sr.ReadToEnd(), objectType); 
            }
            saveFile.Close();
            return savedObject;
        }
    }
}
