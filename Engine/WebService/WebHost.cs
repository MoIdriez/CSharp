using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace Engine.WebService
{
    public class WebHost
    {
        private readonly ServiceHost _host;
        public WebHost(string uri)
        {
            Uri hostUri = new Uri(uri);
            // Create the ServiceHost.
            _host = new ServiceHost(typeof(Service), hostUri);
            
            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
            };
            _host.Description.Behaviors.Add(smb);

            //host.AddServiceEndpoint(typeof(IService), new BasicHttpBinding(), "Soap");

            ServiceEndpoint endpoint = _host.AddServiceEndpoint(typeof(IService), new WebHttpBinding(), "Web");
            endpoint.Behaviors.Add(new WebHttpBehavior());
            
        }

        public void Run()
        {
            _host.Open();
        }

        public void Stop()
        {
            _host.Close();
        }

        [ServiceContract]
        public interface IService
        {

            [OperationContract]
            [WebGet]
            string Test();

            [OperationContract]
            [WebGet]
            int AddTest(int x, int y);

            [OperationContract]
            [WebGet]
            string SayHello(string name);
        }

        public class Service : IService
        { 
            public string Test()
            {
                return "Test call received";
            }

            public int AddTest(int x, int y)
            {
                return x + y;
            }
            public string SayHello(string name)
            {
                Console.WriteLine($"Received: '{name}'.");
                return $"Hello, {name}";
            }
        }
    }
}
