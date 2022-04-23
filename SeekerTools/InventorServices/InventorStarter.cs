using Inventor;
using System;
using System.Threading.Tasks;

namespace SeekerTools.InventorServices
{
    /// <summary>
    /// Open Inventor in the background with options
    /// </summary>
    internal class InventorStarter
    {
        /// <summary>
        /// Open Inventor with options and error catching
        /// </summary>
        /// <returns></returns>
        public Application StartInventor()
        {
            Application invApp;
            try
            {
                Type oType = Type.GetTypeFromProgID("Inventor.Application");
                invApp = (Application)Activator.CreateInstance(oType);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (NotSupportedException ex)
            {
                throw ex;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                throw ex;
            }
            catch (MethodAccessException ex)
            {
                throw ex;
            }
            catch (MissingMethodException ex)
            {
                throw ex;
            }
            catch (MemberAccessException ex)
            {
                throw ex;
            }
            catch (System.Runtime.InteropServices.InvalidComObjectException ex)
            {
                throw ex;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw ex;
            }
            catch (TypeLoadException ex)
            {
                throw ex;
            }
            // Make inventor invisible
            invApp.Visible = false;
            invApp.SilentOperation = true;
            // Settings
            invApp.DrawingOptions.EnableBackgroundUpdates = false;
            invApp.HardwareOptions.GraphicsSettingType = GraphicsSettingTypeEnum.kPerformanceGraphicsSetting;
            invApp.SaveOptions.ShowSaveReminder = false;
            return invApp;
        }
        /// <summary>
        /// Start Inventor instance asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<Application> StartInventorAsync()
        {
            return await Task.Run(() => StartInventor());
        }
    }
}
