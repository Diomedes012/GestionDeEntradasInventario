using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeEntradasInventario.Models;

public class EntradaDetalle
{
    [Key]
    public int EntradaDetalleId { get; set; }

    [Required]
    public int EntradaId { get; set; }

    [Required]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "Se requiere Cantidad"), Range (1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "Se requiere Costo"), Range(0.1, double.MaxValue, ErrorMessage = "El coste debe ser mayor a 0")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Costo { get; set; }

    [ForeignKey("EntradaId")]
    public Entrada? Entrada { get; set; }

    [ForeignKey("ProductoId")]
    public Producto? Producto { get; set; }
}
