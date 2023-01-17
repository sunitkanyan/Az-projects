// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;

Console.WriteLine("Hello, World!");


string containerName = "testcnt";
BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=myaccoiunt1234589;AccountKey=xwCC04RnTA4R2izqm7NdD0dwz1gaNhHBEyDQ16NUVWCuRDghN/2XLDmgPx0KgBnvhbe6nn+ZswLP+ASt+nZVOQ==;EndpointSuffix=core.windows.net", containerName);

var blobs = container.GetBlobs();
foreach (var blob in blobs)
{
    Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:YYYY-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
}
Console.WriteLine("press any to exit");
Console.ReadLine();