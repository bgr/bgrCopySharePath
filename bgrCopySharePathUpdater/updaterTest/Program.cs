using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.IO;
using System.Diagnostics;

namespace updaterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            bool copyOK = false;
            int i;
            FileStream fin; 
            FileStream fout;
            
            try
            {
                string newVerPath = Path.Combine(args[0], "bgrCopySharePath.new");
                Console.WriteLine("Ovo je automatski updater za bgrCopySharePath\n\n");
                try
                {
                    fin = new FileStream(@"\\buger\bgrCopySharePath\bgrCopySharePath.exe", FileMode.Open, FileAccess.Read);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\n\nNova verzija programa nije nadjena\nUpdate nije izvrsen");
                    Console.Read();
                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("\n\nPristup nije dozvoljen\nUpdate nije izvrsen");
                    Console.Read();
                    return;
                }

                try
                {
                    fout = new FileStream(newVerPath, FileMode.Create);
                }
                catch (IOException exc)
                {
                    Console.WriteLine(exc.Message + "\n\nNe moze se pisati u folder " + args[0] + "\n\nUpdate nije izvrsen");
                    Console.Read();
                    return;
                }

                // Copy File
                try
                {
                    do
                    {
                        i = fin.ReadByte();
                        if (i != -1) fout.WriteByte((byte)i);
                    } while (i != -1);
                    Console.WriteLine("Nova verzija programa iskopirana u " + newVerPath);
                    copyOK = true;
                }
                catch (IOException)
                {
                    Console.WriteLine("\n\nGreska prilikom kopiranja\nUpdate nije izvrsen");
                    Console.Read();
                }

                fin.Close();
                fout.Close();

                if (copyOK)
                {
                    Console.WriteLine("Uspesno iskopirana nova verzija fajla\nSada cu da pokusam da obrisem staru");
                    string oldVerPath = Path.Combine(args[0], "bgrCopySharePath.exe");
                    FileInfo fi = new FileInfo(oldVerPath);
                    try // brisanje stare
                    {
                        fi.Delete();
                        Console.WriteLine("Stara verzija je obrisana");
                    }
                    catch(IOException)
                    {
                        Console.WriteLine("\n\nStara verzija NIJE obrisana!\nUpdate nije izvrsen");
                        Console.Read();
                        Environment.Exit(0);
                    }
                    catch(UnauthorizedAccessException)
                    {
                        Console.WriteLine("\n\nStara verzija NIJE obrisana!\nUpdate nije izvrsen");
                        Console.Read();
                        Environment.Exit(0);
                    }


                    FileInfo fi2 = new FileInfo(newVerPath);

                    try // renameovanje nove iz .new u .exe
                    {
                        fi2.MoveTo(oldVerPath);
                        Console.WriteLine("\n\n\nNova verzija je uspesno instalirana ;)\n\n\nLupi enter za izlazak");
                        try
                        {
                            TextWriter tw = File.AppendText(@"\\buger\bgrCSPsecret$\updatelog.txt");
                            tw.WriteLine(DateTime.Now + "  " + Environment.MachineName + "  sa " + args[1] + " na " + args[2]);
                            tw.Close();
                        }
                        catch { }
                        Console.Read();
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("\n\nNova verzija nije uspesno renameovana u .exe!\nUpdate nije izvrsen");
                        Console.Read();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("\n\nNova verzija nije uspesno renameovana u .exe!\nUpdate nije izvrsen");
                        Console.Read();
                    }

                    
                }
            }

            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Wrong usage, bro");
                Console.Read();
            }
          
        
            
        }
    }
}
