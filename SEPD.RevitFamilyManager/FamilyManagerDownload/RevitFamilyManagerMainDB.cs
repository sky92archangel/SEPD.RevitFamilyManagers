﻿

using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
//using SEPD.RevitFamilyPlatform;
using SEPD.CommunicationModule;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Shell;

namespace SEPD.RevitFamilyManager
{

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class RevitFamilyManagerMainDB : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            const string sqlConnectionLocation = "server=10.193.217.38,1433;uid=sa;Password=SanWei2209;database=DB_Family_Library";
            const string defaultLoginPath = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding";
            const string defaultTempPath = @"C:\RFA_TMP\";
            const string defaultTempPath2 = @"C:\ProgramData\Autodesk\Revit\Addins\2016\SepdBuliding\parameter_excel\parameter_perview";
            DotNet.Utilities.LogHelper.LogMonitor("table_Log", "族库平台批量下载功能");
            //下次开启清空临时空间
            try
            {
                if (Directory.Exists(defaultTempPath))
                {
                    Directory.Delete(defaultTempPath, true);
                }
                if (Directory.Exists(defaultTempPath2))
                {
                    Directory.Delete(defaultTempPath2, true);
                }
            }
            catch (Exception efe)
            {
            }


            RevitFamilyManagerFormDB RevitFamilyManagerFormDB = new RevitFamilyManagerFormDB();
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //下次开启清空临时空间
            try
            {
                if (Directory.Exists(@"C:\RFA_TMP\"))
                {
                    Directory.Delete(@"C:\RFA_TMP\", true);
                }
            }
            catch (Exception efe)
            { }

            RevitFamilyManagerFormDB.Show();


            DataTable dt;
            List<string> familyFilePaths = new List<string>();
            List<string> parameterFilePaths = new List<string>();

            return Result.Succeeded;
     
        }
    }

    class MyFamilyLoadOptions : IFamilyLoadOptions
    {
        public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
        {
            overwriteParameterValues = true;
            return true;
        }
        public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
        {
            source = FamilySource.Project;
            overwriteParameterValues = true;
            return true;
        }
    }


}
