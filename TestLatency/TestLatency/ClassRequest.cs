namespace TestLatency
{
    public class ClassRequest
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public string S0 { get; set; }
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S4 { get; set; }
        public string S5 { get; set; }
        public string S6 { get; set; }

        public ClassRequest(int count=1, int total=1)
        {
            Count = count;
            Total = total;
        }
    }
}
