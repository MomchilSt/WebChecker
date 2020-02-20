using System;
using System.Net;
using WebChecker.Models;
using System.Threading;
using System.IO;

namespace WebChecker
{
    public class StartUp
    {
        static void Main()
        {
            while (true)
            {
                var input = Console.ReadLine();
                var responseModel = WebSiteIsAvailable(input);

                if (!responseModel.IsAlive)
                {
                    Console.WriteLine("down");
                    Console.WriteLine(responseModel.ErrorMsg);
                    //MessageService.SendEmail($"{} is down - Date: {responseModel.TimeOfCrash}");
                }
                else if (responseModel.Content.Contains("erişimin engellenmesi kararı"))
                {
                    Console.WriteLine("blocked");
                    //MessageService.SendEmail($"{} is blocked! - Date: {DateTime.Now}");
                }
                //Thread.Sleep(60000);
                //int contentWhenBlocked = 1963;
            }
        }
        

        public static ResponseModel WebSiteIsAvailable(string Url)
        {
            var responseModel = new ResponseModel();
            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);

            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    responseModel.ContentLength = response.ContentLength;
                    responseModel.IsAlive = true;
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    responseModel.Content = sr.ReadToEnd();
                    responseModel.CookiesCount = response.Cookies.Count;
                    sr.Close();
                }
            }
            catch (WebException ex)
            {
                responseModel.IsAlive = false;
                responseModel.TimeOfCrash = DateTime.Now;
                responseModel.ErrorMsg = ex.Message;
            }

            return responseModel;
        }
    }
}

