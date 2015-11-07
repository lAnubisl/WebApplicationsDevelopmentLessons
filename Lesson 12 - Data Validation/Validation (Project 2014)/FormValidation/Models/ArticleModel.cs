using System.Collections.Generic;
using FormValidation.Repository;

namespace FormValidation.Models
{
    public class ArticleModel
    {
        public ICollection<string> Comments
        {
            get { return CommentsRepository.Comments; }
        }

        public AddCommentModel NewComment { get; set; }
    }
}