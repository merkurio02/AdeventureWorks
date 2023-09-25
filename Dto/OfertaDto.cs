using AdventureWorks.Dto;
using AdventureWorks.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace AdventureWorks.Dto
{
    public class OfertaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int MinQty { get; set; }
        public int? MaxQty { get; set; }
        public string UltimaModificacion { get; set; }

        public List<ProductoDto> Productos { get; set; } = new List<ProductoDto>();

        public List<ProductoDto> AllProductos { get; set; }
    }
}
