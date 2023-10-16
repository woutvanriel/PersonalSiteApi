namespace PersonalSiteApi
{
    public static class Utility
    {
        public static async Task<string> UploadFile(IFormFile file, string folder, IWebHostEnvironment hostEnvironment)
        {
            string now = DateTime.Now.Ticks.ToString();
            string folderLocation = Path.Combine(hostEnvironment.ContentRootPath, $"uploads/{folder}/{now}");
            while (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }
            string fileLocation = Path.Combine(folderLocation, file.FileName);
            using (Stream fileStream = new FileStream(fileLocation, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Path.Combine($"{folder}/{now}", file.FileName);
        }
    }
}
