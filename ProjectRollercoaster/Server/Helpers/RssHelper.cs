using Microsoft.EntityFrameworkCore;
using ProjectRollercoaster.Server.Data;
using ProjectRollercoaster.Shared;
using System.Security.Claims;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProjectRollercoaster.Server.Helpers
{
    public class RssHelper

    {
        private readonly DataContext _context;

        public RssHelper(DataContext context)
        {
            _context = context;
        }

        public bool IsRssValid(string urlTest, int userId)
        {
            if (DoesRssExist(urlTest) && DoesRssExistInDb(urlTest, userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesRssExist(string urlTest)
        {
            try
            {
                var reader = XmlReader.Create(urlTest);
                var rssfeed = SyndicationFeed.Load(reader);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Feed AddRssInfo(string url, string feedName, string image, User user)
        {
            Feed feedObject = new();

            feedObject.Url = url;

            feedObject.Name = feedName;

            feedObject.Image = image;

            feedObject.User = user;

            return feedObject;
        }

        public List<List<FeedContent>> GetRssContent(List<Feed> listOfFeeds)
        {
            List<List<FeedContent>> ListWithListOfFeeds = new List<List<FeedContent>>();
            foreach (var feed in listOfFeeds)
            {
                List<FeedContent> ListOfFeedsWithContent = new List<FeedContent>();
                var reader = XmlReader.Create(feed.Url);
                var feedResponse = SyndicationFeed.Load(reader);
                var counter = 0;
                foreach (SyndicationItem item in feedResponse.Items)
                {
                    FeedContent feedObject = new();
                    List<string> LinkList = new();

                    try
                    {
                        feedObject.Id = feed.Id;
                        feedObject.Name = feed.Name;
                        feedObject.Title = item.Title.Text;
                        feedObject.Image = feed.Image;
                        feedObject.Summary = item.Summary.Text;
                        if (feedObject.Summary.Contains("img"))
                        {
                            feedObject.Picture = ExtractImg(item.Summary.Text);
                            feedObject.Summary = RemoveImgString(item.Summary.Text, feedObject);
                        }
                        if (feedObject.Summary.Contains("<p>") || feedObject.Summary.Contains("<br>") || feedObject.Summary.Contains("&nbsp") || feedObject.Summary.Contains("</br>") || feedObject.Summary.Contains("<") || feedObject.Summary.Contains(">") || feedObject.Summary.Contains("img"))
                        {
                            feedObject.Summary = RemoveTags(feedObject.Summary);
                        }

                        feedObject.PublishDate = item.PublishDate.ToString("yyyy/MM/dd HH:mm");

                        if (item.Links is not null)
                        {
                            foreach (var link in item.Links)
                            {
                                if (!link.Uri.AbsoluteUri.Contains("image"))
                                {
                                    LinkList.Add(link.Uri.AbsoluteUri);
                                }
                            }
                            feedObject.Links = LinkList;
                        }
                        ListOfFeedsWithContent.Add(feedObject);
                        counter++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    if (counter == 5)
                    {
                        break;
                    }
                }
                ListWithListOfFeeds.Add(ListOfFeedsWithContent);
            }
            return ListWithListOfFeeds;
        }

        private string ExtractImg(string text)
        {
            int firstIndex = text.IndexOf("https:");
            int lastIndex = text.IndexOf(".jpg");
            return text.Substring(firstIndex, lastIndex - 6);
        }

        private string RemoveImgString(string summaryText, FeedContent feedObject)
        {
            return summaryText.Replace(feedObject.Picture, "");
        }

        private string RemoveTags(string text)
        {
            var parsedString = text;
            string stringsToRemove = "<p>|</p>|<br>|</br>|&nbsp;|<|>|img|src=|src='/|src=\"/";

            Regex rgx = new Regex(stringsToRemove);

            return rgx.Replace(parsedString, "");
        }

        public bool DoesRssExistInDb(string urlTest, int userId)
        {
            var dbFeed = _context.Feeds.FirstOrDefaultAsync(f => f.Url == urlTest && f.User.Id == userId);
            if (dbFeed != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<FeedContent> GetSpecificRssContent(string feedUrl)
        {
            var reader = XmlReader.Create(feedUrl);
            var feedResponse = SyndicationFeed.Load(reader);

            List<FeedContent> ListOfSpecificFeedsContent = new();

            var counter = 0;

            foreach (SyndicationItem item in feedResponse.Items)
            {
                FeedContent feedObject = new();
                List<string> LinkList = new();

                try
                {
                    feedObject.Title = item.Title.Text;
                    feedObject.Summary = item.Summary.Text;
                    //feedObject.Content = item.Content.AttributeExtensions.Values.ToString();
                    feedObject.PublishDate = item.PublishDate.ToString();

                    if (item.Links is not null)
                    {
                        foreach (var link in item.Links)
                        {
                            if (!link.Uri.AbsoluteUri.Contains("image"))
                            {
                                LinkList.Add(link.Uri.AbsoluteUri);
                            }
                        }
                        feedObject.Links = LinkList;
                    }

                    ListOfSpecificFeedsContent.Add(feedObject);
                    counter++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                if (counter == 6)
                {
                    break;
                }
            }
            return ListOfSpecificFeedsContent;
        }

        public string ParsePodcastString(string podcastUrl)
        {
            string firstPart = podcastUrl.Substring(0, 25);
            string firstPartAndEmbed = firstPart + "/embed/";
            string secondPart = podcastUrl.Substring(25);
            string[] splitSecondPart = secondPart.Split("?");
            var validSecondPart = $"{splitSecondPart[0]}?utm_source=generator";
            return firstPartAndEmbed + validSecondPart;
        }
    }
}