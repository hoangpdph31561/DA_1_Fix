using BaseSolution.Application.DataTransferObjects.Account.request;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Infrastructure.Extensions;
using FluentValidation;
using static BaseSolution.Application.DataTransferObjects.Account.request.LoginInputRequest;
using static BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request.AmenityCreateUpdateDeleteRequest;
using static BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request.AmenityRoomDetailCreateRequest;
using static BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request.AmenityRoomDetailUpdateRequest;
using static BaseSolution.Application.DataTransferObjects.RoomType.Request.RoomTypeCreateRequest;
using static BaseSolution.Application.DataTransferObjects.RoomType.Request.RoomTypeDeleteRequest;
using static BaseSolution.Application.DataTransferObjects.RoomType.Request.RoomTypeUpdateRequest;
using static BaseSolution.Application.DataTransferObjects.ServiceOrder.Request.ServiceOrderCreateRequest;
using static BaseSolution.Application.DataTransferObjects.ServiceOrder.Request.ServiceOrderDeleteRequest;
using static BaseSolution.Application.DataTransferObjects.ServiceOrder.Request.ServiceOrderUpdateRequest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddScoped<IValidator<LoginInputRequest>, LoginValication>();
builder.Services.AddScoped<IValidator<RoomTypeUpdateRequest>, RoomTypeValication>();
builder.Services.AddScoped<IValidator<RoomTypeCreateRequest>, RoomTypeCreateValication>();
builder.Services.AddScoped<IValidator<RoomTypeDeleteRequest>, RoomTypeDeleteValication>();
builder.Services.AddScoped<IValidator<ServiceOrderCreateRequest>, ServiceOrderValication>();
builder.Services.AddScoped<IValidator<ServiceOrderUpdateRequest>, ServiceOrderUpdateValication>();
builder.Services.AddScoped<IValidator<ServiceOrderDeleteRequest>, ServiceOrderDeleteValication>();

builder.Services.AddScoped<IValidator<AmenityCreateUpdateDeleteRequest>, AmenityRoomValication>();
builder.Services.AddScoped<IValidator<AmenityRoomDetailCreateRequest>, AmenityRoomCreateValication>();
builder.Services.AddScoped<IValidator<AmenityRoomDetailUpdateRequest>, AmenityRoomUpdateValication>();


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
