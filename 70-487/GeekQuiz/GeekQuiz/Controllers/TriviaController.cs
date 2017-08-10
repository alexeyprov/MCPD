using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GeekQuiz.Models;

namespace GeekQuiz.Controllers
{
    [Authorize]
    public class TriviaController : ApiController
    {
        private readonly TriviaDbContext _db;

        public TriviaController()
        {
            _db = new TriviaDbContext();
        }

        // GET /api/Trivia
        [ResponseType(typeof(TriviaQuestion))]
        public async Task<IHttpActionResult> Get()
        {
            string userId = User.Identity.Name;
            int lastQuestionId = await _db.TriviaAnswers
                .Where(a => a.UserId == userId)
                .Select(a => a.QuestionId)
                .OrderByDescending(i => i)
                .FirstOrDefaultAsync();

            TriviaQuestion nextQuestion = await _db.TriviaQuestions
                .Where(q => q.Id > lastQuestionId)
                .OrderBy(q => q.Id)
                .Include(q => q.Options)
                .FirstOrDefaultAsync();

            return nextQuestion == null ?
                (IHttpActionResult)NotFound() :
                Ok(nextQuestion);
        }

        // POST /api/Trivia
        public async Task<IHttpActionResult> Post(TriviaAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            answer.UserId = User.Identity.Name;
            _db.TriviaAnswers.Add(answer);
            await _db.SaveChangesAsync();

            TriviaOption choice = await _db.TriviaOptions
                .SingleAsync(o => o.Id == answer.OptionId);

            return Ok(choice.IsCorrect);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}
