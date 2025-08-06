using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json.Linq; // 用于解析 JSON

namespace FuturesWatcher
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var config = YamlConfigLoader.LoadConfig();
            var timeout = config.timeout;
            var startup = config.startup;
            var futures = config.futures;
            var proxy = config.proxy;
            var port = config.port;

            AutoStartHelper.SetAutoStart(startup);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var floatForm = new FloatingNumberForm("123.45");

            // 在新线程中执行请求逻辑
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        var request = (HttpWebRequest)WebRequest.Create(
                            $"https://fapi.binance.com/fapi/v1/premiumIndex?symbol={futures}"
                        );
                        request.Proxy = new WebProxy(proxy, port);
                        request.Method = "GET";
                        request.Timeout = 5000;

                        using (var response = (HttpWebResponse)request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        using (var reader = new StreamReader(stream))
                        {
                            var result = reader.ReadToEnd();

                            // 解析 JSON 中的 "markPrice"
                            var json = JObject.Parse(result);
                            var price = json["markPrice"]?.ToString();
                            var raw = json["markPrice"]?.ToString() ?? "0";

                            if (decimal.TryParse(raw, out var num))
                            {
                                price = num.ToString("F2");
                            }
                            else
                            {
                                price = "错误数据";
                            }


                            if (price != null)
                            {
                                // 在主线程更新窗体文字
                                floatForm.Invoke(new Action(() =>
                                {
                                    floatForm.UpdateNumber(price, Color.LimeGreen);
                                }));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("请求错误: " + ex.Message);
                    }

                    Thread.Sleep(timeout); // 每次等待 timeout 毫秒
                }
            }).Start();

            Application.Run(floatForm); // 主线程运行窗体
        }
    }
}
