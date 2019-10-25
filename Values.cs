using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;

namespace BulkApiCall
{
    class Values
    {
        private Dictionary<int, string> _valuedict = new Dictionary<int, string>();
        private List<string> _units = new List<string>(){"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R",
            "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

        //, "!","@","#","$","%","^","&","*","(",")",
            //"-","_","=","+","\\","|","[","]","{","}",";",":","\'","\"","/","?",".",">",",","<","`","~"
        public Values()
        {
            int count = 0;

            foreach(string uno in _units)
            {
                foreach(string dos in _units)
                {
                    foreach(string tres in _units)
                    {
                        string value = uno + dos + tres;
                        _valuedict.Add(count, value);
                        count++;
                    }
                }
            }
        }

        public string GetValue(int place)
        {
            return _valuedict[place];
        }

        public int Length
        {
            get { return _valuedict.Count; }
        }

        public void RunValues(string value)
        {
            //Console.WriteLine("https://test.api/verify-value/VerifySource?v=" + value);
            Task<string> newt = VerifyValue(value);
            newt.Wait();
            var resp = newt.Result;
            if (!resp.Contains("bad"))
                Console.WriteLine(resp + value);
        }

        private async Task<string> VerifyValue(string value)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync("https://test.api/verify-value/VerifySource?v=" + value);
                    response.EnsureSuccessStatusCode();

                    // return URI of the created resource.
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
        }
    }
}
