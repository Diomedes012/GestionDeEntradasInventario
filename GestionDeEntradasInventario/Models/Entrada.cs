using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeEntradasInventario.Models;

public class Entrada
{
    [Key]
    public int EntradaId { get; set; }

    [Required(ErrorMessage = "Se requiere Fecha")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Se requiere Concepto")]
    [StringLength(100, ErrorMessage = "El concepto no puede tener mas de 100 caracteres")]
    public string Concepto { get; set; }

    [Required(ErrorMessage = "se requiere Total"), Range(0.1, double.MaxValue, ErrorMessage = "Total debe ser mayor a 0")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Total { get; set; }

    [InverseProperty("Entrada")]
    public ICollection<EntradaDetalle> EntradaDetalles { get; set; } = new List<EntradaDetalle>();
}
