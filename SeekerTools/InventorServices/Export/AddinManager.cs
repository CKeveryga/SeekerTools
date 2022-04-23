using System;
using Inventor;
using Microsoft.Extensions.Logging;

namespace SeekerTools.InventorServices.Export
{
    internal class AddinManager : IExportManager
    {
        /// <summary>
        /// Enum for type of Addin to configure
        /// </summary>
        public enum ExporterAddinEnum
        {
            PDF,
            DWG,
            DWF,
            DXF
        }
        private readonly string _pdfTransId = "{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}";
        private readonly string _dwgTransId = "{C24E3AC2-122E-11D5-8E91-0010B541CD80}";
        private readonly string _dxfTransId = "{C24E3AC4-122E-11D5-8E91-0010B541CD80}";
        private readonly string _dwfTransId = "{0AC6FD95-2F4D-42CE-8BE0-8AEA580399E4}";
        private readonly string _iniFile;
        private readonly TranslatorAddIn _transAddin;
        private readonly TransientObjects _transientObjects;
        private readonly ApplicationAddIns _addins;
        private readonly ILogger _logger;
        private readonly TranslationContext _translationContext;
        private readonly NameValueMap _nameValueMap;
        private readonly DataMedium _dataMedium;
        /// <summary>
        /// Create exporter using Translator Addin
        /// </summary>
        /// <param name="transientObjects"></param>
        /// <param name="addins"></param>
        /// <param name="exporterEnum"></param>
        /// <param name="logger"></param>
        public AddinManager(TransientObjects transientObjects, ApplicationAddIns addins, ExporterAddinEnum exporterEnum, ILogger logger, string dwgDwfIniFile = "")
        {
            _transientObjects = transientObjects;
            _addins = addins;
            _logger = logger;
            _translationContext = _transientObjects.CreateTranslationContext();
            _translationContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;
            _iniFile = dwgDwfIniFile;
            // setup addin based on type
            switch (exporterEnum)
            {
                case ExporterAddinEnum.PDF:
                    _nameValueMap = ConfigurePdfOptions(_transientObjects.CreateNameValueMap());
                    _transAddin = GetAddinById(_pdfTransId, _addins);
                    break;
                case ExporterAddinEnum.DWG:
                    _nameValueMap = ConfigureDwgOptions(_transientObjects.CreateNameValueMap(), _iniFile);
                    _transAddin = GetAddinById(_dwgTransId, _addins);
                    break;
                case ExporterAddinEnum.DWF:
                    _nameValueMap = ConfigureDwfOptions(_transientObjects.CreateNameValueMap());
                    _transAddin = GetAddinById(_dwfTransId, _addins);
                    break;
                case ExporterAddinEnum.DXF:
                    _nameValueMap = ConfigureDwgOptions(_transientObjects.CreateNameValueMap(), _iniFile);
                    _transAddin = GetAddinById(_dxfTransId, _addins);
                    break;
                default:
                    break;
            }
            _dataMedium = _transientObjects.CreateDataMedium();
        }
        /// <summary>
        /// Export Drawing Document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exportFullFileName"></param>
        public void Export(DrawingDocument document, string exportFullFileName)
        {
            _dataMedium.FileName = exportFullFileName;
            try
            {
                _transAddin.SaveCopyAs(document, _translationContext, _nameValueMap, _dataMedium);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        /// <summary>
        /// Export Part Document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exportFullFileName"></param>
        public void Export(PartDocument document, string exportFullFileName)
        {
            _dataMedium.FileName = exportFullFileName;
            try
            {
                _transAddin.SaveCopyAs(document, _translationContext, _nameValueMap, _dataMedium);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        /// <summary>
        /// Export Assembly Document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exportFullFileName"></param>
        public void Export(AssemblyDocument document, string exportFullFileName)
        {
            _dataMedium.FileName = exportFullFileName;
            try
            {
                _transAddin.SaveCopyAs(document, _translationContext, _nameValueMap, _dataMedium);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private NameValueMap ConfigurePdfOptions(NameValueMap nameValueMap)
        {
            nameValueMap.Value["All_Color_AS_Black"] = "False";
            nameValueMap.Value["Remove_Line_Weights"] = 1;
            nameValueMap.Value["Vector_Resolution"] = 200;
            nameValueMap.Value["Sheet_Range"] = PrintRangeEnum.kPrintAllSheets;
            return nameValueMap;
        }

        private NameValueMap ConfigureDwgOptions(NameValueMap nameValueMap, string iniFile)
        {
            if (string.IsNullOrWhiteSpace(iniFile) == false)
                nameValueMap.Add("Export_Acad_IniFile", iniFile);
            nameValueMap.Value["USE_TRANSMITTAL"] = "No";
            return nameValueMap;
        }

        private NameValueMap ConfigureDwfOptions(NameValueMap nameValueMap)
        {
            nameValueMap.Value["Launch_Viewer"] = 0;
            nameValueMap.Value["Publish_All_Component_Props"] = 1;
            nameValueMap.Value["Publish_All_Physical_Props"] = 1;
            nameValueMap.Value["Publish_3D_Models"] = 1;
            return nameValueMap;
        }

        private TranslatorAddIn GetAddinById(string translatorId, ApplicationAddIns addIns)
        {
            TranslatorAddIn translatorAddIn;
            translatorAddIn = (TranslatorAddIn)addIns.ItemById[translatorId];
            if (translatorAddIn == null)
                throw new Exception("Unable to load addin.");
            if (!translatorAddIn.Activated)
                translatorAddIn.Activate();
            return translatorAddIn;
        }
    }
}
