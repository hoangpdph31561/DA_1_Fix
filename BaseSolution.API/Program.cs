using BaseSolution.Application.DataTransferObjects.Account.request;
using BaseSolution.Infrastructure.Extensions;
using BaseSolution.Infrastructure.validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddScoped<IValidator<LoginInputRequest>, LoginValication>();

builder.Services.AddValidatorsFromAssemblyContaining<LoginValication>(); // khai báo để nó valiate hết tất cả mọi đối tượng có cùng project với LoginValication


builder.Services.AddLocalization(builder.Configuration);

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
