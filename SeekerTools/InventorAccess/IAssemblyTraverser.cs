using Inventor;

namespace SeekerTools.InventorAccess
{
    public interface IAssemblyTraverser
    {
        /// <summary>
        /// Traverses into the next layer of sub occurrences
        /// </summary>
        /// <param name="compOccs">SubOccurrences</param>
        /// <param name="depth">Depth of current occurrence</param>
        /// <param name="parentOccurrence">parent occurrence of current occurrence</param>
        void OccLoop(ComponentOccurrences compOccs, int depth, string parentId);
        /// <summary>
        /// Traverses into the next layer of sub occurrences
        /// </summary>
        /// <param name="compOccs">SubOccurrences</param>
        /// <param name="depth">Depth of current occurrence</param>
        /// <param name="parentOccurrence">parent occurrence of current occurrence</param>
        void OccLoop(ComponentOccurrencesEnumerator compOccs, int depth, string parentId);
        /// <summary>
        /// Depending on occurrence type, treats occurrence and steps into suboccurrences
        /// </summary>
        /// <param name="compOccs">SubOccurrences</param>
        /// <param name="depth">Depth of current occurrence</param>
        /// <param name="parentOccurrence">parent occurrence of current occurrence</param>
        void TraverseOccurrence(ComponentOccurrence occ, int depth, string parentId);
    }
}