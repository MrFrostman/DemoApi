using HotChocolate.AspNetCore.Authorization;
using Sprache;
using System.Security.Claims;


namespace Product.Api.GraphQL.Mutation
{

    public record CreateArticleInput(string Title, string? Content = "");

    public record CreateArticlePayload(string message);
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class CreateArticleMutation
    {

        public async Task<CreateArticlePayload> CreateArticle(CreateArticleInput input, ProductContext context)
        {

            var exists = context.Articles.Any(a => a.Title == input.Title);

            if (exists)
            {
                throw new GraphQLException("Este Articulo ya existe");
            }

            var article = new Article
            {
                Title = input.Title,
                Content = input.Content
            };

            context.Articles.Add(article);

            await context.SaveChangesAsync();

            return new CreateArticlePayload("Se Creo el Articulo");



        }
    }
}