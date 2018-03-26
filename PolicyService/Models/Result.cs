namespace Service.PolicyService.Models
{
    public class Result
    {
        public string ErrorMessage { get; set; }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(this.ErrorMessage);
            }
        }
    }
}