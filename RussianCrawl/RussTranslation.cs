using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace RussianCrawl
{
    [BepInPlugin("org.anumania.plugins.russiantranslation", "RussianTranslation", "1.0.0.0")]
    public class RussTranslation : BaseUnityPlugin
    {
        public void Awake()
        {
            On.SystemMain.LoadLevel_string += SystemMain_LoadLevel_string;
            string ass = "";
            foreach (Weapon a in Resources.FindObjectsOfTypeAll<Weapon>())
            {
                //ass += a.name + "#" + a.m_weaponName + "#" + a.m_weaponDescription + "|\n";
            }
            foreach (Environment a in Resources.FindObjectsOfTypeAll<Environment>())
            {
                //ass += a.name + "#" + a.GetDisplayName() + "|\n";
            }
            List<Deity> ds = SystemDeity.GetDeities();
            foreach (Deity d in ds)
            {
                //ass += typeof(Deity).GetField("m_name", (BindingFlags)36).GetValue(d) + "#" + d.GetText() + "#" + d.GetTextFlavour() + "|\n";
            }
            foreach (GameObject a in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                switch (a.name)
                {

                }
            }
            foreach(Powerup pw in Resources.FindObjectsOfTypeAll<Powerup>()){
                ass += pw.name + "#" + pw.m_displayName + "#" + pw.m_description + "|\n";
            }
            //Console.WriteLine(ass);
            File.WriteAllText("./out.log", ass);
            if (!Directory.Exists("./Crawl_Translation"))
            {
                Directory.CreateDirectory("./Crawl_Translation");
            }
            string skunkweed;
            if (File.Exists("./Crawl_Translation/weapon.txt"))
            {
                
                System.IO.StreamReader file = new System.IO.StreamReader("./Crawl_Translation/weapon.txt");
                string line = "";
                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    foreach (Weapon a in Resources.FindObjectsOfTypeAll<Weapon>())
                    {
                        if(line.Substring(0,line.IndexOf("#")) == a.name)
                        {
                            string theRest = line.Substring(line.IndexOf("#")+1,line.Length- line.IndexOf("#")-1);
                            //Console.WriteLine(theRest);
                            a.m_weaponName = theRest.Substring(0, theRest.IndexOf("#"));
                            theRest = theRest.Substring(theRest.IndexOf("#") + 1, theRest.Length - theRest.IndexOf("#") - 1);
                            a.m_weaponDescription = theRest.Substring(0, theRest.IndexOf("|"));
                        }
                    }
                }
            }
            if (File.Exists("./Crawl_Translation/powerups.txt"))
            {
                System.IO.StreamReader file = new System.IO.StreamReader("./Crawl_Translation/powerups.txt");
                string line = "";
                string wholeFile = file.ReadToEnd();
                while (wholeFile.IndexOf("|") != -1)
                {
                    line = wholeFile.Substring(0, wholeFile.IndexOf("|")+1);
                    wholeFile.Remove(0, wholeFile.IndexOf("|"));
                    //Console.WriteLine(line);
                    foreach (Powerup a in Resources.FindObjectsOfTypeAll<Powerup>())
                    {
                        if (line.Substring(0, line.IndexOf("#")) == a.name)
                        {
                            
                            string theRest = line.Substring(line.IndexOf("#") + 1, line.Length - line.IndexOf("#") - 1);
                            
                            //Console.WriteLine(theRest);
                            a.m_displayName = theRest.Substring(0, theRest.IndexOf("#"));
                            theRest = theRest.Substring(theRest.IndexOf("#") + 1, theRest.Length - theRest.IndexOf("#") - 1);
                            Console.WriteLine(theRest);
                            a.m_description = theRest.Substring(0, theRest.IndexOf("|"));
                            
                        }
                    }
                }
            }

                foreach (Font font in Resources.FindObjectsOfTypeAll<Font>())
            {
                if(font.name == "CrawlLargeRussian")
                {
                    
                }

            }
            foreach (exSpriteFont font in Resources.FindObjectsOfTypeAll<exSpriteFont>())
            {
                //Console.WriteLine(font.name);
            }
                //Console.WriteLine(ass);

                //On.SystemMain.Awake += SystemMain_Awake;
        }

        private void SystemMain_Awake(On.SystemMain.orig_Awake orig, SystemMain self)
        {
            List<Type> objList = new List<Type>();
            List<string> ls = new List<string>();
            foreach (UnityEngine.GameObject i in Resources.FindObjectsOfTypeAll<UnityEngine.GameObject>())
            {

                switch (i.name)
                {
                    case "TextMenuMain": //143518 just in case i wanna swap out names for instance id's in the future for speed
                        AssetBundle ab = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("RussianCrawl.dll", "test"));
                        Font rusfont = ab.LoadAsset<Font>("CrawlLargeRussian");
                        //Font rusfont = Resources.Load<Font>(Assembly.GetExecutingAssembly().Location.Replace("RussianCrawl.dll", "test"));
                        foreach(MenuTextMenuItem mitem in Resources.FindObjectsOfTypeAll<MenuTextMenuItem>())
                        {
                            //Console.WriteLine(mitem.name);
                            switch (mitem.name)
                            {
                                case "TextMenuTextMainStart":
                                    mitem.GetComponent<TextMesh>().fontSize = 100;
                                    break;
                                case "TextMenuTextMainLibrary":
                                    mitem.GetComponent<TextMesh>().fontSize = 100;
                                    break;
                                case "TextMenuTextMain":
                                    //mitem.GetComponent<TextMesh>().text = "Б";
                                    string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890Б";
                                    if (rusfont == null)
                                    {
                                        Console.WriteLine("its null lol");
                                    }
                                    else
                                    {
                                        //rusfont.characterInfo = null;
                                        //Console.WriteLine(rusfont.fontSize);
                                        //TextMesh textMesh = mitem.GetComponent<TextMesh>();
                                        //Console.WriteLine(textMesh.fontSize-=100);
                                        //textMesh.fontSize -= 100;
                                        mitem.GetComponent<TextMesh>().fontSize = 100;
                                        //mitem.GetComponent<TextMesh>().font = rusfont;
                                        //mitem.GetComponent<TextMesh>().font.RequestCharactersInTexture(text, textMesh.fontSize, FontStyle.Normal);
                                    }
                                    break;
                            }
                        }
                        //Console.WriteLine(i.GetInstanceID());
                        MenuTextMenu mainMenu = i.GetComponent<MenuTextMenu>();
                        List<MenuTextMenuItem> theGuys = (List<MenuTextMenuItem>)typeof(MenuTextMenu).GetField("m_items", (BindingFlags)36).GetValue(mainMenu);
                        //Console.WriteLine(theGuys.Count);
                        //Console.WriteLine(mainMenu.m_itemsData.Count);

                        mainMenu.m_itemsData[0].m_text = "НАЧАТЬ ИГРУ";

                        mainMenu.m_itemsData[1].m_text = "ХРАНИЛИЩЕ";
                        mainMenu.m_itemsData[2].m_text = "НАСТРОЙКИ";
                        mainMenu.m_itemsData[3].m_text = "Авторы".ToUpper();
                        //mainMenu.m_itemsData[3].m_text = "Бasd";
                        mainMenu.m_itemsData[4].m_text = "ВЫЙТИ";
                        //mainMenu.m_itemsData[4].m_text = "Б";
                        break;

                }
            }
        } 

        private void SystemMain_LoadLevel_string(On.SystemMain.orig_LoadLevel_string orig, string levelName)
        {
            orig(levelName);
        }
    }
}
