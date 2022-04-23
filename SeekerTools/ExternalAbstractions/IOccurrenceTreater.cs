using Inventor;

namespace SeekerTools.ExternalAbstractions
{
    public interface IOccurrenceTreater
    {
        /// <summary>
        /// Perform checks or other operations on occurrence
        /// </summary>
        /// <param name="compOcc"></param>
        /// <param name="depth"></param>
        /// <param name="parentOccurrence"></param>
        void AssemblyOcc(ComponentOccurrence compOcc, int depth, string parentId);
        /// <summary>
        /// Perform checks or other operations on occurrence
        /// </summary>
        /// <param name="compOcc"></param>
        /// <param name="depth"></param>
        /// <param name="parentOccurrence"></param>
        void PartOcc(ComponentOccurrence compOcc, int depth, string parentId);
        /// <summary>
        /// Perform checks or other operations on occurrence
        /// </summary>
        /// <param name="compOcc"></param>
        /// <param name="depth"></param>
        /// <param name="parentOccurrence"></param>
        void SheetMetalOcc(ComponentOccurrence compOcc, int depth, string parentId);
    }
}