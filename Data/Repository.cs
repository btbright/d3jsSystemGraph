using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SETArchitecture.Data.Objects;

namespace SETArchitecture.Data
{
    class Repository
    {
        #region Dependency setup and class initialization

        private SETArchitectureTestEntities _entities;


        public Repository()
        {
            _entities = new SETArchitectureTestEntities();
        }
        #endregion

        public IEnumerable<Server> GetServers()
        {
            return _entities.Servers.DefaultIfEmpty();
        } 

        public IEnumerable<Application> GetApplications()
        {
            return _entities.Applications.DefaultIfEmpty();
        }

        public IEnumerable<ApplicationType> GetApplicationTypes()
        {
            return _entities.ApplicationTypes.DefaultIfEmpty();
        }

        public IEnumerable<ServerType> GetServerTypes()
        {
            return _entities.ServerTypes.DefaultIfEmpty();
        }

        public void InsertServer(Server source)
        {
            _entities.AddToServers(source);
        }

        public void InsertApplication(Application source)
        {
            _entities.AddToApplications(source);
        }

        public void InsertApplicationType(ApplicationType source)
        {
            _entities.AddToApplicationTypes(source);
        }

        public void InsertServerType(ServerType source)
        {
            _entities.AddToServerTypes(source);
        }
        
    }
}
