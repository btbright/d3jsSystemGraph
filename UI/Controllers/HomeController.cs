using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SETArchitecture.Data.Objects;
using SETArchitecture.UI.Models;

namespace SETArchitecture.UI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HelloWorld()
        {
            return View();
        }

        public ActionResult ApplicationGraph()
        {
            #region ServerTypes

            var webServer = new ServerType {TypeName = "Web",ServerTypeID = 1};
            var dbServer = new ServerType { TypeName = "DB", ServerTypeID = 2 };

            #endregion

            #region Servers

            var Web2 = new Server
                           {
                               ServerID = 1,
                               ServerName = "WEB2",
                               ServerType = webServer
                           };
            var web9 = new Server
                               {
                                   ServerID = 2,
                                   ServerName = "WEB9",
                                   ServerType = webServer
                               };
            var db15 = new Server
                               {
                                   ServerID = 3,
                                   ServerName = "DB15",
                                   ServerType = dbServer
                               };
            var atlsetweb1 = new Server
            {
                ServerID = 4,
                ServerName = "atl-setweb1",
                ServerType = webServer
            };

            var atlsetweb2 = new Server
            {
                ServerID = 5,
                ServerName = "atl-setweb2",
                ServerType = webServer
            };

            var atlsetweb3 = new Server
            {
                ServerID = 6,
                ServerName = "atl-setweb3",
                ServerType = webServer
            };

            var atlsetweb4 = new Server
            {
                ServerID = 7,
                ServerName = "atl-setweb4",
                ServerType = webServer
            };

            var db8 = new Server
            {
                ServerID = 8,
                ServerName = "db8",
                ServerType = dbServer
            };

            var creditAppDB = new Server
            {
                ServerID = 9,
                ServerName = "CreditAppDB",
                ServerType = dbServer
            };

            #endregion

            #region ApplicationTypes

            var DotNetWebForms = new ApplicationType
                                     {
                                         TypeName = ".NET WebForms",
                                         ApplicationTypeID = 1
                                     };

            var DotNetMVC = new ApplicationType
            {
                TypeName = ".NET MVC",
                ApplicationTypeID = 2
            };

            var ClassicASP = new ApplicationType
                                 {
                                     TypeName = "Classic ASP",
                                     ApplicationTypeID = 3
                                 };

            var Webservice = new ApplicationType
            {
                TypeName = "Web Service",
                ApplicationTypeID = 4
            };

            #endregion

            #region Applications

            var oldBAT = new Application
                             {
                                 ApplicationID = 1,
                                 Name = "Old BAT",
                                 ApplicationType = DotNetWebForms
                             };


            var oldBATConfigurator = new Application
            {
                ApplicationID = 2,
                Name = "OldBAT Configurator",
                ApplicationType = ClassicASP
            };

            var newBAT = new Application
                             {
                                 ApplicationID = 3,
                                 Name = "NewBAT",
                                 ApplicationType = DotNetMVC
                             };

            var ice = new Application
            {
                ApplicationID = 4,
                Name = "ICE",
                ApplicationType = DotNetWebForms
            };

            var ipa = new Application
            {
                ApplicationID = 5,
                Name = "IPA",
                ApplicationType = DotNetWebForms
            };

            var SETLeads = new Application
            {
                ApplicationID = 6,
                Name = "SET Leads",
                ApplicationType = Webservice
            };


            #endregion

            #region Application Dependencies

            var applicationDeps = new List<ApplicationDependencies>();

            applicationDeps.Add(new ApplicationDependencies
            {
                Application = newBAT,
                Servers = new List<Server> { db15, atlsetweb1, atlsetweb2, atlsetweb3, atlsetweb4 }
            });

            applicationDeps.Add(new ApplicationDependencies
            {
                Application = oldBAT,
                Servers = new List<Server> { db15, web9, Web2 }
            });

            applicationDeps.Add(new ApplicationDependencies
            {
                Application = oldBATConfigurator,
                Servers = new List<Server> { db15, web9, Web2, db8 }
            });

            applicationDeps.Add(new ApplicationDependencies
            {
                Application = ice,
                Servers = new List<Server> { db15, web9, Web2 }
            });

            applicationDeps.Add(new ApplicationDependencies
            {
                Application = ipa,
                Servers = new List<Server> { db15, web9, Web2 }
            });

            applicationDeps.Add(new ApplicationDependencies
            {
                Application = SETLeads,
                Servers = new List<Server> { db15, atlsetweb2, creditAppDB }
            });

            #endregion

            var nodes = new List<Node>();

            nodes = AddApplicationsToNodeList(new List<Application>
                                                  {
                                                      oldBAT,
                                                      newBAT,
                                                      oldBATConfigurator,
                                                      ice,
                                                      ipa,
                                                      SETLeads
                                                  },nodes );

            nodes = AddServersToNodeList(new List<Server>
                                             {
                                                 web9, 
                                                 Web2, 
                                                 db15, 
                                                 atlsetweb1, 
                                                 atlsetweb2, 
                                                 atlsetweb3, 
                                                 atlsetweb4,
                                                 db8,
                                                 creditAppDB
                                             }, nodes);


            var links = new List<Link>();

            var count = 0;
            foreach (var dep in applicationDeps)
            {
                links.AddRange(dep.Servers.Select(serv => new Link
                    {
                        Source = nodes.Where(a => a.Name == dep.Application.Name).Select(a => a.Index).FirstOrDefault(), 
                        Target = nodes.Where(a => a.Name == serv.ServerName).Select(a => a.Index).FirstOrDefault(),
                        Value = 1,
                        Weight = 1
                    }));
            }

            var jsonObj = new {nodes = nodes, links = links};
            var serializer = new JavaScriptSerializer();
            var model = new ApplicationGraphModel {json = serializer.Serialize(jsonObj).ToLower()};

            return View(model);
        }

        public List<Node> AddServersToNodeList(List<Server> serverList, List<Node> nodeList  )
        {
            foreach (var server in serverList)
            {
                nodeList.Add(new Node
                                 {
                                     Name = server.ServerName,
                                     Group = server.ServerType.ServerTypeID,
                                     Index = nodeList.Count,
                                     Type = "Server",
                                     Weight = 1,
                                     GroupName = server.ServerType.TypeName
                                 });
            }
            return nodeList;
        }

        public List<Node> AddApplicationsToNodeList(List<Application> applicationList, List<Node> nodeList)
        {
            foreach (var application in applicationList)
            {
                nodeList.Add(new Node
                {
                    Name = application.Name,
                    Group = application.ApplicationType.ApplicationTypeID,
                    Index = nodeList.Count,
                    Type = "Application",
                    Weight = 1,
                    GroupName = application.ApplicationType.TypeName
                });
            }
            return nodeList;
        }
    }
}
