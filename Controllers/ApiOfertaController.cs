using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.Repository;
using AdventureWorks.Models;
using AdventureWorks.Dto;

namespace AdventureWorks.Controllers
{
    [ApiController]
    [Route("api/Oferta")]
    public class ApiOfertaController : ControllerBase
    {
        private IOfertaRepository _oferta;
        public ApiOfertaController(IOfertaRepository oferta) {
            _oferta = oferta;
        }

        [HttpGet]
        public async  Task<ActionResult<List<OfertaDto>>> Get()
        {
            try
            {


                return await _oferta.GetOfertas();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.ToString());
            }
                    
        }

        [HttpPost]
        public async void Save(OfertaDto oferta)
        {
            try
            {
                 _oferta.createOffert(oferta);
            }catch (Exception ex)
            {

            }
        }
        [HttpPost]
        public async void Update(OfertaDto oferta)
        {
            try
            {
                _oferta.UpdateOferta(oferta);
            }
            catch (Exception ex)
            {

            }
        }
        [HttpPost]
        public async void delete(int idOferta)
        {
            try
            {
                _oferta.deleteOffert(idOferta);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
