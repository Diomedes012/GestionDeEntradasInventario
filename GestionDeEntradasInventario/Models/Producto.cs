using System.ComponentModel.DataAnnotations;

namespace GestionEntradasInventario.Models;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "Se requiere descripcion")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Se requiere Costo"), Range(0.1, double.MaxValue)]
    public double Costo { get; set; }

    [Required(ErrorMessage = "Se requiere Precio"), Range(0.1, double.MaxValue)]
    public double Precio { get; set; }

    [Required(ErrorMessage = "Se requiere existencia"), Range(1, int.MaxValue)]
    public int Existenci { get; set; }
}
