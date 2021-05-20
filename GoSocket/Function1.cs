namespace GoSocket
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Host;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            String MainPath = Directory.GetCurrentDirectory();
            XElement xmlRoot = XElement.Load(MainPath.Replace(@"bin\Debug\net461",@"Resource\File.xml"));
            IEnumerable<XElement> lArea = xmlRoot.Descendants("area");

            int AreaEmpleado = 0;
            double salaryByArea = 0;

            foreach (XElement area in lArea)
            {
                IEnumerable<XElement> lEmployee = area.Descendants("employee");

                if (lEmployee.Count() > 2)
                    AreaEmpleado += 1;

                log.Info($"---------------------------------------------------------------------------");

                foreach (XElement emp in lEmployee)
                    salaryByArea += double.Parse(emp.Attribute("salary").Value);

                log.Info(String.Format("{0}|{1}", area.Element("name").Value , salaryByArea));
            }
            
            log.Info($"Nodos de tipo “<area>”: " + lArea.Count());
            log.Info($"Nodos de tipo “<area>” que tiene mas de 2 Empleados: " + AreaEmpleado);
            log.Info($"---------------------------------------------------------------------------");
        }
    }
}
