using System;
using UnityEngine;

namespace Common
{
    public class MyDebug : MonoBehaviour
    {
	     public static void Try(Action methode)
        {
            try
            {
                methode();
            }
            catch (Exception)
            {

                throw;
            }
        }
  
    }
}
 