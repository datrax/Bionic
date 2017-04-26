using System;

namespace SimpleWEBServer
{
    class RequestParser
    {
        public string Method { get; set; } = "";

        public string FullUrl { get; set; } = "";
        public string Parametrs { get; set; } = "";

    
        public RequestParser(string source)
        {
            if (source.Substring(0, 4).ToLower() == "post")
            {
                Method = "post";
                Parametrs = source.Substring(source.IndexOf("\r\n\r\n")+4);
            }
            if (source.Substring(0, 4).ToLower() == "get ")
                Method = "get";
            source = source.Substring(source.IndexOf(" ") + 1);
            source = source.Substring(0, source.IndexOf(" "));

            if (source.IndexOf("?") > 0)
            {
                if (Method == "get")
                    Parametrs = source.Substring(source.IndexOf("?")+1);
                source = source.Substring(0, source.IndexOf("?"));
             
            }
            FullUrl = source;

        }

        public RequestParser(string method, string fullUrl, string parametrs)
        {
            Method = method;
            FullUrl = fullUrl;
            Parametrs = parametrs;
        }

        public override string ToString()
        {
            return String.Format(@"
Method:{0}
Url:{1}
Parametrs:{2}
",Method,FullUrl,Parametrs);
        }
    }
}
