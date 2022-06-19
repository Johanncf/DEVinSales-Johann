namespace DevInSales.Core.Data.Dtos
{
    public class AddAddressRequest
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Cep { get; set; }
    }
}
