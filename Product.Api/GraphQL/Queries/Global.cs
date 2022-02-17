namespace Product.Api.GraphQL.Queries
{
    public class Global
    {
     
        public List<Article> GetListArticles(ProductContext context)
        {
            var articles = context.Articles.ToList();   
            return articles;
        }

    }
}
