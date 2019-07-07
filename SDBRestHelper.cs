using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Extension
{
    class SDBRestHelper
    {
        //request = WebRequest.Create("http://192.168.100.61:11814/application/x-www-form-urlencoded;charset=UTF-8?cmd=query&name=jeecg.t_dycl_project&sort={\"project_name\":1}&skip=0&returnnum=3&filter={\"create_name\":\"刘钦泉\"}") as HttpWebRequest;
        //服务器，ip或者机器名，或者域名，构建服务器池
        public String Server = "192.168.100.61";
        //服务器端口号
        public int Port = 11814;
        public String CharSet = "UTF-8";
        private String CommonString = "/application/x-www-form-urlencoded";
        public String CommandString = "";
        public String CommandStringBase = "";
        //集合空间
        public String CollectionSpace="Test";
        //集合
        public String Collection="Test";
        //错误代码
        public int errno = 0;

        public void BuildCommandStringBase()
        {
            CommandStringBase += "http://";
            CommandStringBase += Server;
            CommandStringBase += ":";
            CommandStringBase += Port;
            CommandStringBase += CommonString;
            CommandStringBase += ";charset=";
            CommandStringBase += CharSet;
            CommandStringBase += "?cmd=";
            Console.WriteLine("CommandStringBase: " + CommandStringBase);
        }

        /// <summary>
        /// 类的构造函数
        /// </summary>
        public SDBRestHelper()
        {
            BuildCommandStringBase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        public SDBRestHelper(String server)
        {
            this.Server =server;
            BuildCommandStringBase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public SDBRestHelper(String server, int port)
        {
            this.Server = server;
            this.Port = port;
            BuildCommandStringBase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="collectionSpace"></param>
        public SDBRestHelper(String server, int port, String collectionSpace)
        {
            this.Server = server;
            this.Port = port;
            this.CollectionSpace = collectionSpace;
            BuildCommandStringBase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="collectionSpace"></param>
        /// <param name="collection"></param>
        public SDBRestHelper(String server, int port, String collectionSpace,String collection)
        {
            this.Server = server;
            this.Port = port;
            this.CollectionSpace = collectionSpace;
            this.Collection = collection;
            BuildCommandStringBase();
        }

        public String ExecuteCommand()
        {
            String ReturnStr = "";

            HttpWebRequest request = WebRequest.Create(this.CommandString) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string jsonText = reader.ReadToEnd();
                //MessageBox.Show(jsonText);
                Console.WriteLine(jsonText);

                /*
                string seperate = "#***?#";
                jsonText = jsonText.Replace("}{", "}" + seperate + "{");

                String[] jText = jsonText.Split(new string[] { seperate }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < jText.Length; i++)
                {
                    Console.WriteLine(jText[i]);
                }

                //JObject jo = (JObject)JsonConvert.DeserializeObject(jText[1]);
                //string projectName = jo["project_detail_name"].ToString();
                //MessageBox.Show(projectName);
                String tmpStr = jsonText.Substring(11, Math.Min(jsonText.IndexOf("}"), jsonText.IndexOf(",")) - 11);
                */

                errno = Convert.ToInt32(jsonText.Substring(11,Math.Min(jsonText.IndexOf("}"),jsonText.IndexOf(",")) - 11));
                Console.WriteLine("errono: " + errno);
                if (jsonText.Contains("}{"))
                {
                    ReturnStr = jsonText.Substring(jsonText.IndexOf("}{") + 2);
                    Console.WriteLine("returnstr: " + ReturnStr);
                }
            }
            return ReturnStr;
        }

        public void CreateCollectionSpace()
        {
            this.CommandString = this.CommandStringBase + "create collectionspace&name=";
            this.CommandString += this.CollectionSpace;
            ExecuteCommand();
        }
        public void CreateCollectionSpace(String collectionSpace)
        {
            this.CommandString = this.CommandStringBase + "create collectionspace&name=";
            this.CommandString += collectionSpace;
            ExecuteCommand();
        }

        public void DropCollectionSpace()
        {
        }

        public void CreateCollection()
        {
        }

        public void DropCollection()
        {
        }
    }
}
