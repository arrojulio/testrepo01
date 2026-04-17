using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    public abstract class BaseViewModel
    {
        public override string ToString()
        {
            //object value = this.GetType().GetProperty("Nombre").GetValue(this, null);
            //return value != null ? value.ToString() : "(empty)";
            return string.IsNullOrEmpty(Nombre) ? "(empty)" : Nombre;
        }

        public virtual string Nombre { get; set; }
    }
}