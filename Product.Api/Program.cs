
using Product.Core.Interfaces;
using Product.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using Product.Api.GraphQL.Mutation;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>(ServiceLifetime.Transient);
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

//builder.Services.AddSingleton<ICountryService, CountryService>();
//builder.Services.AddTransient<IMemberService, MemberService>();
//builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Issuer"),
        ValidateIssuer = true,
        ValidAudience = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Audience"),
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenSettings").GetValue<string>("Key"))),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
builder.Services
    .AddGraphQLServer()
    .AddConvention<IFilterConvention>(new FilterConvention(x =>
        x.AddDefaults()))
    .AddConvention<IFilterConvention>(new FilterConventionExtension(descriptor =>
    {
        descriptor.ArgumentName("filter");
    }))
    .AddQueryType<Global>()
    //.AddMutationType()
    //    .AddTypeExtension<CreateMemberMutation>()>
    .AddMutationType()
        .AddTypeExtension<CreateArticleMutation>()
        .AddTypeExtension<UpdateArticleMutation>()
        .AddTypeExtension<DeleteArticleMutation>()
    .ConfigureResolverCompiler(c => c.AddService<ProductContext>())
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddAuthorization();

builder.Services.AddCors(option => {
    option.AddPolicy("allowedOrigin",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        );
});

var app = builder.Build();

app.UseCors("allowedOrigin");
app.UseAuthentication();

app.MapGraphQL();

app.MapGet("/", () => "POWERED IN IXULABS");

//KUBERNETES
//liveness, readiness and startup probes for containers
app.MapGet("/liveness", () => "Liveness Product");
app.MapGet("/readiness", () => "Readiness Product");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ProductContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}


await app.RunAsync();
