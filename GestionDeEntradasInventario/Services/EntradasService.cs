using GestionDeEntradasInventario.Data;
using GestionDeEntradasInventario.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionDeEntradasInventario.Services;

public class EntradasService(IDbContextFactory<ApplicationDbContext> factory) 
{
    public async Task<bool> Guardar(Entrada entrada)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        if(!await Existe(entrada.EntradaId))
        {
            return await Insertar(entrada);
        }
        else
        {
            return await Modificar(entrada);
        }
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        return await contexto.Entradas.AnyAsync(e => e.EntradaId == id);
    }

    private async Task<bool> Insertar(Entrada entrada)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        contexto.Entradas.Add(entrada);

        foreach (var detalle in entrada.EntradaDetalles)
        {
            var producto = await contexto.Productos.FindAsync(detalle.ProductoId);
            if (producto != null)
            {
                producto.Existencia += detalle.Cantidad;
            }
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Entrada entrada)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        var entradaVieja = await contexto.Entradas
            .Include(e => e.EntradaId)
            .FirstOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if(entradaVieja == null)
        {
            return false;
        }

        foreach(var detalleViejo in entradaVieja.EntradaDetalles)
        {
            var producto = await contexto.Productos.FindAsync(detalleViejo.ProductoId);
            if (producto != null)
            {
                producto.Existencia -= detalleViejo.Cantidad;
            }
        }

        contexto.EntradasDetalles
            .RemoveRange(entradaVieja.EntradaDetalles);

        entradaVieja.Fecha = entrada.Fecha;
        entradaVieja.Concepto = entrada.Concepto;
        entradaVieja.Total = entrada.Total;

        foreach(var detalleNuevo in entrada.EntradaDetalles)
        {
            entradaVieja.EntradaDetalles.Add(detalleNuevo);

            var producto = await contexto.Productos.FindAsync(detalleNuevo.ProductoId);
            if(producto != null)
            {
                producto.Existencia += detalleNuevo.Cantidad;
            }
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Entrada?> Buscar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        return await contexto.Entradas
            .Include(e => e.EntradaDetalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        var entrada = await contexto.Entradas.FindAsync(id);

        if(entrada == null)
        {
            return false;
        }

        contexto.Entradas.Remove(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Entrada>> Listar(Expression<Func<Entrada, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();

        return await contexto.Entradas
            .Include(e => e.EntradaDetalles)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
