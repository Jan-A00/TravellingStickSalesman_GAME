using System;
using UnityEngine;

namespace Toolbox
{
    // Because Unity have taken over 5 years to add this basic feature. I have to use a wrapper like this...
    // https://forum.unity.com/threads/how-to-load-an-array-with-jsonutility.375735/ - 5 Years ago
    // https://forum.unity.com/threads/working-with-top-level-json-arrays-in-unity.1002219/ - Still not able to do this.
    public static class JsonHelper
    {
        public static string WriteJsonArray<T>(T[] arrayObjects)
        {
            string jsonArray = JsonUtility.ToJson(arrayObjects);
            string jsonToWrite = "{ \"array\": ";
            return jsonArray;
        }
        public static T[] ReadJsonArray<T>(string originalJson)
        {
            string newJson = "{ \"array\": " + originalJson + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
            return wrapper.array;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;

            public Wrapper(T[] array)
            {
                this.array = array;
            }
        }
    }
}