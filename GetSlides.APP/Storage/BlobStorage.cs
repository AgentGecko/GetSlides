using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetSlides.APP.Storage
{
    public class BlobStorage
    {
        public static async Task<Tuple<string, string>> SavePdfToBlob(Stream pdfStream, string blobName)
        {
            CloudStorageAccount storageAccount = CreateStorageAccount(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer pdfContainer = blobClient.GetContainerReference("pdfstorage");
            try
            {
                await pdfContainer.CreateIfNotExistsAsync();
            }
            catch (StorageException)
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            await UploadPdf(pdfContainer, pdfStream, blobName);
            await UploadThumbnail(pdfContainer, pdfStream, blobName);


            return new Tuple<string, string>(pdfContainer.StorageUri.PrimaryUri.ToString() + "/" + blobName, null);
        }

        private static async Task UploadPdf(CloudBlobContainer container, Stream pdfStream, string blobName)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            await blockBlob.UploadFromStreamAsync(pdfStream);
        }

        private static async Task UploadThumbnail(CloudBlobContainer container, Stream pdfStream, string blobName)
        {
            
        }

        private static CloudStorageAccount CreateStorageAccount(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }        

    }
}