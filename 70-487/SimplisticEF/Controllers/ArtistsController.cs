using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

using SimplisticEF.Models;
using SimplisticEF.Services;

namespace SimplisticEF.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ArtistRepository _repository;

        public ArtistsController()
        {
            _repository = new ArtistRepository();
        }

        // GET: Artists
        public ActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }
            _repository.Add(artist);
            _repository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.IsConcurrencyError = false;
            Artist artist = _repository.Get(id);
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Artist artist)
        {
            ViewBag.IsConcurrencyError = false;
            if (!ModelState.IsValid)
            {
                return View(artist);
            }

            try
            {
                _repository.Update(artist);
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.IsConcurrencyError = true;
                return View(artist);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = _repository.Get(id.Value);

            if (artist == null)
            {
                return HttpNotFound();
            }

            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Artist artist = _repository.Get(id);

            if (artist == null)
            {
                return HttpNotFound();
            }

            return View(artist);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _repository.Dispose();
            }
        }
    }
}