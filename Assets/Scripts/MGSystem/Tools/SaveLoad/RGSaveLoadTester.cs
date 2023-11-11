using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace MyGame.MGSystem
{
    /// <summary>
    /// A test object to store data to test the RGSaveLoadManager class
    /// </summary>
    [Serializable]
    public class RGSaveLoadTestObject
    {
        public string SavedText;
    }

    /// <summary>
    /// A simple class used in the RGSaveLoadTestScene to test the RGSaveLoadManager class
    /// </summary>
    public class RGSaveLoadTester : MonoBehaviour
    {
        [Header("Bindings")]
        /// the text to save
        public InputField TargetInputField;

        [Header("Save settings")]
        /// the chosen save method (json, encrypted json, binary, encrypted binary)
        public RGSaveLoadManagerMethods SaveLoadMethod = RGSaveLoadManagerMethods.Binary;
        /// the name of the file to save
        public string FileName = "TestObject";
        /// the name of the destination folder
        public string FolderName = "RGTest/";
        /// the extension to use
        public string SaveFileExtension = ".testObject";
        /// the key to use to encrypt the file (if needed)
        public string EncryptionKey = "ThisIsTheKey";

        /// Test button
        [RGInspectorButton("Save")]
        public bool TestSaveButton;
        /// Test button
        [RGInspectorButton("Load")]
        public bool TestLoadButton;
        /// Test button
        [RGInspectorButton("Reset")]
        public bool TestResetButton;

        protected IRGSaveLoadManagerMethod _saveLoadManagerMethod;

        /// <summary>
        /// Saves the contents of the TestObject into a file
        /// </summary>
        public virtual void Save()
        {
            InitializeSaveLoadMethod();
            RGSaveLoadTestObject testObject = new RGSaveLoadTestObject();
            testObject.SavedText = TargetInputField.text;
            RGSaveLoadManager.Save(testObject, FileName + SaveFileExtension, FolderName);

        }

        /// <summary>
        /// Loads the saved data
        /// </summary>
        public virtual void Load()
        {
            InitializeSaveLoadMethod();
            RGSaveLoadTestObject testObject = (RGSaveLoadTestObject)RGSaveLoadManager.Load(typeof(RGSaveLoadTestObject), FileName + SaveFileExtension, FolderName);
            TargetInputField.text = testObject.SavedText;
        }

        /// <summary>
        /// Resets all saves by deleting the whole folder
        /// </summary>
        protected virtual void Reset()
        {
            RGSaveLoadManager.DeleteSaveFolder(FolderName);
        }

        /// <summary>
        /// Creates a new RGSaveLoadManagerMethod and passes it to the RGSaveLoadManager
        /// </summary>
        protected virtual void InitializeSaveLoadMethod()
        {
            switch (SaveLoadMethod)
            {
                case RGSaveLoadManagerMethods.Binary:
                    _saveLoadManagerMethod = new RGSaveLoadManagerMethodBinary();
                    break;
                case RGSaveLoadManagerMethods.BinaryEncrypted:
                    _saveLoadManagerMethod = new RGSaveLoadManagerMethodBinaryEncrypted();
                    (_saveLoadManagerMethod as RGSaveLoadManagerEncrypter).Key = EncryptionKey;
                    break;
                case RGSaveLoadManagerMethods.Json:
                    _saveLoadManagerMethod = new RGSaveLoadManagerMethodJson();
                    break;
                case RGSaveLoadManagerMethods.JsonEncrypted:
                    _saveLoadManagerMethod = new RGSaveLoadManagerMethodJsonEncrypted();
                    (_saveLoadManagerMethod as RGSaveLoadManagerEncrypter).Key = EncryptionKey;
                    break;
            }
            RGSaveLoadManager.saveLoadMethod = _saveLoadManagerMethod;

        }
    }
}
