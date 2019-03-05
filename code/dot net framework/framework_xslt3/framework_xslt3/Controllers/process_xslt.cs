using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

public class transformmodel
{
    public String input;
    public String xslt;
}

namespace framework_xslt3.Controllers
{
    public class xsltController : ApiController
    {

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post([FromBody] transformmodel value)
        {
            // XslCompiledTransform objXSLTransform = new XslCompiledTransform();
            //objXSLTransform.Load(value.xslt);
            try
            {
                //Console.WriteLine(value.xslt.ToString());
                //Console.WriteLine(value.input.ToString());

                XslCompiledTransform proc = new XslCompiledTransform();
                using (StringReader sr = new StringReader(value.xslt.ToString()))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        proc.Load(xr);
                    }
                }
                using (StringReader sr = new StringReader(System.Net.WebUtility.HtmlDecode(value.input.ToString())))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        using (StringWriter sw = new StringWriter())
                        {
                            proc.Transform(xr, null, sw);
                            var hrm= new HttpResponseMessage(HttpStatusCode.OK);
                            hrm.Content = new StringContent(sw.ToString(), System.Text.Encoding.UTF8, "text/html"); 
                            return hrm;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                hrm.Content = new StringContent(ex.ToString(), System.Text.Encoding.UTF8, "text/html");
                return hrm; 
            }
        }


    }
}
