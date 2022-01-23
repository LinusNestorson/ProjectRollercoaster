﻿using Microsoft.EntityFrameworkCore;
using ProjectRollercoaster.Server.Data;
using ProjectRollercoaster.Shared;
using System.ServiceModel.Syndication;
using System.Xml;

namespace ProjectRollercoaster.Server.Helpers
{
    public class RssHelper

    {
        private readonly DataContext _context;

        public bool IsRssValid(string urlTest)
        {
            if (DoesRssExist(urlTest))
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

        public Feed AddRssInfo(string url, string feedName, User user)
        {
            Feed feedObject = new();

            feedObject.Url = url;

            feedObject.Name = feedName;

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

                        ListOfFeedsWithContent.Add(feedObject);
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
                ListWithListOfFeeds.Add(ListOfFeedsWithContent);
            }
            return ListWithListOfFeeds;
        }

        public async Task<bool> DoesRssExistInDb(string urlTest)
        {
            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Url == urlTest);
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
    }
}