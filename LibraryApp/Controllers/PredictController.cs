using LibraryApp.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Newtonsoft.Json;
using RecommendationSystem.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private readonly PredictionEnginePool<RatingData, RatingPrediction> _predictionEnginePool;
        private readonly IBookService _bookService;
        private readonly IAssetService _assetService;

        public PredictController(PredictionEnginePool<RatingData, RatingPrediction> predictionEnginePool, IBookService bookService, IAssetService assetService)
        {
            _predictionEnginePool = predictionEnginePool;
            _bookService = bookService;
            _assetService = assetService;
        }

        class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
        {
            public int Compare(T x, T y)
            {
                return y.CompareTo(x);
            }
        }

        public class PredictInput
        {
            public string Input { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PredictInput input)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            var userId = JsonConvert.DeserializeObject<string>(input.Input);
            var ratingPredictions = new SortedDictionary<float, int>(new DescendingComparer<float>());

            var assets = await _assetService.GetAll();

            if (assets == null)
            {
                return StatusCode(500);
            }

            foreach (var asset in assets)
            {
                var data = new RatingData { userId = float.Parse(userId), assetId = asset.Id };
                RatingPrediction prediction = _predictionEnginePool.Predict(modelName: "BookRecommenderModel", example: data);
                var book = await _bookService.GetGenericBook(asset.Id);
                ratingPredictions.Add(prediction.Score, book.AssetId);
            }

            return Ok(ratingPredictions.Where(s => s.Key > 3.5).Take(3));
        }
    }
}
