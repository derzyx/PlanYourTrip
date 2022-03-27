global using PlanYourTrip_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using PlanYourTrip_BackEnd;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//app.MapGet("/get-all-users", async () => await UserRepository.GetUsersAsync())
//    .WithTags("User Endpoints");

//app.MapGet("/get-user-by-id/{userId}", async (int userId) =>
//{
//    User userToReturn = await UserRepository.GetUserAsync(userId);

//    if(userToReturn != null)
//    {
//        return Results.Ok(userToReturn);
//    }
//    else
//    {
//        return Results.BadRequest();
//    }
//})
//    .WithTags("User Endpoints");

//app.MapPost("/create-user", async (User userToAdd) =>
//{
//    bool createSuccessful = await UserRepository.CreateUserAsync(userToAdd);

//    if (createSuccessful)
//    {
//        return Results.Ok("Create successful");
//    }
//    else
//    {
//        return Results.BadRequest();
//    }
//})
//    .WithTags("User Endpoints");

//app.MapPut("/update-user", async (User userToUpdate) =>
//{
//    bool updateSuccessful = await UserRepository.UpdateUserAsync(userToUpdate);

//    if (updateSuccessful)
//    {
//        return Results.Ok("Update successful");
//    }
//    else
//    {
//        return Results.BadRequest();
//    }
//})
//    .WithTags("User Endpoints");

//app.MapDelete("/delete-user-by-id/{userId}", async (int userId) =>
//{
//    bool deleteSuccessful = await UserRepository.DeleteUserAsync(userId);

//    if (deleteSuccessful)
//    {
//        return Results.Ok("Delete successful");
//    }
//    else
//    {
//        return Results.BadRequest();
//    }
//})
//    .WithTags("User Endpoints");

