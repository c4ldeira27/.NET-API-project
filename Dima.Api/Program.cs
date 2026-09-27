using Dima.Api.Common.Api;
using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();


var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(Configuration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();


app.Run();

