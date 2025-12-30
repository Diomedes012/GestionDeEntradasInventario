using System.ComponentModel.DataAnnotations;

namespace GestionDeEntradasInventario.Models;

public class EntradaDetalle
{
    [Key]
    public int EntradaDetalleId { get; set; }

    [Required]
    public int EntradaId { get; set; }

    [Required]
    public int ProductoId { get; set; }

    [Required, Range (1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }

    [Required, Range(0.1, double.MaxValue, ErrorMessage = "El coste debe ser mayor a 0")]
    public double Costo { get; set; }   


}
