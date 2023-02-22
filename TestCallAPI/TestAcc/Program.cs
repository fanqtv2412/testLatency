using Confluent.Kafka;

class Program
{
    static async Task Main(string[] args)
    {
        // Số lần gọi API
        int numCalls = 10;

        // API endpoint
        string url = "https://localhost:7003/api/Request";

        // Thực hiện gọi API numCalls lần
        using var client = new HttpClient();
        for (int i = 0; i < numCalls; i++)
        {
            HttpResponseMessage response = await client.GetAsync(url);

            // Kiểm tra mã trạng thái HTTP
            if (response.IsSuccessStatusCode)
            {
                // Xử lý kết quả trả về
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response {i + 1}: {responseString}");
            }
            else
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Response {i + 1} failed with status code {response.StatusCode}");
            }
        }
    }
}