//using Microsoft.Win32.SafeHandles;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using Teakisland.Tools;
//using UnityEngine;

///// <summary>
///// 这个类成功将选择的一个文件复制到了指定文件夹下，开心！
///// </summary>
//public class IOStreamTest : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {    

//        string from = FileBrowser.OpenFileWin("请选择要复制的文件");
//        string to = FileBrowser.OpenDirectoryWin("请选择要复制到的路径");
//        //--------------获取被选择文件的文件名，带文件后缀哦。-----------------------------
//        string[] part = from.Split('\\');
//        string fileName = part[part.Length - 1];
//        //--------------将文件名与选择的文件夹合成完整的路径----------------------------------
//        to = Path.Combine(to, fileName);
//        //--------------将文件读取出来然后写入上面合成的完整路径中------------------------------
//        using (FileStream str = new FileStream(from, FileMode.Open, FileAccess.Read))
//        {            
//            using(BinaryReader r = new BinaryReader(str))
//            {
//                byte[] masa = r.ReadBytes((int.Parse(str.Length.ToString())));
//                print(masa.Length);
//                using(FileStream s = new FileStream(to, FileMode.CreateNew))
//                {
//                    using(BinaryWriter w = new BinaryWriter(s))
//                    {
//                        w.Write(masa);

//                    }
//                }
//            }
//        }

//    }


//}
