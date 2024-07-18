using static ISubscriber;

WeatherPublisher weatherPublisher = new WeatherPublisher();
StockPublisher stockPublisher = new StockPublisher();

User user1 = new User("Alice");
User user2 = new User("Bob");

weatherPublisher.Subscribe(user1);
stockPublisher.Subscribe(user1);
stockPublisher.Subscribe(user2);

weatherPublisher.Notify("It's sunny today");
stockPublisher.Notify("AAPL stock price is $150");

stockPublisher.Unsubscribe(user2);
stockPublisher.Notify("GOOGL stock price is $2800");




public interface IPublisher
{
    void Subscribe(ISubscriber subscriber);
    void Unsubscribe(ISubscriber subscriber);
    void Notify(string message);
}

public interface ISubscriber
{
    void Update(string message);
    string Name { get; }

    public class WeatherPublisher : IPublisher
    {
        private List<ISubscriber> _subscribers = new List<ISubscriber>();

        public void Subscribe(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            Console.WriteLine($"NEW SUB {subscriber.Name} to Weather");
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            Console.WriteLine($"{subscriber.Name} UNSUB from Weather");
        }

        public void Notify(string message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update("Weather: " + message);
            }
        }
    }

    public class StockPublisher : IPublisher
    {
        private List<ISubscriber> _subscribers = new List<ISubscriber>();

        public void Subscribe(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            Console.WriteLine($"NEW SUB {subscriber.Name} to Stock");
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            Console.WriteLine($"{subscriber.Name} UNSUB from Stock");
        }

        public void Notify(string message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update("Stock: " + message);
            }
        }
    }

    public class User : ISubscriber
    {
        public string Name { get; private set; }

        public User(string name)
        {
            Name = name;
        }

        public void Update(string message)
        {
            Console.WriteLine($"{Name} received: {message}");
        }
    }
}

