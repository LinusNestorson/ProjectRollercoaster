using Microsoft.EntityFrameworkCore;
using ProjectRollercoaster.Server.Data;
using ProjectRollercoaster.Shared;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProjectRollercoaster.Server.Helpers
{
    /// <summary>
    /// Collection of methods handling the requests from client via controllers.
    /// </summary>
    public class RssHelper

    {
        private readonly DataContext _context;

        public RssHelper(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Checks if the rss-link is valid.
        /// </summary>
        /// <param name="urlTest">url provided by user.</param>
        /// <param name="userId">Id of user.</param>
        /// <returns>True if rss is valid and false if not.</returns>
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

        /// <summary>
        /// Sends the url from user to se if the response is a valid rss feed.
        /// </summary>
        /// <param name="urlTest">url provided by user.</param>
        /// <returns>True if rss is valid and false if not.</returns>
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

        /// <summary>
        /// Adds info from the user to an feed object.
        /// </summary>
        /// <param name="url">Url provided by user.</param>
        /// <param name="feedName">Name of feed provided by user.</param>
        /// <param name="image">Chosen image provided by user.</param>
        /// <param name="user">User object.</param>
        /// <returns>Feed object containing feed information.</returns>
        public Feed AddRssInfo(string url, string feedName, string image, User user)
        {
            Feed feedObject = new();

            feedObject.Url = url;

            feedObject.Name = feedName;

            feedObject.Image = image;

            feedObject.User = user;

            return feedObject;
        }

        /// <summary>
        /// Sends rss urls and uses the response from XMLReader-class to fill up objects with up to date info from the sources.
        /// </summary>
        /// <param name="listOfFeeds">List of the feeds that the user subscribes to.</param>
        /// <returns>List of a list with feeds with content.</returns>
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

        /// <summary>
        /// Extracts image from descriptions if img is placed wrong by rss-source.
        /// </summary>
        /// <param name="text">Text of the rss content that needs img-text to be extracted.</param>
        /// <returns>Link to the img source.</returns>
        private string ExtractImg(string text)
        {
            int firstIndex = text.IndexOf("https:");
            int lastIndex = text.IndexOf(".jpg");
            return text.Substring(firstIndex, lastIndex - 6);
        }

        /// <summary>
        /// Removes img text from content and replaces it with empty string.
        /// </summary>
        /// <param name="summaryText">Text from rss-link without img text extracted.</param>
        /// <param name="feedObject">Object with feed content.</param>
        /// <returns></returns>
        private string RemoveImgString(string summaryText, FeedContent feedObject)
        {
            return summaryText.Replace(feedObject.Picture, "");
        }

        /// <summary>
        /// Removes unwanted tags from text.
        /// </summary>
        /// <param name="text">Text content of rss feed.</param>
        /// <returns>Text without tags.</returns>
        private string RemoveTags(string text)
        {
            var parsedString = text;
            string stringsToRemove = "<p>|</p>|<br>|</br>|&nbsp;|<|>|img|src=|src='/|src=\"/";

            Regex rgx = new Regex(stringsToRemove);

            return rgx.Replace(parsedString, "");
        }

        /// <summary>
        /// Looks through database to see if url already exists.
        /// </summary>
        /// <param name="urlTest">Url string.</param>
        /// <param name="userId">Id of user.</param>
        /// <returns>True if url exists and false if not.</returns>
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

        /// <summary>
        /// Gets info from a specific feed if user choose to visit the page of a single feed.
        /// </summary>
        /// <param name="feedUrl">Url of specific feed.</param>
        /// <returns>List with news from specific feed.</returns>
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

        /// <summary>
        /// Removes parts from spotify-link which makes it possible to replace code in embeded player with the podcast of users choice.
        /// </summary>
        /// <param name="podcastUrl">Url to specific podcast</param>
        /// <returns>String that is viable to inject into the podcast player.</returns>
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