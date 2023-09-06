namespace RapidPay.Services.UFE
{
    public class UFEService
    {
        public UFEService()
        {
            var random = new Random();
            Fee = Math.Round((decimal)random.NextDouble(), 2);
            Timer = new System.Timers.Timer(3600000);
            Timer.Elapsed += (sender, e) => UpdateFee();
            Timer.Start();
        }

        public decimal Fee { get; private set; }

        private System.Timers.Timer Timer { get; set; }

        private void UpdateFee()
        {
            var random = new Random();
            decimal factor = (decimal)random.NextDouble() * 2;
            Fee = Math.Round(Fee * factor, 2);
        }
    }
}
