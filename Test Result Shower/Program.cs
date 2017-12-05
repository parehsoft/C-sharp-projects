using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;


namespace BRA_Lab_Project_Result_Shower
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1 || args[0].Length == 0)
            {
                Console.WriteLine("Please provide only one argument, which is path to folder with *.trx files.");
                return 1;
            }

            string pathToFolderWithTrxFiles = args[0];
            if (Directory.Exists(pathToFolderWithTrxFiles) != true)
            {
                Console.WriteLine("Directory, which you have provided doesn't exist! Perhaps, remove the last \\ ?");
                return 1;
            }

            string[] arrayOfFilePaths = Directory.GetFiles(pathToFolderWithTrxFiles, "*.trx");
            if (arrayOfFilePaths.Length == 0)
            {
                Console.WriteLine("No *.trx files found inside of the provided directory.");
                return 1;
            }

            /* If all is ok, writing to file will be estabilished. */
            string resultTextFilePath;
            if (pathToFolderWithTrxFiles[pathToFolderWithTrxFiles.Length - 1].Equals("\\") == true) { resultTextFilePath = pathToFolderWithTrxFiles + "__Summary.txt"; }
            else { resultTextFilePath = pathToFolderWithTrxFiles + "\\" + "__Summary.txt"; }

            StreamWriter sw;
            try
            {
                sw = new StreamWriter(resultTextFilePath, true /* append to the end of the file, yes */, ASCIIEncoding.ASCII);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while file processing: {0}.", resultTextFilePath);
                Console.WriteLine(e.GetType().ToString());
                return 1;
            }

            sw.WriteLine("------- NEW RESULTS, " + System.Globalization.CultureInfo.CurrentCulture.ToString() + " time: " + DateTime.Now.ToString() + " -------");
            sw.WriteLine(" file count: {0}\n", arrayOfFilePaths.Length);
            
            

            string[] arrayOfFileNames = new string[arrayOfFilePaths.Length];
            for (int i = 0; i < arrayOfFilePaths.Length; i++)
            {
                Console.Write("{0} : ", arrayOfFileNames[i] = Path.GetFileName(arrayOfFilePaths[i])); // Write Name of the processed *.trx file.
                     sw.Write("{0} : ", arrayOfFileNames[i] = Path.GetFileName(arrayOfFilePaths[i]));

                XmlDocument trxFile = new XmlDocument(); // Create xml document.

                /* Load xml document from file and close the file stream. */
                FileStream fileStream = new FileStream(arrayOfFilePaths[i], FileMode.Open, FileAccess.Read);
                trxFile.Load(arrayOfFilePaths[i]);
                fileStream.Close(); // Close the stream because files will remain used and won't be deleted.

                /*
                 * Test Result Aggregation nodes are loaded into the list of nodes.
                 * The 0th Test Result Aggregation node is the test list outcome.
                 */
                XmlNodeList TestResultAggregation = trxFile.GetElementsByTagName("TestResultAggregation");

                /* Print the result of the test in the test list. */
                Console.Write(TestResultAggregation[0].Attributes["testName"].Value);
                     sw.Write(TestResultAggregation[0].Attributes["testName"].Value);
                Console.Write(" - ");
                     sw.Write(" - ");
                string outcome = TestResultAggregation[0].Attributes["outcome"].Value;
                Console.WriteLine(outcome);
                     sw.WriteLine(outcome);

                /* 
                 * If the test failed print sub results what passed before it failed and than the failed subtest and not executed sub test's procedures.
                 * Execution is stopped after the first failed sub test as results afterwards are not interesting.
                 */
                if (outcome.Equals("failed", StringComparison.OrdinalIgnoreCase) == true)
                {
                    for (int j = 0; j < TestResultAggregation.Count; ++j)
                    {
                        if (j == 0) { continue; } // Skip the Oth test result element which is the root element, name of the test list and it is shown above already.
                        
                        /* Get outcome of the sub tests of the test list. */
                        outcome = TestResultAggregation[j].Attributes["outcome"].Value;
                        Console.Write("  " + outcome + " - " + TestResultAggregation[j].Attributes["testName"].Value);
                             sw.Write("  " + outcome + " - " + TestResultAggregation[j].Attributes["testName"].Value);

                        /* If it failed, print why. If it didn't fail, than there is no reason to nest into it. */
                        if (outcome.Equals("failed", StringComparison.OrdinalIgnoreCase) == true)
                        {
                            Console.WriteLine(" :"); // Write colon if we are diving into the failed sub test, to separate it from others before.
                                 sw.WriteLine(" :");

                            XmlNode subNode = TestResultAggregation[j].LastChild; // Get the last child of the selected failed test. Last chiled of the failed is <InnerResults>.
                            XmlNodeList innerResults = subNode.ChildNodes; // Get the child nodes of <InnerResults>, this is where sub tests's results are located.

                            /* Print outcome of each test. */
                            foreach (XmlNode testResult in innerResults)
                            {
                                Console.WriteLine("    " + testResult.Attributes["testName"].Value + " - " + testResult.Attributes["outcome"].Value);
                                     sw.WriteLine("    " + testResult.Attributes["testName"].Value + " - " + testResult.Attributes["outcome"].Value);
                            }

                            break; // Break the execution, because the results after the first failed sub test are not interesting.
                        }
                        else
                        {
                            Console.WriteLine(); // Get to the next line if test didn't fail.
                                 sw.WriteLine();
                        }
                    }
                    Console.WriteLine();
                         sw.WriteLine();
                }
                sw.Flush(); // Flush the stream after file was processed.
            }
            sw.Close(); // Flush the stream for file output and close it.
            Console.ReadKey();
            return 0;
        }
    }
}
