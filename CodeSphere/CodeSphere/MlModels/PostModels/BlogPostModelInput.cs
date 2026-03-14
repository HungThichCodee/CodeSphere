using Microsoft.ML.Data;

namespace CodeSphere.MlModels.PostModels
{
    public class BlogPostModelInput
    {
        [ColumnName("Content")]
        [LoadColumn(0)]
        public string Content { get; set; }

        [ColumnName("Prediction")]
        [LoadColumn(1)]
        public string Prediction { get; set; }
    }
}
