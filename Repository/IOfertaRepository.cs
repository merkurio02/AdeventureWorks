using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Dto;
using AdventureWorks.Models;

namespace AdventureWorks.Repository
{
    public interface IOfertaRepository
    {
        Task<List<OfertaDto>> GetOfertas();
        Task<int> createOffert(OfertaDto oferta);
        Task<OfertaDto> GetOfertasById(int specialOfferId);

        Task<int> UpdateOferta(OfertaDto oferta);
        Task<List<ProductoDto>> getProductos();
        Task<int> deleteOffert(int idOffer);

        Task<int> addProductToOffer(List<OfertaProductoDto> OPs);
    }
}
