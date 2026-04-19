using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    public class Status : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public const string NORMAL = "1";
        /// <summary>
        /// 
        /// </summary>
        public const string EN_EJECUCION = "2";
        /// <summary>
        /// 
        /// </summary>
        public const string EJECUTADA = "3";

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public virtual string Nombre
        {
            get;
            set;
        }
    }
}
