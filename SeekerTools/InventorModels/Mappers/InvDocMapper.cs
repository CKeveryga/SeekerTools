using Inventor;

namespace SeekerTools.InventorModels
{
    public abstract class InvDocMapper : IInvDoc
    {
        public InvDocMapper(Document doc)
        {
            Initialise(doc);
        }

        protected InvDocMapper(PartDocument partDocument)
        {
            var doc = (Document)partDocument;
            Initialise(doc);
        }

        protected InvDocMapper(AssemblyDocument assemblyDocument)
        {
            var doc = (Document)assemblyDocument;
            Initialise(doc);
        }

        private void Initialise(Document doc)
        {
            DesignTrackPropSet = doc.PropertySets["Design Tracking Properties"];
            PartNumber = DesignTrackPropSet["Part Number"].Value.ToString();
            Vendor = DesignTrackPropSet["Vendor"].Value.ToString();

            Description = DesignTrackPropSet["Description"].Value.ToString();

            CustomPropSet = doc.PropertySets["Inventor User Defined Properties"];
            DocumentType = doc.DocumentType;

            try
            {
                Revision = int.Parse(DesignTrackPropSet["Revision"].Value.ToString());
            }
            catch { }
            try
            {
                StepNumber = int.Parse(DesignTrackPropSet["Step Number"].Value.ToString());
            }
            catch { }
            try
            {
                CostCenter = int.Parse(DesignTrackPropSet["Cost Center"].Value.ToString());
            }
            catch { }
        }

        public PropertySet DesignTrackPropSet { get; private set; }

        public PropertySet CustomPropSet { get; private set; }

        public string PartNumber { get; private set; }

        public string Description { get; private set; }

        public string Vendor { get; private set; }

        public DocumentTypeEnum DocumentType { get; private set; }

        public int Revision { get; private set; }

        public int StepNumber { get; private set; }

        public int CostCenter { get; private set; }
    }
}
