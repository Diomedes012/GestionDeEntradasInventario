using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeEntradasInventario.Models;

public class Producto
{
    [Key]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "Se requiere descripcion")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Se requiere Costo"), Range(0.1, double.MaxValue, ErrorMessage = "El costo debe ser mayor a 0")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "Se requiere Precio"), Range(0.1, double.MaxValue, ErrorMessage = "El Precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "Se requiere existencia"), Range(0, int.MaxValue, ErrorMessage = "Existencia no puede ser menor a 0")]
    public int Existencia { get; set; }

    [InverseProperty("Producto")]
    public ICollection<EntradaDetalle> EntradaDetalles { get; set; } = new List<EntradaDetalle>();
}
