using e_Commerce.Muebles.Entidades;
using e_Commerce.Muebles.Repos;
using System.Collections.Generic;

namespace e_Commerce.Muebles.Services
{
    public class CarritoService
    {
        private readonly ICarritoRepository _carritoRepository;

        public CarritoService(ICarritoRepository carritoRepository)
        {
            _carritoRepository = carritoRepository;
        }

        public bool AgregarProductoAlCarrito(int clienteId, int productoId, int cantidad)
        {
            // Verificamos si el producto ya existe en el carrito
            var carritos = _carritoRepository.GetCarritos(clienteId);
            var carritoItem = carritos?.FirstOrDefault(c => c.producto_id == productoId);

            if (carritoItem != null)
            {
                // Si ya existe, aumentamos la cantidad
                int nuevaCantidad = carritoItem.cantidad + cantidad;
                return _carritoRepository.EditarCantidadProducto(clienteId, productoId, nuevaCantidad);
            }
            else
            {
                // Si no existe, lo agregamos
                return _carritoRepository.AgregarProducto(productoId, clienteId, cantidad);
            }
        }

        public bool EliminarProductoDelCarrito(int clienteId, int productoId)
        {
            return _carritoRepository.EliminarProducto(productoId, clienteId);
        }

        public bool ActualizarCantidadProducto(int clienteId, int productoId, int nuevaCantidad)
        {
            return _carritoRepository.EditarCantidadProducto(clienteId, productoId, nuevaCantidad);
        }

        public IEnumerable<Carrito> ObtenerCarritoDeCliente(int clienteId)
        {
            return _carritoRepository.GetCarritos(clienteId);
        }
    }
}
