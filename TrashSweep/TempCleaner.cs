using System;
using System.IO;
using System.Windows.Forms;

namespace TrashSweep
{
    public class TempCleaner
    {
        public static void CleanTemp(ref long totalJunkFiles, ref long bytesCleaned, ref StreamWriter logFile)
        {
            string tempPath = Path.GetTempPath();
            foreach (string f in Directory.GetDirectories(tempPath, "*", SearchOption.AllDirectories))
            {
                foreach (string p in Directory.GetFiles(f, "*", SearchOption.AllDirectories))
                {
                    FileInfo info = null;
                    try
                    {
                        info = new FileInfo(p);
                        totalJunkFiles++;
                        bytesCleaned += info.Length;
                        File.Delete(p);
                        logFile.WriteLineAsync("[VERBOSE]" + p + " deleted successfully" +
                                               DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString());
                    }
                    catch (AccessViolationException ex)
                    {
                        if (info != null) bytesCleaned -= info.Length;
                        totalJunkFiles--;
                        logFile.WriteLineAsync("[ERROR]" + ex.Message + "\t" +
                                               DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString());
                    }
                    catch (Exception ex)
                    {
                        if (info != null) bytesCleaned -= info.Length;
                        totalJunkFiles--;
                        logFile.WriteLineAsync("[ERROR]" + ex.Message + "\t" +
                                               DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString());
                    }
                }
            }
            foreach (string s in Directory.GetFiles(tempPath, "*", SearchOption.AllDirectories))
            {
                FileInfo info = null;
                try
                {
                    info = new FileInfo(s);
                    totalJunkFiles++;
                    bytesCleaned += info.Length;
                    File.Delete(s);
                    logFile.WriteLineAsync("[VERBOSE]" + s + " deleted successfully" +
                                           DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString());
                }
                catch (AccessViolationException ex)
                {
                    if (info != null) bytesCleaned -= info.Length;
                    totalJunkFiles--;
                    logFile.WriteLineAsync("[ERROR]" + ex.Message + "\t" +
                                           DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString());
                }
                catch (Exception ex)
                {
                    if (info != null) bytesCleaned -= info.Length;
                    totalJunkFiles--;
                    logFile.WriteLineAsync("[ERROR]" + ex.Message + "\t" +
                                           DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString());
                }
            }
            MessageBox.Show("Cleaning successfully completed\nSummary:\nTotal cleaned - " + (bytesCleaned/1024.0) +
                            " kBytes");
        }
    }
}