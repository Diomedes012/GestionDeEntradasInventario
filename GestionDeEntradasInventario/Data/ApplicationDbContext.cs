using GestionDeEntradasInventario.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionDeEntradasInventario.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<EntradaDetalle> EntradasDetalles { get; set; }
    public DbSet<Producto> Productos { get; set; }
}
