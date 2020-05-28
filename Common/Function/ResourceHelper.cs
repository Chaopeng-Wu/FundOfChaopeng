using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 用于获取Resources 文件夹下的资源
    /// </summary>
    public class ResourceHelper
    {
        private static Dictionary<string, string> map;
        static ResourceHelper()
        {
            map = new Dictionary<string, string>();
            CreateMap();
        }
        private static void CreateMap()
        {
            
            string content = ConfigReader.ReadConfigFile("ResMap.txt");
            ConfigReader.LoadConfigFile(content, MapBuild);
        }

        private static void MapBuild(string line)
        {
            string[] keyValue = line.Split('=');
            map.Add(keyValue[0], keyValue[1]);
        }
        /// <summary>
        /// 获取想要的资源，必须在Resources 文件夹下，必须是ResMap 中有记录的。Res 是资源的意思
        /// </summary>
        /// <typeparam name="T">获取的资源的类型</typeparam>
        /// <param name="ResName">资源名字</param>
        /// <returns></returns>
        public static T LoadRes<T>(string ResName) where T:UnityEngine.Object
        {
            if (!map.ContainsKey(ResName)) return null;
            return Resources.Load(map[ResName]) as T;
        }
    }
}