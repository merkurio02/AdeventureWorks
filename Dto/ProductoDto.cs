namespace AdventureWorks.Dto
{
    public class ProductoDto
    {

        
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public string Numero { get; set; } 
        public string Color { get; set; }
        public decimal CostoEstandar { get; set; }
        public decimal Precio { get; set; }
        public string Tamaño { get; set; }
        public decimal Peso { get; set; }
        public string Clase { get; set; }
        public string Estilo { get; set; }
        public string Categoria { get; set; }
        public string Modelo { get; set; }
        public DateTime VentaInicio { get; set; }
        public DateTime? VentaFin { get; set; }
        public DateTime? FechaDescontinuado { get; set; }
        public DateTime? UltimoCambio { get; set; }
        public DateTime? UltimoCambioOferta { get; set; }

    }
}
