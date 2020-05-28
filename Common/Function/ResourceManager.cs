using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	/// <summary>
	/// 资源管理器
	/// </summary>
	public class ResourceManager
	{
        private static Dictionary<string, string> map;
        static ResourceManager()
        {
            map = new Dictionary<string, string>();
            string fileContent = ConfigReader.ReadConfigFile("ResMap.txt");
            ConfigReader.LoadConfigFile(fileContent, BuildMap);
        }

        private static void BuildMap(string line)
        {
            string[] keyValue = line.Split('=');
            map.Add(keyValue[0], keyValue[1]);
        }

        public static T Load<T>(string resName) where T :Object
        {
            //文件名 --> 路径
            if (!map.ContainsKey(resName)) return null;
            string path = map[resName];
            return Resources.Load<T>(path);
        }
	}
}