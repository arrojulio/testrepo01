using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Clase encargada de representar la informacion de sincronizacion de TEINSA.
    /// Representa el estado de la operacion.
    /// </summary>
    public class SyncLog : EntityBase
    {
        public static String Status_SUCCESS = "Success";
        public static String Status_FAIL = "Fail";

        /// <summary>
        /// TimeStamp de la operacion de Sync
        /// </summary>
        public virtual DateTime TimeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// Status de la operacion sync
        /// Success | Fail
        /// </summary>
        private String _Status;
        public virtual String Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (value != Status_SUCCESS && value != Status_FAIL) throw new Exception("Invalid property value");
                _Status = value;
            }
        }

        /// <summary>
        /// Fecha corte procesada
        /// </summary>
        public virtual DateTime? FechaCorte
        {
            get;
            set;
        }

        /// <summary>
        /// Lista separada por espacios de los numeros IDs agregados
        /// </summary>
        public virtual string ItemsAdded
        {
            get;
            set;
        }

        /// <summary>
        /// Lista separada por espacios de los numeros IDs actualizados
        /// </summary>
        public virtual string ItemsUpdated
        {
            get;
            set;
        }

        /// <summary>
        /// Lista separada por espacios de los numeros IDs borrados
        /// </summary>
        public virtual string ItemsDeleted
        {
            get;
            set;
        }

        /// <summary>
        /// Mensage de Log
        /// </summary>
        public virtual string Message
        {
            get;
            set;
        }

        public SyncLog(string status, string message)
        {
            this.Status = status;
            this.Message = message;
            this.TimeStamp = DateTime.Now;

        }

        public SyncLog(string status, string message, DateTime fechaCorte, string itemsAdded, string itemsUpdated, string itemsDeleted)
        {
            this.Status = status;
            this.Message = message;
            this.FechaCorte = fechaCorte;
            this.ItemsAdded = itemsAdded;
            this.ItemsUpdated = itemsUpdated;
            this.ItemsDeleted = itemsDeleted;
            this.TimeStamp = DateTime.Now;

        }
    }
}
