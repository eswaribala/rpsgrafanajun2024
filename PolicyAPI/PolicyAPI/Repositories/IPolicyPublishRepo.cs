namespace PolicyAPI.Repositories
{
    public interface IPolicyPublishRepo
    {

        Task<string> PublishPolicyData(string TopicName, string Message, 
            IConfiguration configuration);
    }
}
