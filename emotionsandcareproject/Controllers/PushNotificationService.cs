using FirebaseAdmin.Messaging;

public class PushNotificationService
{
    public async Task<string> SendPushAsync(string title, string body, string token, Dictionary<string, string> data)
    {

        // var dataExample = new Dictionary<string, string>
        // {
        //     { "module", "yard", 
         //       "event", "growStage"
         //},
        // };
        var message = new Message()
        {
            Notification = new Notification()
            {
                Title = title,
                Body = body
            },
            Token = token,
            Data = data
        };

        try
        {
            var notiResult = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine("Notificación enviada: " + notiResult);
            return notiResult;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error enviando notificación: " + ex.Message);
            throw; // Re-throw para manejo adicional si es necesario
        }
    }
}

