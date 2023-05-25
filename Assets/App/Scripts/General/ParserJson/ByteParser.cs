using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

namespace App.Scripts.General.ByteParser
{
    public class ByteParser<T> where T : class, new()
    {
        private T _data;

        public ByteParser() 
        {
            _data = new T();
        }

        public void SaveDataToFile(T dataClass, string path)
        {
            FileInfo file = new FileInfo(path);
            file.Directory?.Create();

            string json = JsonConvert.SerializeObject(dataClass, Formatting.Indented);
            byte[] buffer = ObjectToByteArray(dataClass);

            try
            {
                File.WriteAllText(path.Replace("txt", "json"), json);
                File.WriteAllBytes(path, buffer);
                
                Debug.Log("Saved");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public T LoadDataFromFile(string path)
        {
            try
            {
                byte[] buffer = File.ReadAllBytes(path);

                _data = ByteArrayToObject(buffer);
                
                Debug.Log("Loaded");
            }
            catch (Exception e)
            {
                _data = null;
                Debug.LogWarning(e.Message);
            }

            return _data;
        }
        
        private byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }
        
        private T ByteArrayToObject(byte[] buffer)
        {
            T resultObject;
            
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                resultObject = (T) binaryFormatter.Deserialize(memoryStream);
            }

            return resultObject;
        }
    }
}
