using ApiVehiculos.Models;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GestionVehiculos25Context>
    (options =>
    options.UseSqlServer("server=GestionVehiculos25.mssql.somee.com;database=GestionVehiculos25;uid=instrusenati2024_SQLLogin_1;pwd=SENATI2024;TrustServerCertificate=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Api para devolver un listado de vehículos
app.MapGet("/ListaVehiculos", () =>
{
    //Referenciar la conexión a la BD++
    using (GestionVehiculos25Context db = new GestionVehiculos25Context())
    {
        var vehiculos = db.Vehiculos.ToList();

        if (vehiculos.Count() == 0)
        {
            return Results.NoContent();
        }
        else 
        {
            return Results.Ok(vehiculos);
        }
    }
});

//Api para devolver un listado de vehículos en función del ID
app.MapGet("/ListaVehiculos/{id}", (int id) =>
{
    //Referenciar la conexión a la BD++
    using (GestionVehiculos25Context db = new GestionVehiculos25Context())
    {
        var vehiculos = db.Vehiculos.Where(filtro=>filtro.Id==id).ToList();

        if (vehiculos.Count() == 0)
        {
            return Results.NoContent();
        }
        else
        {
            return Results.Ok(vehiculos);
        }
    }
});

///Api para devolver los datos del usuario GET
///
app.MapGet("/usuarios/{email}/{password}",
    (string email, string password) =>
{ 
    using(GestionVehiculos25Context bd=new GestionVehiculos25Context())
    {
        var usuarioSistema = bd.Usuarios.Where(filtro=>filtro.Email==email
                                                    && filtro.Password== password).ToList();
        if (usuarioSistema.Count() == 0)
        {
            //cod 204 No hay contenido API REST
            return Results.NoContent();
        }
        else 
        {
            return Results.Ok(usuarioSistema);
        }
    }
});

//Api POST para envío de parametros de manera segura.
app.MapPost("/usuarios/buscarPorEmail",
    async (LoginRequerido requerido, GestionVehiculos25Context DB) =>
{
    if (requerido == null || string.IsNullOrEmpty(requerido.Email)) 
    {
        return Results.BadRequest("Falta datos de correo electrónico");
    }

    if (requerido == null || string.IsNullOrEmpty(requerido.Password))
    {
        return Results.BadRequest("Falta datos de contraseña");
    }

    var usuarioSistema=await DB.Usuarios.Where(filtro => filtro.Email == requerido.Email
                                                    && filtro.Password == requerido.Password).ToListAsync();

    if (usuarioSistema.Count() == 0)
    {
        //cod 204 No hay contenido API REST
        return Results.NoContent();
    }
    else
    {
        return Results.Ok(usuarioSistema);
    }
});

//API para registrar un nuevo usuario
app.MapPost("/usuarios/creaUsuario", async (Usuario nuevoUsuario, GestionVehiculos25Context BD) =>
{
    if (nuevoUsuario == null || string.IsNullOrEmpty(nuevoUsuario.NombreUser))
    {
        return Results.BadRequest("Falta datos del nombre del usuario");
    }

    if (nuevoUsuario == null || string.IsNullOrEmpty(nuevoUsuario.Email))
    {
        return Results.BadRequest("Falta datos del Correo electrónico");
    }
    
    if (nuevoUsuario == null || string.IsNullOrEmpty(nuevoUsuario.Password))
    {
        return Results.BadRequest("Falta datos de la contraseña");
    }

    BD.Usuarios.Add(nuevoUsuario);
    await BD.SaveChangesAsync();

    return Results.Created($"/usuarios{nuevoUsuario.IdUser}",nuevoUsuario);
});

//API para eliminar usuarios
app.MapDelete("/usuarios/{id}", async (int id, GestionVehiculos25Context BD) =>
{
    var usuarioElimnar= await BD.Usuarios.FindAsync(id);

    if (usuarioElimnar == null)
    {
        return Results.NotFound($"No se encontró el usuario con el ID {id}");
    }
    BD.Usuarios.Remove(usuarioElimnar);
    await BD.SaveChangesAsync();
    return Results.Ok(usuarioElimnar);
});

//API para actualizar los datos del usuario
app.MapPut("/usuarios/{id}", async (int id, Usuario usuaioActualizado, GestionVehiculos25Context BD) =>
{
    if (usuaioActualizado == null || string.IsNullOrEmpty(usuaioActualizado.NombreUser))
    {
        return Results.BadRequest("Falta datos del nombre del usuario");
    }

    if (usuaioActualizado == null || string.IsNullOrEmpty(usuaioActualizado.Email))
    {
        return Results.BadRequest("Falta datos del Correo electrónico");
    }

    if (usuaioActualizado == null || string.IsNullOrEmpty(usuaioActualizado.Password))
    {
        return Results.BadRequest("Falta datos de la contraseña");
    }

    var usuarioActulizar = await BD.Usuarios.FindAsync(id);
    if (usuarioActulizar == null)
    {
        return Results.NotFound($"No se encontró el usuario con el ID {id}");
    }

    usuarioActulizar.NombreUser = usuaioActualizado.NombreUser;
    usuarioActulizar.Email = usuaioActualizado.Email;
    usuarioActulizar.Password = usuaioActualizado.Password;

    await BD.SaveChangesAsync();
    return Results.Ok(usuarioActulizar);
});

app.UseHttpsRedirection();

app.Run();
