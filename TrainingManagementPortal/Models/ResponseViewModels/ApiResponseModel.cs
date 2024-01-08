namespace TrainingManagementPortal.Models.ResponseViewModels
{
    public class ApiResponseModel
    {
        public string Code { get; set; } = "200";
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = "success";
        public object Body { get; set; }
    }
}
