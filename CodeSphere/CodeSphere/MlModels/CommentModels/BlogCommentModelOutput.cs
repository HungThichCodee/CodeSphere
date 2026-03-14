using Microsoft.ML.Data;

namespace CodeSphere.MlModels.CommentModels
{
    public class BlogCommentModelOutput
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }

        public float[] Score { get; set; }
    }
}
