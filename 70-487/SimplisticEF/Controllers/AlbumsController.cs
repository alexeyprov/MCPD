using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

using SimplisticEF.Models;
using SimplisticEF.Services;

namespace SimplisticEF.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly AlbumRepository _repository;

        public AlbumsController()
        {
            _repository = new AlbumRepository();
        }

        // GET: Albums
        public async Task<ActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        // GET: Albums/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await _repository.GetAsync(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AlbumId,Name,Price")] Album album)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(album);
                await _repository.SaveAsync();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await _repository.GetAsync(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }

            ViewBag.IsConcurrencyError = false;
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AlbumId,Name,Price,Version")] Album album)
        {
            ViewBag.IsConcurrencyError = false;
            if (!ModelState.IsValid)
            {
                return View(album);
            }

            TransactionScope scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await _repository.UpdateAsync(album);
                await _repository.SaveAsync();
                scope.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.IsConcurrencyError = true;
                return View(album);
            }
            finally
            {
                scope.Dispose();
            }

            return RedirectToAction("Index");
        }

        // GET: Albums/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await _repository.GetAsync(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
