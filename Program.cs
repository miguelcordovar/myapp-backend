var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(logging => logging.AddConsole());
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IUserService, UserService>();
var app = builder.Build();


app.MapGet("/users", async (IUserService userService) =>
    await userService.GetUsersAsync());

app.Run();
