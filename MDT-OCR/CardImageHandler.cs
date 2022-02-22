using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace MDT_OCR
{
    public class CardImageHandler
    {
        private static readonly string OcrApiUrl = "http://127.0.0.1:5000/getcardname";
        public static Bitmap CaptureImage(Bitmap bmp, int width, int height, int spaceX, int spaceY)
        {
            int x = 0;
            int y = 0;
            int sX = bmp.Width - width;
            int sY = bmp.Height - height;
            if (sX > 0)
            {
                x = sX > spaceX ? spaceX : sX;
            }
            else
            {
                width = bmp.Width;
            }
            if (sY > 0)
            {
                y = sY > spaceY ? spaceY : sY;
            }
            else
            {
                height = bmp.Height;
            }
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphic = Graphics.FromImage(bitmap);
            graphic.DrawImage(bmp, 0, 0, new System.Drawing.Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            Bitmap saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            bitmap.Dispose();
            graphic.Dispose();
            return saveImage;
        }
        private static string ToBase64(Bitmap bmp)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                return strbaser64;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetCardName(int x,int y,int width,int height)
        {
            Bitmap bmp = NativeMethodEx.CaptureScreen(x, y, width, height);
            //bmp.Save("1.png");
            string base64String = ToBase64(bmp);
            bmp.Dispose();
            string result = string.Empty;
            if (base64String == string.Empty)
                return result;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(OcrApiUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new { img = base64String });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                httpResponse.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ocr_api未启动");
            }
            return result;
        }
    }
}

