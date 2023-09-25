using AdventureWorks.Dto;
using AdventureWorks.Models;

namespace AdventureWorks.Dto
{
    public class OfertaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int MinQty { get; set; }
        public int? MaxQty { get; set; }
        public string UltimaModificacion { get; set; }

        public List<ProductoDto> Productos { get; set; } = new List<ProductoDto>();
    }
}
