using Inventor;
using SeekerTools.InventorModels;

namespace SeekerTools
{
    public static class SeekerModels
    {
        public static IInvAsmDoc MapAssemblyDocument(AssemblyDocument document)
        {
            return new InvAsmDocMapper(document);
        }

        public static IInvPartDoc MapPartDocument(PartDocument document)
        {
            return new InvPartDocMapper(document);
        }
    }
}
