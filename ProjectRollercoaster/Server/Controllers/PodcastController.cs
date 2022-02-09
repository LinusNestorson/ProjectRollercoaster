namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Server.Helpers;
    using ProjectRollercoaster.Shared;
    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PodcastController : ControllerBase
    {
        private readonly DataContext _context;

        public PodcastController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPodcast(Podcast podcastInfo)
        {
            RssHelper rssHelper = new(_context);
            var viableUrl = rssHelper.ParsePodcastString(podcastInfo.Url);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await Task.Delay(1000);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var dbPodcast = await _context.Podcasts.FirstOrDefaultAsync(f => f.Url == viableUrl && f.User.Id == userId);

            if (dbPodcast == null)
            {
                var podcast = new Podcast() { Url = viableUrl, Image = podcastInfo.Image, Name = podcastInfo.Name, User = user };
                _context.Podcasts.Add(podcast);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePodcast(int Id)
        {
            var dbPodcast = await _context.Podcasts.FirstOrDefaultAsync(f => f.Id == Id);
            if (dbPodcast == null)
            {
                return NotFound("Podcast with given link does not exist");
            }
            _context.Podcasts.Remove(dbPodcast);
            await _context.SaveChangesAsync();
            return Ok(await _context.Podcasts.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPodcast()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dbPodcasts = await _context.Podcasts.Where(f => f.User.Id == userId).ToListAsync();
            return Ok(dbPodcasts);
        }
    }
}