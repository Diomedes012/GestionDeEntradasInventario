using GestionDeEntradasInventario.Data;
using GestionDeEntradasInventario.Models;
using GestionDeEntradasInventario.Data;
using GestionDeEntradasInventario.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionDeEntradasInventario.Services;

public class ProductosService(IDbContextFactory<ApplicationDbContext> factory)
{
    public async Task<bool> Guardar(Producto producto)
    {
        if (!await Existe(producto.ProductoId))
        {
            return await Insertar(producto);
        }
        else
        {
            return await Modificar(producto);
        }
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Productos.AnyAsync(p => p.ProductoId == id);
    }
    public async Task<bool> ExisteNombre(string descripcion, int id = 0)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Productos
            .AnyAsync(p => p.Descripcion.ToLower() == descripcion.ToLower() && p.ProductoId != id);
    }

    private async Task<bool> Insertar(Producto producto)
    {
        if (await ExisteNombre(producto.Descripcion))
            return false;

        await using var contexto = await factory.CreateDbContextAsync();
        contexto.Productos.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Producto producto)
    {
        if (await ExisteNombre(producto.Descripcion, producto.ProductoId))
            return false;

        await using var contexto = await factory.CreateDbContextAsync();
        contexto.Productos.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Producto?> Buscar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Productos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductoId == id);
    }

    public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Productos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        var enUso = await contexto.EntradasDetalles.AnyAsync(d => d.ProductoId == id);

        if (enUso)
        {
            return false;
        }

        var producto = await contexto.Productos.FindAsync(id);
        if (producto == null) return false;

        contexto.Productos.Remove(producto);
        return await contexto.SaveChangesAsync() > 0;
    }
}