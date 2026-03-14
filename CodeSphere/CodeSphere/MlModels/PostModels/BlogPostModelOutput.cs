using Microsoft.ML.Data;

namespace CodeSphere.MlModels.PostModels
{
    public class BlogPostModelOutput
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }

        public float[] Score { get; set; }
    }
}
