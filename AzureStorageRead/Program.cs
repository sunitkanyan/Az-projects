// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;

Console.WriteLine("Hello, World!");


string containerName = "testcnt";
BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=myaccoiunt1234589;AccountKey=xwCC04RnTA4R2izqm7NdD0dwz1gaNhHBEyDQ16NUVWCuRDghN/2XLDmgPx0KgBnvhbe6nn+ZswLP+ASt+nZVOQ==;EndpointSuffix=core.windows.net", containerName);

// Uploads the image to Blob storage.  If a blb already exists with this name it will be overwritten
string blobName = "test";
string fileName = "test.png";
BlobClient blobClient = container.GetBlobClient(blobName);
blobClient.Upload(fileName, true);

var blobs = container.GetBlobs();
foreach (var blob in blobs)
{
    Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:YYYY-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
}
Console.WriteLine("new container name please");

string cnt = Console.ReadLine();

string containerName1 = cnt;
BlobContainerClient container1 = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=myaccoiunt1234589;AccountKey=xwCC04RnTA4R2izqm7NdD0dwz1gaNhHBEyDQ16NUVWCuRDghN/2XLDmgPx0KgBnvhbe6nn+ZswLP+ASt+nZVOQ==;EndpointSuffix=core.windows.net", containerName);

container1.CreateIfNotExists();
Console.WriteLine("press any to exit");
Console.ReadLine();