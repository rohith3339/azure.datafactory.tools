using System;
using System.Collections;
using System.Collections.Generic;
//using System.Text.Json;
//using System.Text.Json.Serialization;
using Microsoft.Azure.Commands.DataFactoryV2.Models;

namespace SQLPlayer.AdfTools {


    public class Adf {

        // Constructor
        public Adf(string name)
        {
            this.Name = name;
        }

        public String Name;
        public String ResourceGroupName;

        public List<AdfObject> Pipelines = new List<AdfObject>();
        public List<AdfObject> LinkedServices = new List<AdfObject>();
        public List<AdfObject> DataSets = new List<AdfObject>();
        public List<AdfObject> DataFlows = new List<AdfObject>();
        public List<AdfObject> Triggers = new List<AdfObject>();
        public List<AdfObject> IntegrationRuntimes = new List<AdfObject>();
        public String Location;

        public List<AdfObject> AllObjects()
        {
            List<AdfObject> combined = new List<AdfObject>();
            combined.AddRange(LinkedServices);
            combined.AddRange(Pipelines);
            combined.AddRange(DataSets);
            combined.AddRange(DataFlows);
            combined.AddRange(Triggers);
            combined.AddRange(IntegrationRuntimes);
            return combined;
        }

        public Hashtable GetObjectsByFullName(string pattern)
        {
            Hashtable r = new Hashtable();
            // this.AllObjects() | ForEach-Object {
            //     $oname = $_.FullName($false);
            //     if ($oname -like $pattern) { 
            //         $null = $r.Add($oname, $_)
            //     }
            // }
            return r;
        }

        public Hashtable GetObjectsByFolderName(string folder)
        {
            Hashtable r = new Hashtable();
            // this.AllObjects() | ForEach-Object {
            //     $ofn = $_.GetFolderName()
            //     if ($ofn -eq $folder) 
            //     { 
            //         $oname = $_.FullName($false);
            //         $null = $r.Add($oname, $_)
            //     }
            // }
            return r;
        }

    }

    public class AdfInstance {
        public String Id;
        public String Name;
        public String ResourceGroupName;
        public String Location;
        public List<AdfObject> Pipelines = new List<AdfObject>();
        public List<AdfObject> LinkedServices = new List<AdfObject>();
        public List<PSDataset> DataSets = new List<PSDataset>();
        public List<AdfObject> DataFlows = new List<AdfObject>();
        public List<AdfObject> Triggers = new List<AdfObject>();
        public List<AdfObject> IntegrationRuntimes = new List<AdfObject>();

        public List<AdfObject> AllObjects()
        {
            List<AdfObject> combined = new List<AdfObject>();
            combined.AddRange(LinkedServices);
            combined.AddRange(Pipelines);
            combined.AddRange(DataSets);
            combined.AddRange(DataFlows);
            combined.AddRange(Triggers);
            combined.AddRange(IntegrationRuntimes);
            return combined;
        }
    }


    public class AdfObject {
        // Constructor
        public AdfObject(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name;
        public string Type;
        public string FileName;
        public Hashtable DependsOn = new Hashtable();
        public Boolean Deployed = false;
        public  Boolean ToBeDeployed = true;
        public Adf Adf;
        public object Body;

        public Boolean AddDependant (string name, string type)
        {
            string type2 = type.Replace("Reference", "");
            if (!this.DependsOn.ContainsKey(name)) {
                this.DependsOn.Add( name, type2 );
            }
            return true;
        }

        public String FullName (bool quoted)
        {
            String simtype = GetSimplifiedType(this.Type);
            if (quoted) {
                return String.Format("[{0}].[{1}]", simtype, this.Name);
            } else {
                return String.Format("{0}.{1}", simtype, this.Name);
            }
        }

        public String FullName()
        {
            return this.FullName(false);
        }

        public String FullNameQuoted()
        {
            return this.FullName(true);
        }


        // private AdfObjectJson Deserialize(string json)
        // {
        //     var options = new JsonSerializerOptions
        //     {
        //         AllowTrailingCommas = true
        //     };
        //     return JsonSerializer.Parse<AdfObjectJson>(json, options);
        // }

        // String GetFolderName()
        // {
        //     o = this.Body.properties;
        //     $ofn = null;
        //     if (o.PSobject.Properties.Name -contains "folder")
        //     {
        //         ofn = $_.Body.properties.folder.name
        //     }
        //     return $ofn
        // }

        public static String GetSimplifiedType (String type)
        {            
            String simtype = type;
            if (type.StartsWith("PS")) { simtype = type.Substring(2); }
            if (simtype.EndsWith("IntegrationRuntime")) { simtype = "IntegrationRuntime"; }
            return simtype;
        }

        public static readonly string[] ADF_FOLDERS = { "integrationRuntime", "pipeline", "dataset", "dataflow", "linkedService", "trigger" };

    }

    // public class AdfObjectJson 
    // {
    //     public string Name { get; set; }
    //     public string Properties { get; set; }
    // }




}




// https://docs.microsoft.com/en-us/dotnet/api/system.collections.hashtable?view=netcore-3.1
// https://social.technet.microsoft.com/wiki/contents/articles/27080.powershell-how-to-create-and-use-classes.aspx

