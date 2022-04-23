using Inventor;

namespace SeekerTools.InventorServices.Export
{
    /// <summary>
    /// Replaces file extension to output type
    /// </summary>
    internal static class FileNameHelper
    {
        /// <summary>
        /// Replace Inventor extension with output extension
        /// </summary>
        /// <param name="document"></param>
        /// <param name="extensionOut"></param>
        /// <returns></returns>
        public static string FileName(PartDocument document, string extensionOut)
        {
            return document.FullFileName.Replace(".ipt", extensionOut);
        }
        /// <summary>
        /// Replace Inventor extension with output extension
        /// </summary>
        /// <param name="document"></param>
        /// <param name="extensionOut"></param>
        /// <returns></returns>
        public static string FileName(AssemblyDocument document, string extensionOut)
        {
            return document.FullFileName.Replace(".iam", extensionOut);
        }
        /// <summary>
        /// Replace Inventor extension with output extension
        /// </summary>
        /// <param name="document"></param>
        /// <param name="extensionOut"></param>
        /// <returns></returns>
        public static string FileName(DrawingDocument document, string extensionOut)
        {
            return document.FullFileName.Replace(".idw", extensionOut);
        }
    }
}
