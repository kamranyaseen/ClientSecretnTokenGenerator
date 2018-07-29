using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ClientSecretnTokenGenerator.Context;
using ClientSecretnTokenGenerator.Models;
using ClientSecretnTokenGenerator.Filters;

namespace ClientSecretnTokenGenerator.Controllers
{
    [APIAuthorizeAttribute]
    public class LatestMusicController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/LatestMusic
        public List<MusicStore> GetMusicStore()
        {
            try
            {
                var listofSongs = db.MusicStore.ToList();
                return listofSongs;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

      
    }
}