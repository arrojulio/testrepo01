using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Presentation.Website.Models;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The maker checker operation viewer view model class.
    /// </summary>
    public class MakerCheckerOperationViewerViewModel 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerOperationViewerViewModel"/> class.
        /// </summary>
        /// <param name="operation">The operation of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        public MakerCheckerOperationViewerViewModel(MakerCheckerOperation operation)
        {
            this.Operation = operation;
            this.Model = ((GarantiaBase)this.Operation.GetMakerCheckerObject().Object).CreateModel();
            // Add an identifier to the record
            this.Proposed = new Dictionary<string, object> {{"Id. Temporal", operation.OperationId}};
            IDictionary<string, object> properties = this.Model.GetPropertiesForViewer();
            // if we have properties include them.
            if (properties != null)
            {
                // Filter only not added properties
                foreach (var p in properties.Where(o=>!this.Proposed.Keys.Contains(o.Key)))
                {
                    this.Proposed.Add(p);
                }
            }
        }

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/>
        /// </value>
        public MakerCheckerOperation Operation { get; set; }
        /// <summary>
        /// Gets or sets the proposed.
        /// </summary>
        /// <value>
        /// The proposed of type <see cref="object"/>
        /// </value>
        public IDictionary<string, object> Proposed { get; set; }
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/>
        /// </value>
        public GarantiaBaseModel Model { get; set; }
    }

    /// <summary>
    /// The maker checker changeset viewer view model class.
    /// </summary>
    public class MakerCheckerChangesetViewerViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerChangesetViewerViewModel"/> class.
        /// </summary>
        public MakerCheckerChangesetViewerViewModel()
        {
            this.Operations = new List<MakerCheckerOperationViewerViewModel>();
        }

        /// <summary>
        /// Gets or sets the changeset.
        /// </summary>
        /// <value>
        /// The changeset of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/>
        /// </value>
        public MakerCheckerChangeset Changeset { get; set; }
        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        /// <value>
        /// The operations of type <see cref="System.Collections.Generic.List&lt;Bladex.Garantias.Presentation.Website.ViewModels.MakerCheckerOperationViewerViewModel&gt;"/>
        /// </value>
        public List<MakerCheckerOperationViewerViewModel> Operations { get; set; }

        /// <summary>
        /// Gets the columns to display.
        /// </summary>
        /// <returns></returns>
        public List<string> GetColumnsToDisplay()
        {
            List<string> columns = new List<string>();
            foreach (var o in this.Operations)
            {
                foreach (var column in o.Proposed.Keys)
                {
                    if (!columns.Contains(column))
                        columns.Add(column);
                }
            }
            return columns;
        }
    }
}