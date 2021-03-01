using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergePdfFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new List<string>();
            files.Add("file0.pdf");
            files.Add("file1.pdf");
            files.Add("file2.pdf");
            files.Add("file3.pdf");
            files.Add("file4.pdf");
            files.Add("file5.pdf");
            files.Add("file6.pdf");
            files.Add("file7.pdf");
            files.Add("file8.pdf");
            files.Add("file9.pdf");

            Merge(files, "out.pdf");
        }


        public static void Merge(List<String> InFiles, String OutFile)
        {

            using (FileStream stream = new FileStream(OutFile, FileMode.Create))
            using (Document doc = new Document())
            using (PdfCopy pdf = new PdfCopy(doc, stream))
            {
                doc.Open();

                PdfReader reader = null;
                PdfImportedPage page = null;

                //fixed typo
                InFiles.ForEach(file =>
                {
                    reader = new PdfReader(file);

                    for (int i = 0; i < reader.NumberOfPages; i++)
                    {
                        page = pdf.GetImportedPage(reader, i + 1);
                        pdf.AddPage(page);
                    }

                    pdf.FreeReader(reader);
                    reader.Close();
                    File.Delete(file);
                });
            }
        }
    }
}