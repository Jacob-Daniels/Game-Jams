using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using System.IO;

public class LaunchVN : MonoBehaviour
{
    public bool Hit = false;
    // Start is called before the  first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Hit == false)
        {
            Hit = true;
            Process vn = new Process();

            try
            {
                vn.StartInfo.UseShellExecute = false;

                if (Application.isEditor)
                {
                    vn.StartInfo.FileName = Directory.GetCurrentDirectory() + "/VN/VNDISTS/PC/BlackAndWhiteJam8VN.exe";
                }
                else if(Application.isEditor == false)
                {
                    vn.StartInfo.FileName = Directory.GetCurrentDirectory() + "/BlackAndWhiteJam8_Data/VN/VNDISTS/PC/BlackAndWhiteJam8VN.exe";
                }

                vn.Start();
                Application.Quit();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
            }
        }
    }
}
