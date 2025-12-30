using System.ComponentModel.DataAnnotations;

namespace GestionEntradasInventario.Models;

public class Entrada
{
    [Key]
    public int EntradaId { get; set; }

    [Required(ErrorMessage = "Se requiere Fecha")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Se requiere Concepto")]
    public string Concepto { get; set; }

    [Required(ErrorMessage = "se requiere Total"), Range(0.1, double.MaxValue)]
    public double Total { get; set; }
}
