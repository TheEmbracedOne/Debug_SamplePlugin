using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace SamplePlugin
{
    public static class DreamGate
    {
        public static Dictionary<string, KeyValuePair<string, float[]>> DgData = new Dictionary<string, KeyValuePair<string, float[]>>();
        public static int ScrollPosition;
        public static bool AddMenu;
        public static bool DelMenu;
        public static bool DataBusy;

        public static void Reset()
        {
            DgData.Clear();
            ScrollPosition = 0;
            ReadData(false);
        }

        public static void WriteData()
        {
            if (DgData == null) return;
            if (File.Exists("dreamgate.dat"))
            {
                try
                {
                    File.Delete("dreamgate.dat");
                }
                catch (Exception arg)
                {
                    //Modding.ModHooks.ModLog("[DEBUG MOD] [DREAM GATE] Unable to delete existing dreamgate.dat " + arg);
                    Console.AddLine("[DebugMod::DGata] Unable to delete existing dreamgate.dat " + arg);
                    return;
                }
            }
            var num = 0;
            DataBusy = true;
            foreach (var keyValuePair in DgData)
            {
                File.AppendAllText("dreamgate.dat", string.Concat(keyValuePair.Key, "|", keyValuePair.Value.Key, "|", keyValuePair.Value.Value[0], "-", keyValuePair.Value.Value[1], Environment.NewLine));
                num++;
            }
            DataBusy = false;
            if (File.Exists("dreamgate.dat"))
            {
                Console.AddLine("DGdata written sucessfully, entries written: " + num);
            }
        }

        public static void ReadData(bool update)
        {
            if (File.Exists("dreamgate.dat"))
            {
                DataBusy = true;
                if (DgData == null)
                {
                    DgData = new Dictionary<string, KeyValuePair<string, float[]>>();
                }
                if (!update)
                {
                    DgData.Clear();
                }
                var array = File.ReadAllLines("dreamgate.dat");
                if (array.Length == 0)
                {
                    Console.AddLine("Unable to read content of dreamgate.dat properly, file is empty?");
                    //Modding.ModHooks.ModLog("[DEBUG MOD] [DREAM GATE] Unable to read content of dreamgate.dat properly, file is empty?");
                    DataBusy = false;
                    return;
                }
                foreach (var v in array)
                {
                    var num = v.Length - v.Replace("|", "").Length;
                    if (string.IsNullOrEmpty(v) || v.Length >= 500 || v.Length <= 17 || num != 2) continue;
                    var array2 = v.Split('|');
                    if (string.IsNullOrEmpty(array2[0]) || string.IsNullOrEmpty(array2[1]) ||
                        string.IsNullOrEmpty(array2[2])) continue;
                    var key = array2[0];
                    var num2 = 0f;
                    var num3 = 0f;
                    var array3 = array2[2].Split('-');
                    if (array3.Length == 2)
                    {
                        try
                        {
                            num2 = float.Parse(array3[0], CultureInfo.InvariantCulture);
                            num3 = float.Parse(array3[1], CultureInfo.InvariantCulture);
                        }
                        catch (FormatException)
                        {
                            //Modding.ModHooks.ModLog("[DEBUG MOD] [DREAM GATE] FormatException - incorrect float format");
                            Console.AddLine("DGdata::FormatException - incorrect float format");
                            DataBusy = false;
                            return;
                        }
                        catch (OverflowException)
                        {
                            //Modding.ModHooks.ModLog("[DEBUG MOD] [DREAM GATE] OverflowException - incorrect float format");
                            Console.AddLine("DGdata::OverflowException - incorrect float format");
                            DataBusy = false;
                            return;
                        }
                    }
                    if (num2 != 0f && num3 != 0f && !DgData.ContainsKey(key))
                    {
                        DgData.Add(key, new KeyValuePair<string, float[]>(array2[1], new float[]
                        {
                            num2,
                            num3
                        }));
                    }
                }
                DataBusy = false;
                if (DgData.Count <= 0) return;
                Console.AddLine("Filled DGdata: " + DgData.Count);
                //Modding.ModHooks.ModLog("[DEBUG MOD] [DREAM GATE] Filled DGdata: " + DGData.Count);
                return;
            }
            Console.AddLine("File dreamgate.dat not found!");
            //Modding.ModHooks.ModLog("[DEBUG MOD] [DREAM GATE] File dreamgate.dat not found!");
        }

        public static void ClickedEntry(string text)
        {
            if (DelMenu)
            {
                DataBusy = true;
                Console.AddLine("Removed entry " + text + " from the list");
                DgData.Remove(text);
                DataBusy = false;
                DelMenu = false;
            }
            else
            {
                var pd = PlayerData.instance;

                pd.dreamGateScene = DgData[text].Key;
                pd.dreamGateX = DgData[text].Value[0];
                pd.dreamGateY = DgData[text].Value[1];
                
                Console.AddLine("New Dreamgate warp set: " + pd.dreamGateScene + "/" + pd.dreamGateX + "/" + pd.dreamGateY);
            }
        }

        public static void AddEntry(string name)
        {
            if (!string.IsNullOrEmpty(name) && !name.Contains("|") && DgData != null && !DgData.ContainsKey(name) && !DataBusy)
            {
                var value6 = new[] { GUIController.RefKnight.transform.position.x, GUIController.RefKnight.transform.position.y };
                DelMenu = false;
                DataBusy = true;
                DgData.Add(name, new KeyValuePair<string, float[]>(GUIController.Gm.sceneName, value6));
                DataBusy = false;
                Console.AddLine("Added new DGdata entry named: " + name);
                AddMenu = false;
            }
            else
            {
                Console.AddLine("Entry name either empty or contains symbol '|' or entry with the same name already exist or some weird as shit internal error");
            }
        }
    }
}
