using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PolicyAPI.Configurations;
using PolicyAPI.Contexts;
using PolicyAPI.Repositories;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Steeltoe.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using System.Text.Json;
using PolicyAPI.Schemas;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VaultSharp.V1.Commons;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//step to add config server
builder.Configuration.AddConfigServer();
ConfigurationManager configuration = builder.Configuration;
//var Url = configuration["awsvaulturl"].ToString();
//var RootKey = configuration["rootkey"].ToString();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


//IDictionary<string, Object> result = new VaultConfiguration(configuration)
  //  .GetSecrets(RootKey, Url).Result;

//IDictionary<string, Object> jwtResult = new VaultConfiguration(configuration)
  // .GetJWTSecrets(RootKey, Url).Result;

//var data = _configuration["Secret"];
//var SecretData = jwtResult["secret"].ToString();
SqlConnectionStringBuilder providerCs
    = new SqlConnectionStringBuilder();
//providerCs.UserID = result["username"].ToString();
//providerCs.Password = result["password"].ToString();
providerCs.UserID = "sa";
providerCs.Password = "Vignesh@95";
//providerCs.DataSource = configuration["trainerservername"];
providerCs.DataSource = "host.docker.internal,1403";
providerCs.InitialCatalog = "PolicyDbBatch2";
providerCs.MultipleActiveResultSets = true;
providerCs.TrustServerCertificate = true;

builder.Services.AddDbContext<PolicyContext>(o =>
o.UseSqlServer(providerCs.ToString()));
builder.Services.AddDbContext<PolicyIdentityContext>(o =>
o.UseSqlServer(configuration.GetConnectionString("PolicyIdentityConn")));

builder.Services.AddTransient<IPolicyHolderRepo, PolicyHolderRepo>();

builder.Services.AddTransient<IPolicyRepo, PolicyRepo>();

builder.Services.AddTransient<IAddressRepo, AddressRepo>();

builder.Services.AddTransient<IVehicleRepo, VehicleRepo>();
builder.Services.AddTransient<IPolicyPublishRepo, PolicyPublishRepo>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PolicyIdentityContext>()
    .AddDefaultTokenProviders();



builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "PolicyAPI", Version = "v1" });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
var policyName = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                             .WithOrigins("http://localhost:*", "")
                             //.WithOrigins("http://localhost:3000")
                             // specifying the allowed origin
                             // .WithMethods("GET") // defining the allowed HTTP method
                             .AllowAnyOrigin()
                             // .WithHeaders(HeaderNames.ContentType, "ApiKey")
                             .AllowAnyMethod()
                            .AllowAnyHeader(); // allowing any header to be sent
                      });
});

builder.Services.AddScoped<PolicySchema>();

builder.Services.AddGraphQL()
               .AddSystemTextJson()
               .AddGraphTypes(typeof(PolicySchema), ServiceLifetime.Scoped);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Add services to the container.

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
       // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretData))
    };
});

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}
app.UseGraphQL<PolicySchema>();
app.UseGraphQLPlayground(options: new PlaygroundOptions());
app.UseHttpsRedirection();
app.UseCors(policyName);
app.UseAuthentication();
app.UseAuthorization();
app.UseMetricServer();
app.UseHttpMetrics();
app.MapControllers();

app.Run();
