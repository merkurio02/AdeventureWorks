using Microsoft.EntityFrameworkCore;
using AdventureWorks.Models;
using AdventureWorks.Models.Context;
using AdventureWorks.Dto;

namespace AdventureWorks.Repository
{
    public class OfertaRepository:IOfertaRepository
    {
        private readonly AdventureWorks2019Context _context=null;

        public OfertaRepository(AdventureWorks2019Context context)
        {
            _context = context;

        }

        public async Task<List<OfertaDto>>  GetOfertas()
        {

            List<OfertaDto> list = new List<OfertaDto>();
            var query = await _context.SpecialOffers
                .Include(of => of.SpecialOfferProducts).ThenInclude(sop => sop.Product)
                //.ThenInclude(p=>p.ProductModel)
                //.Include(of => of.SpecialOfferProducts).ThenInclude(sop => sop.Product).ThenInclude(p => p.ProductSubcategory)
                .ToListAsync();
            foreach (var item in query)
            {
                OfertaDto oferta = new OfertaDto();
                oferta.Id = item.SpecialOfferId;
                oferta.Descripcion = item.Description;
                oferta.Tipo = item.Type;
                oferta.Categoria = item.Category;
                oferta.FechaInicio = item.StartDate.ToShortDateString();
                oferta.FechaFin=item.EndDate.ToShortDateString(); 
                oferta.MinQty = item.MinQty;
                oferta.MaxQty=item.MaxQty;
                oferta.UltimaModificacion = item.ModifiedDate.ToShortDateString();
                oferta.Porcentaje = item.DiscountPct;

                oferta.Productos = new();
                foreach(var item2 in item.SpecialOfferProducts)
                {
                    ProductoDto producto = new ProductoDto();
                    producto.Id = item2.ProductId;
                    producto.Nombre = item2.Product.Name;
                    producto.Numero = item2.Product.ProductNumber;
                    producto.Color = item2.Product.Color ?? "";
                    producto.CostoEstandar = item2.Product.StandardCost;
                    producto.Precio = item2.Product.ListPrice;
                    producto.Tamaño = item2.Product.Size ?? "";
                    producto.Peso = item2.Product.Weight ?? 0;
                    producto.Clase = item2.Product.Class ?? "";
                    producto.Estilo = item2.Product.Style ?? "";
                    //producto.Categoria = item2.Product.ProductSubcategory?.Name ?? "";
                    //producto.Modelo = item2.Product.ProductModel?.Name ?? "";
                    producto.VentaFin = item2.Product.SellStartDate;
                    producto.VentaFin = item2.Product.SellEndDate;
                    producto.FechaDescontinuado = item2.Product.DiscontinuedDate;
                    producto.UltimoCambio = item2.Product.ModifiedDate;

                    producto.UltimoCambioOferta = item2.ModifiedDate;

                    oferta.Productos.Add(producto);


                }
                list.Add(oferta);


            }
            return list;
            
        }

        public async Task<OfertaDto> GetOfertasById(int specialOfferId)
        {

            var item = await _context.SpecialOffers
                .Include(of => of.SpecialOfferProducts).ThenInclude(sop => sop.Product).ThenInclude(p => p.ProductModel)
                .Include(of => of.SpecialOfferProducts).ThenInclude(sop => sop.Product).ThenInclude(p => p.ProductSubcategory).
                FirstOrDefaultAsync(m=>m.SpecialOfferId==specialOfferId);
           
                OfertaDto oferta = new OfertaDto();
            if (item != null)
            {
                oferta.Id = item.SpecialOfferId;
                oferta.Descripcion = item.Description;
                oferta.Tipo = item.Type;
                oferta.Categoria = item.Category;
                oferta.FechaInicio = item.StartDate.ToShortDateString();
                oferta.FechaFin = item.EndDate.ToShortDateString();
                oferta.MinQty = item.MinQty;
                oferta.MaxQty = item.MaxQty;
                oferta.UltimaModificacion = item.ModifiedDate.ToShortDateString();
                oferta.Porcentaje = item.DiscountPct;
                oferta.Productos = new();
                foreach (var item2 in item.SpecialOfferProducts)
                {
                    ProductoDto producto = new ProductoDto();
                    producto.Id = item2.ProductId;
                    producto.Nombre = item2.Product.Name;
                    producto.Numero = item2.Product.ProductNumber;
                    producto.Color = item2.Product.Color ?? "";
                    producto.CostoEstandar = item2.Product.StandardCost;
                    producto.Precio = item2.Product.ListPrice;
                    producto.Tamaño = item2.Product.Size ?? "";
                    producto.Peso = item2.Product.Weight ?? 0;
                    producto.Clase = item2.Product.Class ?? "";
                    producto.Estilo = item2.Product.Style ?? "";
                    producto.Categoria = item2.Product.ProductSubcategory?.Name ?? "";
                    producto.Modelo = item2.Product.ProductModel?.Name ?? "";
                    producto.VentaFin = item2.Product.SellStartDate;
                    producto.VentaFin = item2.Product.SellEndDate;
                    producto.FechaDescontinuado = item2.Product.DiscontinuedDate;
                    producto.UltimoCambio = item2.Product.ModifiedDate;

                    producto.UltimoCambioOferta = item2.ModifiedDate;

                    oferta.Productos.Add(producto);


                }
            }


            
            return oferta;

        }


        public async Task<List<ProductoDto>> getProductos()
        {
            var query = await _context.Products.ToListAsync();
            List<ProductoDto> list = new List<ProductoDto>();
            foreach(var item in query) {
                ProductoDto producto = new ProductoDto();
                producto.Id = item.ProductId;
                producto.Nombre = item.Name;
                producto.Numero = item.ProductNumber;
                producto.Color = item.Color ?? "";
                producto.CostoEstandar = item.StandardCost;
                producto.Precio = item.ListPrice;
                producto.Tamaño = item.Size ?? "";
                producto.Peso = item.Weight ?? 0;
                producto.Clase = item.Class ?? "";
                producto.Estilo = item.Style ?? "";
                producto.Categoria = item.ProductSubcategory?.Name ?? "";
                producto.Modelo = item.ProductModel?.Name ?? "";
                producto.VentaFin = item.SellStartDate;
                producto.VentaFin = item.SellEndDate;
                producto.FechaDescontinuado = item.DiscontinuedDate;
                producto.UltimoCambio = item.ModifiedDate;
                list.Add(producto);
            }

            return list;

        }
        public async  Task<int> createOffert(OfertaDto oferta)
        {
            SpecialOffer offer = new SpecialOffer();
            offer.Description = oferta.Descripcion;
            offer.DiscountPct = oferta.Porcentaje/100;
            offer.Type = oferta.Tipo;
            offer.Category = oferta.Categoria;
            offer.StartDate = DateTime.Parse(oferta.FechaInicio);
            offer.EndDate = DateTime.Parse(oferta.FechaFin);
            offer.MinQty= oferta.MinQty;
            offer.MaxQty= oferta.MaxQty;
            offer.ModifiedDate = DateTime.Now;

            var offerResponse = await _context.AddAsync(offer);


           return await _context.SaveChangesAsync();
            
        }

        public void updateOffertData() { }
        public async void addProductToOffer(int idOferta,int idProducto) {
        
            SpecialOfferProduct sop= new SpecialOfferProduct();

            sop.SpecialOfferId = idOferta;
            sop.ProductId = idProducto;
            sop.ModifiedDate= DateTime.Now;
            await _context.AddAsync(sop);
            await _context.SaveChangesAsync();
        }
        public async Task<int> deleteOffert(int idOffer) {


            SpecialOffer ofer = _context.SpecialOffers.Include(of => of.SpecialOfferProducts).Where(of => of.SpecialOfferId == idOffer).FirstOrDefault();

            if(ofer!=null )
            {
                foreach(SpecialOfferProduct asign in ofer.SpecialOfferProducts)
                {
                    _context.Remove(asign);
                }
                _context.Remove(ofer);
              return  await _context.SaveChangesAsync();
            }


            return -1;

        }

        public async Task<int> UpdateOferta(OfertaDto oferta)
        {

            SpecialOffer offer = new SpecialOffer();
            offer.SpecialOfferId = oferta.Id;
            offer.Description = oferta.Descripcion;
            offer.DiscountPct = oferta.Porcentaje / 100;
            offer.Type = oferta.Tipo;
            offer.Category = oferta.Categoria;
            offer.StartDate = DateTime.Parse(oferta.FechaInicio);
            offer.EndDate = DateTime.Parse(oferta.FechaFin);
            offer.MinQty = oferta.MinQty;
            offer.MaxQty = oferta.MaxQty;
            offer.ModifiedDate = DateTime.Now;
            _context.Update(offer);


            return await _context.SaveChangesAsync();
        }
       
        
    }
}
