using AdventureWorks.Dto;
using AdventureWorks.Models;
using AdventureWorks.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Controllers
{
    public class OfertaController : Controller
    {
        private readonly IOfertaRepository _ofertaRepository;
        public OfertaController(IOfertaRepository repo) {
        _ofertaRepository = repo;
        }
        
        public async Task<IActionResult> Ofertas()
        {
            return View(await _ofertaRepository.GetOfertas());
        }
        
        public async Task<IActionResult> CrearOferta()
        {
            return View();
        }
        public async Task<IActionResult> AgregarProducto(int id)
        {
            OfertaDto oferta = await _ofertaRepository.GetOfertasById(id);
            List<ProductoDto> productos = await _ofertaRepository.getProductos();
            List<ProductoDto> productosf = productos;
            foreach (ProductoDto producto in oferta.Productos)
            {
                productosf = productosf.FindAll(p => p.Id != producto.Id);
            }
            oferta.AllProductos = productosf;

            ViewData["productos"] = productosf;
            


            return View(oferta);
        }

        public async Task<IActionResult> EliminarOferta(int id)
        {
            OfertaDto oferta = await _ofertaRepository.GetOfertasById(id);
            return View(oferta);
        }
       
        public async Task<IActionResult> EditarOferta(int id)
        {
            OfertaDto oferta = await _ofertaRepository.GetOfertasById(id);
            return View(oferta);
        }
        
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                int resp = await _ofertaRepository.deleteOffert(id);

            }
            catch (Exception ex)
            {
                string messae = ex.Message;
    
            }
            return RedirectToAction(nameof(Ofertas));
        }

        public async Task<IActionResult> Agregar([Bind()] OfertaDto oferta)
        {


            List<ProductoDto>produ = oferta.AllProductos.FindAll(e=>e.IsSelected).ToList();
            List<OfertaProductoDto> list = new();

            foreach (ProductoDto prod in produ)
            {
                OfertaProductoDto OD = new()
                {
                    ProductId = prod.Id,
                    OfferId = oferta.Id
                };
                list.Add(OD);
            }

            int resp =await _ofertaRepository.addProductToOffer(list);
            return RedirectToAction(nameof(Ofertas));

            //return RedirectToAction(nameof(EditarOferta), oferta.Id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Descripcion,Tipo,Categoria,MinQty,MaxQty,Porcentaje,FechaInicio,FechaFin")] OfertaDto oferta)
        {
            //if (ModelState.IsValid)
            if (oferta != null) 
            {
                try
                {
                   int resp = await _ofertaRepository.createOffert(oferta);
                }catch (Exception ex) {
                string messae = ex.Message;

                }
            }
            return RedirectToAction(nameof(Ofertas));
        }

        [HttpPost]
        public async Task<IActionResult> Editar([Bind("Descripcion,Tipo,Categoria,MinQty,MaxQty,Porcentaje,FechaInicio,FechaFin")] OfertaDto oferta)
        {
            //if (ModelState.IsValid)
            if (oferta != null)
            {
                try
                {
                    int resp = await _ofertaRepository.UpdateOferta(oferta);
                }
                catch (Exception ex)
                {
                    string messae = ex.Message;

                }
            }
            return RedirectToAction(nameof(Ofertas));
        }

    }
}
