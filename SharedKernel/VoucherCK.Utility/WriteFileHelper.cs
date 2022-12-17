namespace VoucherCK.Utility
{
    public class WriteFileHelper
    {
        public static async Task WriteFileHelperAsync(string data)
        {
            string pathString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            System.IO.Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, fileName);

            if (!File.Exists(pathString))
            {
                using StreamWriter file = new(pathString);
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
