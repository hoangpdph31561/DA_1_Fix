﻿using BaseSolution.BlazorServer.Data;
using BaseSolution.BlazorServer.Respository.Implements;
using BaseSolution.BlazorServer.Respository.Interfaces;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

builder.Services.AddScoped(c => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7005")
});
#region AddTransient
builder.Services.AddTransient<IBuildingRespo, BuildingRespo>();
builder.Services.AddTransient<IFloorRespo, FloorRespo>();
builder.Services.AddTransient<IAmenityRespo, AmenityRespo>();
builder.Services.AddTransient<IRoomDetailRespo, RoomDetailRespo>();
builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
builder.Services.AddTransient<ISendMailService, SendMailService>();
builder.Services.AddTransient<IServiceTypeRespo, ServiceTypeRespo>();
builder.Services.AddTransient<IRoomBookingRepo, RoomBookingRepo>();
builder.Services.AddTransient<IBillRepo, BillRepo>();
builder.Services.AddTransient<IRoomTypeRepo, RoomTypeRepo>();
builder.Services.AddTransient<IAmenityRoomDetailRepo, AmenityRoomDetailRepo>();
builder.Services.AddTransient<IServiceRespo, ServiceRespo>();
builder.Services.AddTransient<IRoomBookingRespo, RoomBookingRespo>();
builder.Services.AddTransient<IBillRespo, BillRespo>();
builder.Services.AddTransient<IServiceOrderRepo, ServiceOrderRepo>();
builder.Services.AddTransient<IRoomTypeRespo, RoomTypeRespo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();