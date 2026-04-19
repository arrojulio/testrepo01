using System.Collections.Generic;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Services
{
    /// <summary>
    /// Interfaz que determina la funcionalidad a implementar por un servicio.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T> where T: EntityBase
    {
        /// <summary>
        /// Obtiene un elemento dado su <paramref name="key"/>
        /// </summary>
        /// <param name="key">Key del elemento.</param>
        /// <returns><typeparamref name="T"/></returns>
        T FindBy(object key);

        /// <summary>
        /// Añade un elemento.
        /// </summary>
        /// <param name="item"><typeparamref name="T"/> a añadir.</param>
        /// <returns><typeparamref name="T"/> actualizado.</returns>
        T Add(T item);

        T this[object key] { get; set; }

        /// <summary>
        /// Elimina un elemento del tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="item"><typeparamref name="T"/></param>
        void Remove(T item);
        
        /// <summary>
        /// Obtiene <see cref="List{T}"/>.
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Obtiene una nueva instancia de <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        T GetNewInstance();

        /// <summary>
        /// Obtiene la cantidad de elementos en el repositorio.
        /// </summary>
        /// <returns></returns>
        long Count();
        
    }
}
