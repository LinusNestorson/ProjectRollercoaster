namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Server.Helpers;
    using ProjectRollercoaster.Shared;
    using System.Security.Claims;

    /// <summary>
    /// Controller class handling requests regarding podcasts.
    /// </summary>
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

        /// <summary>
        /// Handling request to add new podcast to database.
        /// </summary>
        /// <param name="podcastInfo">Object containing podcast info.</param>
        /// <returns>Status code.</returns>
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

        /// <summary>
        /// Handling request to delete podcast from database.
        /// </summary>
        /// <param name="Id">Id of specific podcast.</param>
        /// <returns>Not found string if not found or updating list of podcast if success.</returns>
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

        /// <summary>
        /// Handling request to get updated list of podcasts.
        /// </summary>
        /// <returns>List of pdocast.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPodcast()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dbPodcasts = await _context.Podcasts.Where(f => f.User.Id == userId).ToListAsync();
            return Ok(dbPodcasts);
        }
    }
}