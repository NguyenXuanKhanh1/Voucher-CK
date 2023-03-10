namespace VoucherCK.Utility
{
    public class WriteFileHelper
    {
        public static async Task WriteFileHelperAsync(string data, string linkFile)
        {
            string pathString = linkFile;
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

            System.IO.Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, fileName);

            if (!File.Exists(pathString))
            {
                using StreamWriter file = new(pathString);
                var template = "Time,BarCode,Status,StoreCode,PrizeCode";
                await file.WriteLineAsync(template);
                await file.WriteLineAsync(data);
            }
            else
            {
                using StreamWriter file = new(pathString, append: true);
                await file.WriteLineAsync(data);
            }
        }
    }
}
