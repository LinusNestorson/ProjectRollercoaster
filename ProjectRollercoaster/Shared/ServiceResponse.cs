namespace ProjectRollercoaster.Shared
{
    /// <summary>
    /// Class with response from services and message depending if success or failure.
    /// </summary>
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}