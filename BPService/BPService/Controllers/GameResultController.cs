using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using BPService.DataObjects;
using BPService.Models;

namespace BPService.Controllers
{
    public class GameResultController : TableController<GameResult>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<GameResult>(context, Request, Services);
        }

        // GET tables/GameResult
        [AuthorizeLevel(Microsoft.WindowsAzure.Mobile.Service.Security.AuthorizationLevel.Anonymous)]
        public IQueryable<GameResult> GetAllGameResult()
        {
            return Query(); 
        }

        // GET tables/GameResult/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<GameResult> GetGameResult(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/GameResult/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<GameResult> PatchGameResult(string id, Delta<GameResult> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/GameResult
        public async Task<IHttpActionResult> PostGameResult(GameResult item)
        {
            GameResult current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/GameResult/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteGameResult(string id)
        {
             return DeleteAsync(id);
        }

    }
}