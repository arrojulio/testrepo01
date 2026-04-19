using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bladex.Garantias.Presentation.Website.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace Bladex.Garantias.Presentation.Website.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}