
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Path_Explorer.BusinessLogic.Abstractions;
using Path_Explorer.BusinessLogic.Concrete;
using Path_Explorer.DAL;
using Path_Explorer.DAL.Context;
using Path_Explorer.Infrastructure.Abstractions;
using Path_Explorer.Infrastructure.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IDateTimeService), typeof(DateTimeService));
builder.Services.AddDALApplicationDependencies(builder.Configuration);
builder.Services.AddScoped<IQuizManagementService, QuizManagementService>();

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Customize naming policy if needed
    options.JsonSerializerOptions.DictionaryKeyPolicy = null; // Customize key policy if needed
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PATH EXPLORER QUIZ-MANAGEMENT API",
        Description = "<h3>API for Quiz Management Services </h3><p>",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "MANAL KABIR",
            Email = "youremail@email.com"
        },
        License = new OpenApiLicense
        {
            Name = "Proprietary software"
        }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
DbCoreInitializer.Initialize(app);

app.UseAuthorization();

app.MapControllers();

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.Run();
