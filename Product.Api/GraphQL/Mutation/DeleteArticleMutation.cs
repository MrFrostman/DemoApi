using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;


namespace Product.Api.GraphQL.Mutation
{

    public record DeleteArticleInput(Guid ArticleId);
    
    public record DeleteArticlePayload(string message); 

    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class DeleteArticleMutation
    {
        public async Task<DeleteArticlePayload> DeleteArticle(DeleteArticleInput input,ProductContext context)

        {
            var article=context.Articles.FirstOrDefault(a=>a.Id==input.ArticleId);
            if(article==null)
            {
                throw new GraphQLException("Articulo no existe");
            }

            context.Articles.Remove(article); 
            await context.SaveChangesAsync();

            return new DeleteArticlePayload("Se elimino el articulo");


        }
    }
}
