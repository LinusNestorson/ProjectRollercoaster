namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Server.Helpers;
    using ProjectRollercoaster.Shared;

    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize]
    //public class UserFeedController : ControllerBase
    //{
    //    private readonly DataContext _context;
    //    private readonly IUtilityHelper _utilityHelper;

    //    public UserFeedController(DataContext context, IUtilityHelper utilityHelper)
    //    {
    //        _context = context;
    //        _utilityHelper = utilityHelper;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> AddUserFeed(Feed feed)
    //    {
    //        RssHelper xmlHelpers = new();
    //        var check = xmlHelpers.IsRssValid(feed.Link);

    //        var dbFeed = await _context.Feeds.FirstOrDefaultAsync<Feed>(f => f.Link == feed.Link);

    //        if (check && dbFeed == null)
    //        {
    //            var feedObject = xmlHelpers.GetRssInfo(feed.Link);
    //            _context.Feeds.Add(feedObject);
    //            await _context.SaveChangesAsync();
    //            return Ok(await _context.Feeds.ToListAsync());
    //        }
    //        else
    //        {
    //            return BadRequest();
    //        }
    //    }
    //}
}