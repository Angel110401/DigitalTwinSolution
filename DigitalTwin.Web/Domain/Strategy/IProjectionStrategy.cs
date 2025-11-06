namespace DigitalTwin.Web.Domain.Strategy
{
    public interface IProjectionStrategy
    {
        (double t, double p, double rpm) Next((double t, double p, double rpm) current);
    }

    public class LinearTrendProjection : IProjectionStrategy
    {
        private readonly (double t, double p, double rpm) _delta;
        public LinearTrendProjection(double dt, double dp, double drpm) => _delta = (dt, dp, drpm);
        public (double t, double p, double rpm) Next((double t, double p, double rpm) cur)
            => (cur.t + _delta.t, cur.p + _delta.p, cur.rpm + _delta.rpm);
    }

    public class RandomWalkProjection : IProjectionStrategy
    {
        private readonly Random _rnd = new();
        public (double t, double p, double rpm) Next((double t, double p, double rpm) cur)
            => (cur.t + (_rnd.NextDouble() - 0.5) * 2,
                cur.p + (_rnd.NextDouble() - 0.5) * 1.5,
                Math.Max(0, cur.rpm + (_rnd.NextDouble() - 0.5) * 50));
    }
}

