using System;
using System.Media;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Trinet.Networking;
using System.Diagnostics;
using System.Resources;

namespace bgrCopySharePath
{
    public class bgrCSP
    {

        string version = "1.053";

        public bgrCSP(string [] args)
        {
            int nshared = 0; // counter for number of shared files

            if (args.Length == 0)
                MessageBox.Show("ovaj program se koristi tako što prevučeš jedan ili\r\nviše fajlova/foldera na ikonicu koju si upravo kliknuo\r\n\r\nza više informacija pogledaj uputstvo", "bgrCopySharePath " + version, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            else
            {
                string all = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    string temp = ShareCollection.PathToUnc(args[i]);
                    if (temp.StartsWith("\\"))
                    {
                        all += "[URL]" + temp + "[/URL]\r\n";
                        nshared++;
                    }
                }

                if (all == string.Empty)
                {
                    MessageBox.Show("fajlovi su iz foldera koji NIJE SHAREOVAN\r\n\r\nshareuj folder pa probaj opet\r\nili stavi fajlove u neki već shareovan folder", "greška", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (nshared < args.Length)
                    {
                        string srpskaGramatika;
                        int notshared = args.Length - nshared;
                        if (notshared % 10 == 1 && notshared != 11) srpskaGramatika = "izabran fajl nije shareovan";
                        else if ((notshared % 10 == 2 || notshared % 10 == 3 || notshared % 10 == 4) && (notshared != 12 && notshared != 13 && notshared != 14)) srpskaGramatika = "izabrana fajla nisu shareovana";
                        else srpskaGramatika = "izabranih fajlova nije shareovano";
                        MessageBox.Show(notshared + " od " + args.Length + " " + srpskaGramatika + "\r\n\r\npreostalih " + nshared + " su pripremljeni za pasteovanje", "poruka", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    try
                    {

                        all = "[QUOTE]\r\n" + all + "[/QUOTE]";

                        //Clipboard.SetText(all);
                        
                        //Clipboard.Clear();
                        //Clipboard.SetDataObject(all, true, 6, 250);

                        TextBox tb = new TextBox();
                        tb.Text = all;
                        tb.SelectAll();
                        tb.Copy();
                        //MessageBox.Show(tb.Text);       
                        SystemSounds.Beep.Play();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Došlo je do greške. Zapiši ili izvadi screenshot i daj bugeru\r\n\r\n" + e.ToString() + "\r\n" + e.Message, "obavesti bugera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }


            }
            string message = checkForUpdate();
            if(message!="OK") 
                MessageBox.Show("Došlo je do sledeće greške u pokušaju update-a:\r\n\r\n\"" + message + "\"\r\n\r\nzapamti tekst greške i javi bugeru", "greška", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public string checkForUpdate()
        {
            string newVersion = string.Empty;
            int i;
            FileStream fin;
            FileStream fout;

            try
            {
                StreamReader getVersion = File.OpenText(@"\\buger\bgrCSPsecret$\poslednja verzija.txt");
                newVersion = getVersion.ReadLine();

                if (newVersion != version)
                {
                    string updaterPath = Path.Combine(Path.GetTempPath(), "bgrCSPupdater.exe");

                    try
                    {
                        fin = new FileStream(@"\\buger\bgrCSPsecret$\bgrCSPupdater.exe", FileMode.Open, FileAccess.Read);
                    }
                    catch (FileNotFoundException)
                    {
                        return "Updater nije pronadjen!";
                    }
                    catch (UnauthorizedAccessException)
                    {
                        return "Pristup nije dozvoljen. Probaj opet za par sekundi.";
                    }


                    try
                    {
                        fout = new FileStream(updaterPath, FileMode.Create);
                    }
                    catch (IOException)
                    {
                        return "Greška prilikom kreiranja updater-a u lokalu";
                    }


                    try
                    {
                        do
                        {
                            i = fin.ReadByte();
                            if (i != -1) fout.WriteByte((byte)i);
                        } while (i != -1);
                    }
                    catch (IOException)
                    {
                        return "Greška prilikom kopiranja updatera";
                    }

                    fin.Close();
                    fout.Close();
                    
                    MessageBox.Show("Postoji nova verzija programa bgrCopySharePath\r\n\r\nklikni OK da updateuješ program sa trenutne verzije " + version + " na " + newVersion, "nova verzija", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Process myProc = Process.Start(updaterPath, "\"" + Directory.GetCurrentDirectory() + "\" " + version + " " + newVersion);
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
            }
            return "OK";
        }
    }
}
