namespace VoucherCK.Utility
{
    public class WriteFileHelper
    {
        public static async Task WriteFileHelperAsync(string data, string linkFile)
        {
            string pathString = linkFile;
            var currentDate = DateTime.Now;

            string fileName = currentDate.ToString("yyyy-MM-dd") + ".csv";

            System.IO.Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, fileName);

            if (!File.Exists(pathString))
            {
                using StreamWriter file = new(pathString);
                var template = "Time,BarCode,Voucher,Status,StoreCode,PrizeCode,Message";
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
