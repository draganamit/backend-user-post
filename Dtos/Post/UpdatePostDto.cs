namespace backend_user_post.Dtos.Post
{
    public class UpdatePostDto
    {
         public int Id { get; set; }

        public string Text { get; set; }="The book is good.";
    }
}