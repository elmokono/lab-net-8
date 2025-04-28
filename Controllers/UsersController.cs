namespace MyAwsApp.Controllers
{
    public class UsersController
    {
        public void MapUsersEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/users/{id}", async (string id, IUsersService usersService) =>
            {

                var user = await usersService.GetUserByIdAsync(id);

                if (user == null) return Results.NotFound();

                return Results.Ok(user);

            });

            app.MapPost("/api/users", async (UserDto user, IUsersService usersService) =>
            {

                await usersService.AddUserAsync(user);

                return Results.Created("/api/users/{user.UserId}", user);

            });

            app.MapPost("/api/users/hydrate/{count}", async (int count, IUsersService usersService) =>
            {

                await usersService.HydrateDB(count);

                return Results.Ok();
            });
        }
    }
}

