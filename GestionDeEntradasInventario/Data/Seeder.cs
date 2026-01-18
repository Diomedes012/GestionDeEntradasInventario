using Microsoft.AspNetCore.Identity;

namespace GestionDeEntradasInventario.Data;

public static class DbSeeder
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string email = "admin@admin.com";
        string password = "Password123!";
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true 
            };

            var resultado = await userManager.CreateAsync(user, password);

            if (resultado.Succeeded)
            {
                Console.WriteLine("✅ Usuario Admin creado exitosamente.");
            }
            else
            {
                var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
                Console.WriteLine($"❌ Error al crear Admin: {errores}");
            }
        }
    }
}