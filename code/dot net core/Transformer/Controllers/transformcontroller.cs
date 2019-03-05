using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Xsl;
using Transformer.Models;

namespace Transformer.Controllers
{
    [Route("api/[controller]")]
    public class transformcontroller : Controller
    {
      
        // POST api/values
        [HttpPost]
        public string Post([FromBody] transformmodel value) 
        {
            // XslCompiledTransform objXSLTransform = new XslCompiledTransform();
            //objXSLTransform.Load(value.xslt);
            try
            {
                Console.WriteLine(value.xslt.ToString());
                Console.WriteLine(value.input.ToString());

                XslCompiledTransform proc = new XslCompiledTransform();
                using (StringReader sr = new StringReader(value.xslt.ToString()))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        proc.Load(xr);
                    }
                }
                using (StringReader sr = new StringReader(value.input.ToString()))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        using (StringWriter sw = new StringWriter())
                        {
                            proc.Transform(xr, null, sw);
                            return sw.ToString();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

      
    }
}
