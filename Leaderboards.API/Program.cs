using Application.Commands.Users;
using Application.Factories;
using Application.Persistance;
using Infrastructure.Factories;
using Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Factories
builder.Services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddSingleton<IUsersFactory, UsersFactory>();
builder.Services.AddSingleton<IUserScoreFactory, UserScoreFactory>();
builder.Services.AddSingleton(typeof(IRepositoryResultFactory<>), typeof(RepositoryResultFactory<>));

//Repositories
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserScoreRepository, UserScoreRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PostUserCommand).Assembly));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
