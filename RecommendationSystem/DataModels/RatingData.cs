using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.DataModels
{
    public class RatingData
    {
        [LoadColumn(0)]
        public float userId;
        [LoadColumn(1)]
        public float assetId;
        [LoadColumn(2), ColumnName("Label")]
        public float Rating;
    }
}
