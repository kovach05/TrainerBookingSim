using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        
        [HttpGet("subscription-count-by-trainer")]
        public async Task<IActionResult> GetSubscriptionCountByTrainer()
        {
            var result = await _statisticsService.GetSubscriptionCountByTrainerAsync();
            return Ok(result);
        }
        
        [HttpGet("new-clients-by-month")]
        public async Task<IActionResult> GetNewClientsByMonth()
        {
            var result = await _statisticsService.GetNewClientsByMonthAsync();
            return Ok(result);
        }

        [HttpGet("popular-trainers-with-occupied-places/{threshold}")]
        public async Task<IActionResult> GetPopularTrainersWithOccupiedPlaces(int threshold)
        {
            var result = await _statisticsService.GetPopularTrainersWithOccupiedPlacesAsync(threshold);
            return Ok(result);
        }

        [HttpGet("visits-count-by-client")]
        public async Task<IActionResult> GetVisitsCountByClient()
        {
            var result = await _statisticsService.GetVisitsCountByClientAsync();
            return Ok(result);
        }

        [HttpGet("clients-with-subscriptions-above-visit-count/{minVisitCount}")]
        public async Task<IActionResult> GetClientsWithSubscriptionsAboveVisitCount(int minVisitCount)
        {
            var result = await _statisticsService.GetClientsWithSubscriptionsAboveVisitCountAsync(minVisitCount);
            return Ok(result);
        }

        [HttpGet("total-income-by-trainer")]
        public async Task<IActionResult> GetTotalIncomeByTrainer()
        {
            var result = await _statisticsService.GetTotalIncomeByTrainerAsync();
            return Ok(result);
        }
    }
}
