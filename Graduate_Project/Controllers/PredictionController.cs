using Microsoft.AspNetCore.Mvc;

namespace Graduate_Project.Controllers
{
    public class PredictionController : Controller
    {
        private readonly JobPredictionService _PredictionService = new JobPredictionService();

        [HttpPost]
        public async Task<ActionResult> PredictJob(List<string> Skills)
        {
            string job = await _PredictionService.GetJobPredictionAsync(Skills);
            ViewBag.PredictedJob = job;
            return View();
        }
    }
}
