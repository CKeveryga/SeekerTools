using Inventor;
using SeekerTools.ExternalAbstractions;
using System;

namespace SeekerTools.InventorAccess
{
    /// <summary>
    /// Traverses Through the Levels of an Inventor Occurrence
    /// </summary>
    internal class AssemblyTraverser : IAssemblyTraverser
    {
        public AssemblyTraverser(IOccurrenceTreater treatOcc)
        {
            _treatOccurrence = treatOcc;
        }
        /// <summary>
        /// DI of occurrence treater
        /// </summary>
        IOccurrenceTreater _treatOccurrence;
        /// <summary>
        /// Traverses into the next layer of sub occurrences
        /// </summary>
        /// <param name="compOccs">SubOccurrences</param>
        /// <param name="depth">Depth of current occurrence</param>
        /// <param name="parentOccurrence">parent occurrence of current occurrence</param>
        public void OccLoop(ComponentOccurrences compOccs, int depth, string parentId)
        {
            depth++;
            foreach (ComponentOccurrence occ in compOccs)
            {
                TraverseOccurrence(occ, depth, parentId);
            }
            depth--;
        }
        /// <summary>
        /// Traverses into the next layer of sub occurrences
        /// </summary>
        /// <param name="compOccs">SubOccurrences</param>
        /// <param name="depth">Depth of current occurrence</param>
        /// <param name="parentOccurrence">parent occurrence of current occurrence</param>
        public void OccLoop(ComponentOccurrencesEnumerator compOccs, int depth, string parentId)
        {
            depth++;
            foreach (ComponentOccurrence occ in compOccs)
            {
                TraverseOccurrence(occ, depth, parentId);
            }
            depth--;
        }
        /// <summary>
        /// Depending on occurrence type, treats occurrence and steps into suboccurrences
        /// </summary>
        /// <param name="compOccs">SubOccurrences</param>
        /// <param name="depth">Depth of current occurrence</param>
        /// <param name="parentOccurrence">parent occurrence of current occurrence</param>
        /// <exception cref="Exception">Exception for unknown occurrence type</exception>
        public void TraverseOccurrence(ComponentOccurrence compOcc, int depth, string parentId)
        {
            if (compOcc.Definition.Type == ObjectTypeEnum.kPartComponentDefinitionObject)
            {
                _treatOccurrence.PartOcc(compOcc, depth, parentId);
            }
            else if (compOcc.Definition.Type == ObjectTypeEnum.kSheetMetalComponentDefinitionObject)
            {
                _treatOccurrence.SheetMetalOcc(compOcc, depth, parentId);
            }
            else if (compOcc.Definition.Type == ObjectTypeEnum.kAssemblyComponentDefinitionObject)
            {
                _treatOccurrence.AssemblyOcc(compOcc, depth, parentId);
                //Set parentAssembly here
                AssemblyDocument aDoc = (AssemblyDocument)compOcc.Definition.Document;
                string tempPartNumber = aDoc.PropertySets["{32853F0F-3444-11D1-9E93-0060B03C1CA6}"]["Part Number"].Value.ToString();
                OccLoop(compOcc.SubOccurrences, depth, tempPartNumber);
            }
            else
            {
                throw new Exception("Check Occurrence Type...");
            }
        }
    }
}
