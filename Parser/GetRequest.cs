using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class GetRequest
    {
        HttpWebRequest _request;
        string _addres;

        public string Responce { get; set; }

        public GetRequest(string addres)
        {
            _addres = addres;
        }

        public void Run()
        {
            _request = (HttpWebRequest)WebRequest.Create(_addres);
            _request.Method = "GET";


            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null) Responce = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {

                
            }
           
        }
    }
}
