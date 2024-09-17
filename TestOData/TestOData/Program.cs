using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataSample.Data.Context;
using ODataSample.Data.Models;
using ODataSample.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var odataBatchHandler = new DefaultODataBatchHandler();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<BookStoreContext>(option => option.UseInMemoryDatabase("BookLists"));
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();
builder.Services.AddScoped<IGenericRepository<Press>, GenericRepository<Press>>();
builder.Services.AddControllers().AddOData(option => option.Select().Filter()
                                    .Count().OrderBy().Expand().SetMaxTop(100)
                                    .AddRouteComponents("odata", GetEdmModel(), odataBatchHandler));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Book>("Books");
    builder.EntitySet<Press>("Presses");

    builder.EntityType<Book>().HasOptional(b => b.Press);
    return builder.GetEdmModel();
}

app.UseHttpsRedirection();
app.UseODataBatching();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
