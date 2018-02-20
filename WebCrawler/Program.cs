using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.IO;


namespace WebCrawler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            StreamData Data = new StreamData();
            foreach (string link in Data.URLList)
            {
                Data.URL = link;
                SourceCode(Data);
                //Visit(Data);
                Write(Data);
            }
            Read(Data);
        }

        
            static void SourceCode(StreamData Data)
            {
                Req(Data);
                Res(Data);
            }

            static void Req(StreamData Data)
            {
                Data.RequestStream = (HttpWebRequest)WebRequest.Create(Data.URL);
            }

            static void Res(StreamData Data)
            {

                Data.ResponseStream = (HttpWebResponse)Data.RequestStream.GetResponse();

                if (Data.ResponseStream.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = Data.ResponseStream.GetResponseStream();
                    StreamReader readStream = null;

                    if (Data.ResponseStream.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(Data.ResponseStream.CharacterSet));
                    }

                    Data.Response = readStream.ReadToEnd();

                    Data.ResponseStream.Close();
                    readStream.Close();

                }
            }

            static void Visit(StreamData Data)
            {
                Data.VisitRequestStream = WebRequest.Create(Data.URL);
                Data.VisitResponseStream = Data.VisitRequestStream.GetResponse();

                Stream streamResponse = Data.VisitResponseStream.GetResponseStream();
                StreamReader sreader = new StreamReader(streamResponse);
                Data.Response = sreader.ReadToEnd();
                GetVisitContent(Data);


                streamResponse.Close();
                sreader.Close();
                Data.VisitResponseStream.Close();
            }

            static void GetVisitContent(StreamData Data)
            {
            String sString = "";
            HtmlDocument d = new HtmlDocument();
            HtmlDocument doc = (HtmlDocument)d;
            doc.write(Rstring);

            HtmlElementCollection L = doc.links;

            foreach (HtmlElement links in L)
            {
                sString += links.getAttribute("href", 0);
                sString += "/n";
            }
            return sString;
        }

            static void Read(StreamData Data)
            {
                Console.Read();
            }

            static void Write(StreamData Data)
            {
                int x = Console.WindowWidth;
                Console.WriteLine(Data.Response);
                Console.WriteLine();
                Console.WriteLine();
                string outputBreak = "";
                for (int i = x; i > 0; i--)
                {
                    outputBreak += "-";
                }
                Console.WriteLine(outputBreak);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public class StreamData
        {
            public HttpWebRequest RequestStream { get; set; }
            public HttpWebResponse ResponseStream { get; set; }
            public WebResponse VisitResponseStream { get; set; }
            public WebRequest VisitRequestStream { get; set; }
            public string Response { get; set; }
            public string URL { get; set; }
            public string[] URLList = { "https://www.duckduckgo.com", "https://google.com" };
            public class School
            {
                public string Name { get; set; }
                public string BaseURL { get; set; }
                public class Contact
                {
                    public string Name { get; set; }
                    public string Title { get; set; }
                    public string URL { get; set; }
                }
            }
        }
    }
