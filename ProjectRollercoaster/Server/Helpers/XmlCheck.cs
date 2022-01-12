using System.ServiceModel.Syndication;
using System.Xml;

namespace ProjectRollercoaster.Server.Helpers
{
    public class XmlCheck
    {
        public async Task<string> DoesXmlExist(string urlTest)
        {
            try
            {
                var reader = XmlReader.Create(urlTest);
                var rssfeed = SyndicationFeed.Load(reader);

            }
            catch (Exception)
            {
                return "failure";
            }
            return "success";
        }
    }
}
