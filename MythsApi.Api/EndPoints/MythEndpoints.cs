using MythsApi.Application.Interfaces;
using MythsApi.Application.Model;

namespace MythsApi.Api.Endpoints
{
    public static class MythEndpoints
    {
        public static IEndpointRouteBuilder MapMythEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/myths", async (IMythService service) =>
            {
                var myths = await service.GetAllAsync();
                return Results.Ok(myths);
            });

            app.MapGet("/api/myths/{id:int}", async (IMythService service, int id) =>
            {
                var myth = await service.GetByIdAsync(id);
                return myth is null ? Results.NotFound() : Results.Ok(myth);
            });

            app.MapPost("/api/myths", async (IMythService service, MythCreateModel model) =>
            {
                if (model == null)
                    return Results.BadRequest();

                var created = await service.CreateAsync(model);
                return Results.Created($"/api/myths/{created.Id}", created);
            });

            app.MapPut("/api/myths/{id:int}", async (IMythService service, int id, MythUpdateModel model) =>
            {
                if (model == null || id != model.Id)
                    return Results.BadRequest();

                await service.UpdateAsync(id, model);
                return Results.NoContent();
            });

            app.MapDelete("/api/myths/{id:int}", async (IMythService service, int id) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            return app;
        }
    }
}
